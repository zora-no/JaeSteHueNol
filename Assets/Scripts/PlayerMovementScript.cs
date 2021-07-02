using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody rb;
    private float moveSpeed = 10.0f;
    private int playerNumber;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (this.name == "Player1")
        {
            playerNumber = 1;
;
        }
        else if (this.name == "Player2")
        {
            playerNumber = 2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
