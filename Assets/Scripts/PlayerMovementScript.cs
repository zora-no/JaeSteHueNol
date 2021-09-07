using System;
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
    
    public GameObject shieldPrefab1;
    public GameObject shieldPrefab2;

    private bool _p1IsPowerUpOn = false;
    private string _p1PowerUpType;
    private bool _p2IsPowerUpOn = false;
    private string _p2PowerUpType;
    public int tileEffectType = 3;
    
    
    Rigidbody rb;
    Rigidbody rb1;
    Rigidbody rb2;
    private float originalMoveSpeed = 10.0f;
    private float moveSpeed = 10.0f;
    private int playerNumber;
    private string _otherBallName; // tag of the ball that can hit this player


    private void Awake()
    {
        // null reference check for prefabs
        if (shieldPrefab1 == null)
        {
            Debug.LogError("Shield1 Prefab is missing!");
        }
        if (shieldPrefab2 == null)
        {
            Debug.LogError("Shield2 Prefab is missing!");
        }
    }

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

        resetPosition();
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
        if (other.gameObject.CompareTag("slowfield"))
        {
            ResetMovespeed();
        }
        if (other.gameObject.CompareTag("cloudedviewfield"))
        {
            ResetVision();
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
    // Resets movespeed to hardcoded original movespeed
    public void ResetMovespeed()
    {
        moveSpeed = originalMoveSpeed;
    }
    // Activates vision impairment
    public void ActivateVisionImpairment()
    {
        transform.Find("PlayerCamera").transform.Find("Vision Impairment").gameObject.SetActive(true);
    }

    // Deactivates vision impairment
    public void ResetVision()
    {
        transform.Find("PlayerCamera").transform.Find("Vision Impairment").gameObject.SetActive(false);
    }

    public void ResetTileEffectType()
    {
        tileEffectType = 3;
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
    
    ////////// PowerUp Stuff START //////////
    
    
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
    
    IEnumerator NormalizeSpeed(int player, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        moveSpeed = originalMoveSpeed;
    }

    IEnumerator RemoveShield(int player, float delayTime, GameObject shield)
    {
        yield return new WaitForSeconds(delayTime);
        shield.gameObject.SetActive(false);
    }
    
    void PowerUpEffect()
    {
        if (_p1IsPowerUpOn)
        {
            _p1IsPowerUpOn = false;
            switch (_p1PowerUpType)
            {
                case "Freeze PowerUp":
                    // fix position of other player
                    rb2.constraints = RigidbodyConstraints.FreezePosition;
                    _audioManager.Play("Freeze");
                    StartCoroutine(Unfreeze(2, 5f));
                    break;
                case "Frequency PowerUp":
                    // increase shooting frequency
                    ballscript.throwCooldown /= 3f;
                    StartCoroutine(NormalizeShootFreq(5f));
                    break;
                case "Shield PowerUp":
                    // activate shield
                    GameObject shieldObject = ObjectPool.SharedInstance.GetPooledObjects("Shield1");
                    shieldObject.SetActive(true);
                    shieldObject.transform.position = new Vector3(this.gameObject.transform.position.x,
                        this.gameObject.transform.position.y,
                        this.gameObject.transform.position.z + shieldObject.GetComponent<Shield>().zDiff);
                    StartCoroutine(RemoveShield(1, 5f, shieldObject));
                    break;
                case "Score PowerUp":
                    // player scores double for certain time
                    game.scorePointsP1 *= 2;
                    StartCoroutine(NormalizeScoring(1, 5f));
                    break;
                case "Speed PowerUp":
                    moveSpeed *= 2;
                    StartCoroutine(NormalizeSpeed(1, 5f));
                    break;
                case "Inhibitor TilePowerUp":
                    tileEffectType = 0;
                    break;
                case "Slow TilePowerUp":
                    tileEffectType = 1;
                    break;
                case "Clouded TilePowerUp":
                    tileEffectType = 2;
                    break;
                default:
                    break;
            }
        }
        if (_p2IsPowerUpOn)
        {
            _p2IsPowerUpOn = false;
            switch (_p2PowerUpType)
            {
                case "Freeze PowerUp":
                    // freeze position of other player
                    rb1.constraints = RigidbodyConstraints.FreezePosition;
                    _audioManager.Play("Freeze");
                    StartCoroutine(Unfreeze(1, 5f));
                    break;
                case "Frequency PowerUp":
                    ballscript.throwCooldown /= 3f;
                    StartCoroutine(NormalizeShootFreq(5f));
                    break;
                case "Shield PowerUp":
                    // activate shield - lass bälle abprallen
                    GameObject shieldObject = ObjectPool.SharedInstance.GetPooledObjects("Shield2");
                    shieldObject.SetActive(true);
                    shieldObject.transform.position = new Vector3(this.gameObject.transform.position.x,
                        this.gameObject.transform.position.y,
                        this.gameObject.transform.position.z + shieldObject.GetComponent<Shield>().zDiff);
                    StartCoroutine(RemoveShield(1, 5f, shieldObject));
                    break;
                case "Score PowerUp":
                    // player scores double for certain time
                    game.scorePointsP2 *= 2;
                    StartCoroutine(NormalizeScoring(2, 5f));
                    break;
                case "Speed PowerUp":
                    moveSpeed *= 2;
                    StartCoroutine(NormalizeSpeed(1, 5f));
                    break;
                default:
                    break;
            }
        }

    }
    
    
    public void ActivatePowerUp(string type, int player)
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
    ////////// PowerUp Stuff END //////////

    void resetPosition()
    {
        if (this.name == "Player1")
        {
            this.gameObject.transform.position = new Vector3(0,27.37f,8.5f);
        }
        else if (this.name == "Player2")
        {
            this.gameObject.transform.position = new Vector3(0f,27.37f,-31.12f);
        }
    }
    
    void OnGameStarted()
    { 
        // unfreeze player
       rb.constraints = RigidbodyConstraints.None;
       // allow shooting
       ballscript.activateShooting();
       // reset position
       resetPosition();
    }

    void OnGameOverConfirmed() 
    {
        // freeze player
        rb.constraints = RigidbodyConstraints.FreezePosition;
        // stop shooting
        ballscript.deactivateShooting();
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

        // if other object is a vision impairment field, player is visually impaired
        if (other.gameObject.tag == "cloudedviewfield")
        {
            ActivateVisionImpairment();
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
        
    }
    
    
}
