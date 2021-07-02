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
    private string throwButton;
    private int playerNumber;
    private float throwCooldown = 2.0f;
    private float timeTillThrow = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "Player1")
        {
            playerNumber = 1;
            throwPoint = gameObject.transform.Find("ThrowPoint1").gameObject;
            ball = GameObject.Find("Ball1");
            speedForward = -speedForward;
        }
        else if (this.name == "Player2")
        {
            playerNumber = 2;
            throwPoint = gameObject.transform.Find("ThrowPoint2").gameObject;
            ball = GameObject.Find("Ball2");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((playerNumber == 1) && Time.time > timeTillThrow)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Throw();
                timeTillThrow = Time.time + throwCooldown;
            }
        }
        else if ((playerNumber == 2) && Time.time > timeTillThrow)
        {
            if (Input.GetKey(KeyCode.Keypad0))
            {
                Throw();
                timeTillThrow = Time.time + throwCooldown;
            }
        }
    }

    void Throw()
    {
        ball.transform.position = throwPoint.transform.position;
        ball.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, speedUp, speedForward).normalized * throwVelocity;
    }
}
