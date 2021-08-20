using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowScript : MonoBehaviour
{

    public float throwVelocity;
    private GameObject throwPoint;
    private GameObject ball;
    public float speedForward;
    public float speedUp;
    private int playerNumber;
    public float throwCooldown = 1.0f; // min. number of seconds between each throw
    private float timeTillThrow = 0.0f;
    
    private Rigidbody rb; // rigidbody of the ball
    
    private float _zDisappear; // z coordinate where the balls disappear

    void Start()
    {

        if (this.name == "Player1")
        {
            playerNumber = 1;
            throwPoint = gameObject.transform.Find("ThrowPoint1").gameObject;
            speedForward = -speedForward;
            _zDisappear = -45f; // behind player 2
        }
        else if (this.name == "Player2")
        {
            playerNumber = 2;
            throwPoint = gameObject.transform.Find("ThrowPoint2").gameObject;
            _zDisappear = 35f; // behind player 1
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerNumber == 1) && Time.time > timeTillThrow)
        {
            //if (Input.GetKey(KeyCode.Space))
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ball = ObjectPool.SharedInstance.GetPooledObjects("Ball1");
                if (ball != null)
                {
                    ball.SetActive(true);
                }
                rb = ball.GetComponent<Rigidbody>();
                Throw();
                FindObjectOfType<AudioManager>().Play("Shot");
                timeTillThrow = Time.time + throwCooldown;
            }
        }
        else if ((playerNumber == 2) && Time.time > timeTillThrow)
        {
            //if (Input.GetKey(KeyCode.Keypad0))
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                ball = ObjectPool.SharedInstance.GetPooledObjects("Ball2");

                if (ball != null)
                {
                    ball.SetActive(true);
                }
                rb = ball.GetComponent<Rigidbody>();
                Throw();
                FindObjectOfType<AudioManager>().Play("Shot");
                timeTillThrow = Time.time + throwCooldown;
            }
        }

        // set ball inactive once it left arena 
        if (ball != null)
        {
            if (this.name == "Player1")
            {
                if (ball.transform.position.z < _zDisappear)
                {
                    ball.SetActive(false); 
                }
            }
            else if (this.name == "Player2")
            {
                if (ball.transform.position.z > _zDisappear)
                {
                    ball.SetActive(false); 
                }
            }
        }

    }

    void Throw()
    {
        ball.transform.position = throwPoint.transform.position;
        rb.velocity = new Vector3(0.0f, speedUp, speedForward).normalized * throwVelocity;
    }
    
}
