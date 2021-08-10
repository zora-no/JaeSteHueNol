using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private GameManager game;
    
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;
    
    
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


    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.Instance;
        
        rb = GetComponent<Rigidbody>(); // rb of this player
        if (this.name == "Player1")
        {
            playerNumber = 1;
        }
        else if (this.name == "Player2")
        {
            playerNumber = 2;
        }
        
        // rigidbodies of both players (for power ups)
        rb1 = GameObject.Find("Player1").GetComponent<Rigidbody>();
        rb2 = GameObject.Find("Player1").GetComponent<Rigidbody>();
        
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
    
    void PowerUpEffect()
    {
        if (_p1IsPowerUpOn)
        {
            switch (_p1PowerUpType)
            {
                case PowerUpType.Freeze:
                    // fix position of other player
                    rb2.constraints = RigidbodyConstraints.FreezePosition;
                    break;
                case PowerUpType.ShootFreq:
                    // increase shooting frequency
                    break;
                case PowerUpType.Shield:
                    // activate shield - lass bälle abprallen
                    _p1IsPowerUpOn = false; // shield should only be instantiated once
                    break;
                default:
                    break;
            }
        }
        else
        {
            // unfreeze player
            rb2.constraints = RigidbodyConstraints.None;
        }
        if (_p2IsPowerUpOn)
        {
            switch (_p2PowerUpType)
            {
                case PowerUpType.Freeze:
                    // freeze position of other player
                    rb1.constraints = RigidbodyConstraints.FreezePosition;
                    break;
                case PowerUpType.ShootFreq:
                    //
                    break;
                case PowerUpType.Shield:
                    //
                    _p2IsPowerUpOn = false;
                    break;
                default:
                    break;
            }
        }
        else
        {
            // unfreeze player
            rb1.constraints = RigidbodyConstraints.None;
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
    
    void OnTriggerEnter(Collider other)
        // check for collision with other game objects
    {
        OnPlayerDied();
        OnPlayerScored();
    }
    
    
}
