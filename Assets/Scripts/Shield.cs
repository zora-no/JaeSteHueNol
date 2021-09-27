using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private GameObject player;
    public float zDiff; // how far away from player
    private string otherBallName;
    
    void Start()
    {
        // initialize variables
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
        // move shield with player
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
            if (other.gameObject.tag == otherBallName) // if a ball from the opponent hits the shield
            {
                // destroy the opponent's ball and play sound
                other.gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("Barrier");
            }
        }
    }
}
