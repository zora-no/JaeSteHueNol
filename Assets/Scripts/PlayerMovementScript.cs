using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovementScript : MonoBehaviour
{
    private GameManager game;
    [SerializeField] private SpawnManager _spawnManager;
    private BallThrowScript ballscript;
    private AudioManager _audioManager;

    private bool _p1IsPowerUpOn = false;
    private PowerUpType _p1PowerUpType;
    private bool _p2IsPowerUpOn = false;
    private PowerUpType _p2PowerUpType;
    
    
    Rigidbody rb;
    Rigidbody rb1;
    Rigidbody rb2;
    private float originalMoveSpeed = 10.0f;
    private float moveSpeed = 10.0f;
    private int playerNumber;
    private string _otherBallName; // tag of the ball that can hit this player

    // Start is called before the first frame update
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        game = GameManager.Instance;
        ballscript = gameObject.GetComponent<BallThrowScript>();
        
        rb = GetComponent<Rigidbody>(); // rb of this player
        if (this.name == "Player1")
        {
            playerNumber = 1;
            _otherBallName = "Ball2";
        }
        else if (this.name == "Player2")
        {
            playerNumber = 2;
            _otherBallName = "Ball1";
        }
        
        // rigidbodies of both players (for power ups)
        rb1 = GameObject.Find("Player1").GetComponent<Rigidbody>();
        rb1.constraints = RigidbodyConstraints.FreezeRotation;
        rb2 = GameObject.Find("Player2").GetComponent<Rigidbody>();
        rb2.constraints = RigidbodyConstraints.FreezeRotation;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        PowerUpEffect();
        rb1.constraints = RigidbodyConstraints.FreezeRotation;
        rb2.constraints = RigidbodyConstraints.FreezeRotation;
    }
    
    // Executes when player leaves a field
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "slowfield")
        {
            ResetMovespeed();
        }
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void onDisenable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }
    
    public void ResetMovespeed()
    {
        moveSpeed = originalMoveSpeed;
    }
    void Move()
    {
        float speedHorizontal = 0;
        float speedVertical = 0;
        float speedForward = 0;
        if (playerNumber == 1)
        {
            //limits player movement 
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -15f, 15f), Mathf.Clamp(transform.position.y, 17.5f, 37f), 22.52f);
            
            if (Input.GetKey(KeyCode.A))
            {
                speedHorizontal = moveSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                speedHorizontal = -moveSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                speedVertical = -moveSpeed;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                speedVertical = moveSpeed;
            }
            rb.velocity = new Vector3((float)(speedHorizontal), (float)(speedVertical), (float)(speedForward)).normalized * moveSpeed;

        }
        else if (playerNumber == 2)
        {
            //limits player movement 
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -15f, 15f), Mathf.Clamp(transform.position.y, 17.5f, 37f), -31.12f);
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                speedHorizontal = -moveSpeed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                speedHorizontal = moveSpeed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                speedVertical = -moveSpeed;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                speedVertical = moveSpeed;
            }
            rb.velocity = new Vector3((float)(speedHorizontal), (float)(speedVertical), (float)(speedForward)).normalized * moveSpeed;

        }
    }
    
    IEnumerator Unfreeze(int player, float delayTime)
        // unfreeze player after X seconds
    {
        yield return new WaitForSeconds(delayTime);
        
        if (player == 1)
        {
            rb1.constraints = RigidbodyConstraints.None;
        }
        else if (player == 2)
        {
            rb2.constraints = RigidbodyConstraints.None;
        }
    }
    
    IEnumerator NormalizeShootFreq(float delayTime) 
        // reduce shooting frequency back to normal value after X seconds
    {
        yield return new WaitForSeconds(delayTime);

        ballscript.throwCooldown *= 3f;
        
    }

    IEnumerator NormalizeScoring(int player, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if (player == 1)
        {
            game.scorePointsP1 /= 2;
        }
        else if (player == 2)
        {
            game.scorePointsP2 /= 2;
        }
    }
    
    
    void PowerUpEffect()
    {
        if (_p1IsPowerUpOn)
        {
            switch (_p1PowerUpType)
            {
                case PowerUpType.Freeze:
                    // fix position of other player
                    rb2.constraints = RigidbodyConstraints.FreezePosition;
                    _audioManager.Play("Freeze");
                    StartCoroutine(Unfreeze(2, 5f));
                    _p1IsPowerUpOn = false;
                    break;
                case PowerUpType.ShootFreq:
                    // increase shooting frequency
                    ballscript.throwCooldown /= 3f;
                    StartCoroutine(NormalizeShootFreq(5f));
                    _p1IsPowerUpOn = false;
                    break;
                case PowerUpType.Shield:
                    // activate shield - lass bälle abprallen
                    _p1IsPowerUpOn = false; // shield should only be instantiated once
                    break;
                case PowerUpType.DoubleScore:
                    // player scores double for certain time
                    game.scorePointsP1 *= 2;
                    StartCoroutine(NormalizeScoring(1, 5f));
                    _p1IsPowerUpOn = false;
                    break;
                default:
                    break;
            }
        }
        if (_p2IsPowerUpOn)
        {
            switch (_p2PowerUpType)
            {
                case PowerUpType.Freeze:
                    // freeze position of other player
                    rb1.constraints = RigidbodyConstraints.FreezePosition;
                    _audioManager.Play("Freeze");
                    StartCoroutine(Unfreeze(1, 5f));
                    _p2IsPowerUpOn = false;
                    break;
                case PowerUpType.ShootFreq:
                    ballscript.throwCooldown /= 3f;
                    StartCoroutine(NormalizeShootFreq(5f));
                    _p2IsPowerUpOn = false;
                    break;
                case PowerUpType.Shield:
                    //
                    _p2IsPowerUpOn = false;
                    break;
                case PowerUpType.DoubleScore:
                    // player scores double for certain time
                    game.scorePointsP2 *= 2;
                    StartCoroutine(NormalizeScoring(2, 5f));
                    _p2IsPowerUpOn = false;
                    break;
                default:
                    break;
            }
        }

    }
    
    
    public void ActivatePowerUp(PowerUpType type, int player)
    {
        if (player == 1)
        {
            _p1PowerUpType = type;
            _p1IsPowerUpOn = true;
        }
        if (player == 2)
        {
            _p2PowerUpType = type;
            _p2IsPowerUpOn = true;
        }
            
    }

    void OnGameStarted()
    {
        // ...
    }

    void OnGameOverConfirmed() 
    {
        // ...
    }

    void ScorePoint(string player)
    // add points for the player who scored
    {
        game.OnPlayerScored(player);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // if other object is a slowfield, player is slowed
        if (other.gameObject.tag == "slowfield")
        {
            moveSpeed = originalMoveSpeed * 0.6f;
        }
        
        // if other object is ball from the other player, other player scores and deactivate ball
        if (other.CompareTag(_otherBallName))
        {
            if (_otherBallName == "Ball1")
            {
                ScorePoint("Player1");
            }
            else if (_otherBallName == "Ball2")
            {
                ScorePoint("Player2");
            }
            _audioManager.Play("Punch");
            other.gameObject.SetActive(false); // deactivate ball
        }

        // OnPlayerDied();
        // OnPlayerScored();
    }
    
    
}
