using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    
    [SerializeField] private float _powerUpSpeed = 7f;
    
    [SerializeField] private string _type;
    
    private float _yFloor = -10f; // y coordinate where the power ups disappear

    private GameObject player1;
    private GameObject player2;
    
    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");

        _type = this.gameObject.tag;
    }
    
    void Update()
    {
        // move downwards
        transform.Translate(Time.deltaTime * _powerUpSpeed * Vector3.down);
        // set inactive once it reaches floor
        if (transform.position.y < _yFloor)
        {
            this.gameObject.SetActive(false); 
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // if it collides with ball from player, set power up and ball inactive & activate power up
        if (other.CompareTag("Ball1"))
        {
            if (other.gameObject != null)
            {
                player1.GetComponent<PlayerMovementScript>().ActivatePowerUp(_type, 1);
                gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }
            
        }
        else if (other.CompareTag("Ball2"))
        {
            if (other.gameObject != null)
            {
                player2.GetComponent<PlayerMovementScript>().ActivatePowerUp(_type, 2);
                gameObject.SetActive(false);
                other.gameObject.SetActive(false);
            }
            
        }

    }
}
