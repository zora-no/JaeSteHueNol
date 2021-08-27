using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Freeze = 0,
    ShootFreq = 1,
    Shield = 2,
    DoubleScore = 3
}
public class PowerUp : MonoBehaviour
{
    
    [SerializeField] private float _powerUpSpeed = 7f;
    
    [SerializeField] private PowerUpType _type = PowerUpType.Freeze;
    [SerializeField] private bool _spawnRandomType = true;

    private float _yFloor = -10f; // y coordinate where the power ups disappear

    private GameObject player1;
    private GameObject player2;
    
    void Start()
    {
        player1 = GameObject.FindWithTag("Player1");
        player2 = GameObject.FindWithTag("Player2");
        
        if (_spawnRandomType)
        {
            _type = (PowerUpType) Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
        }
    }
    
    public PowerUpType GetPowerUpType()
    {
        return _type;
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
