using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private GameObject player;
    public float zDiff;
    private string otherBallName;
    
    void Start()
    {
        if (this.gameObject.CompareTag("Shield1"))
        {
            player = GameObject.FindWithTag("Player1");
            zDiff = -5f;
            otherBallName = "Ball2";
        }
        else if (this.gameObject.CompareTag("Shield2"))
        {
            player = GameObject.FindWithTag("Player2");
            zDiff = 5f;
            otherBallName = "Ball1";
        }
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        this.gameObject.transform.position = new Vector3(
            playerPosition.x,
            playerPosition.y,
            playerPosition.z + zDiff);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.tag == otherBallName) // if a ball from the other player hits
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
