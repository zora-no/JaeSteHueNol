using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private GameManager game;
    [SerializeField] private SpawnManager _spawnManager;
    private BallThrowScript ballscript;
    private AudioManager _audioManager;

    [SerializeField] private float _powerupTimeout = 5f;
    private bool _p1IsPowerUpOn = false;
    private PowerUpType _p1PowerUpType;
    private bool _p2IsPowerUpOn = false;
    private PowerUpType _p2PowerUpType;
    
    
    Rigidbody rb;
    Rigidbody rb1;
    Rigidbody rb2;
    private float moveSpeed = 10.0f;
    private int playerNumber;
    private string _otherBallName; // tag of the ball that can hit this player
    private int _otherPlayerNumber;


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
            _otherPlayerNumber = 2;

        }
        else if (this.name == "Player2")
        {
            playerNumber = 2;
            _otherBallName = "Ball1";
            _otherPlayerNumber = 1;
        }
        
        // rigidbodies of both players (for power ups)
        rb1 = GameObject.Find("Player1").GetComponent<Rigidbody>();
        rb2 = GameObject.Find("Player2").GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        PowerUpEffect();
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
            StartCoroutine(DeactivatePowerUp(1));
        }
        if (player == 2)
        {
            _p2PowerUpType = type;
            _p2IsPowerUpOn = true;
            StartCoroutine(DeactivatePowerUp(2));
        }
            
    }

    IEnumerator DeactivatePowerUp(int player)
    {
        if (player == 1)
        {
            yield return new WaitForSeconds(_powerupTimeout);
            _p1IsPowerUpOn = false;
        }
        if (player == 2)
        {
            yield return new WaitForSeconds(_powerupTimeout);
            _p2IsPowerUpOn = false;
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

    void ScorePoint()
    {
        if (playerNumber == 1)
        {
            game.OnPlayerScored(2); // if this player is hit, the other one scores 
        }
        else if (playerNumber == 2)
        {
            game.OnPlayerScored(2);
        }

        game.SetScoreText(); 
        
    }
    
    void OnTriggerEnter(Collider other)
    {
        // if other object is ball from the other player, damage/ destroy this player and deactivate ball
        if (other.CompareTag(_otherBallName))
        {
            ScorePoint();
            _audioManager.Play("Punch");
            other.gameObject.SetActive(false); // deactivate ball
        }

        // OnPlayerDied();
        // OnPlayerScored();
    }
    
    
}
