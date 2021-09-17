using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowScript : MonoBehaviour
{
    private AudioManager _audioManager;

    public float throwVelocity;
    private GameObject throwPoint;
    private GameObject ball;
    public float speedForward;
    public float speedUp;
    private int playerNumber;
    public float throwCooldown = 1.0f; // min. number of seconds between each throw
    private float timeTillThrow = 0.0f;
    private bool _canShoot = true;
    
    private Rigidbody rb; // rigidbody of the ball
    
    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();

        if (this.name == "Player1")
        {
            playerNumber = 1;
            throwPoint = gameObject.transform.Find("ThrowPoint1").gameObject;
            speedForward = -speedForward;
        }
        else if (this.name == "Player2")
        {
            playerNumber = 2;
            throwPoint = gameObject.transform.Find("ThrowPoint2").gameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {   
        // Checks if player is able to shoot
        if (_canShoot)
        {   
            // checks for player and if enough time has passed since last throw
            if ((playerNumber == 1) && Time.time > timeTillThrow)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ball = ObjectPool.SharedInstance.GetPooledObjects("Ball1");
                    if (ball != null)
                    {
                        ball.SetActive(true);
                    }
                    rb = ball.GetComponent<Rigidbody>();
                    Throw();
                    timeTillThrow = Time.time + throwCooldown;
                }
            }
            // checks for player and if enough time has passed since last throw
            else if ((playerNumber == 2) && Time.time > timeTillThrow)
            {
                if (Input.GetKeyDown(KeyCode.Keypad0))
                    //if (Input.GetKeyDown(KeyCode.P))
                {
                    ball = ObjectPool.SharedInstance.GetPooledObjects("Ball2");
                    if (ball != null)
                    {
                        ball.SetActive(true);
                    }
                    rb = ball.GetComponent<Rigidbody>();
                    Throw();
                    timeTillThrow = Time.time + throwCooldown;
                }
            }
        }
        
        // set ball inactive once it left arena 

        if (this.name == "Player1")
        {
            List<GameObject> activeBalls1 = ObjectPool.SharedInstance.GetActivePooledObjects("Ball1");
            foreach (GameObject ball1 in activeBalls1) {
                if (ball1.transform.position.z < -36f)
                {
                    ball1.SetActive(false); 
                }
            }
        }

        if (this.name == "Player2")
        {
            List<GameObject> activeBalls2 = ObjectPool.SharedInstance.GetActivePooledObjects("Ball2");
                
            foreach (GameObject ball2 in activeBalls2) {
                if (ball2.transform.position.z > 30f)
                {
                    ball2.SetActive(false); 
                }
            }
        }

    }

    void Throw()
    {
        ball.transform.position = throwPoint.transform.position;
        rb.velocity = new Vector3(0.0f, speedUp, speedForward).normalized * throwVelocity;
        _audioManager.Play("Shot");
    }

    public void activateShooting()
    {
        _canShoot = true;
    }

    public void deactivateShooting()
    {
        _canShoot = false;
    }
    
}
