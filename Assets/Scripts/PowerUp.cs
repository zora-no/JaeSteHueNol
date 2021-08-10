using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Freeze = 0,
    ShootFreq = 1,
    Shield = 2
}
public class PowerUp : MonoBehaviour
{
    
    [SerializeField] private float _powerUpSpeed = 7f;
    
    [SerializeField] private PowerUpType _type = PowerUpType.Freeze;
    [SerializeField] private bool _spawnRandomType = true;

    private float _yFloor = -10f; // y coordinate where the power ups disappear
    
    void Start()
    {
        if (_spawnRandomType)
        {
            _type = (PowerUpType) Random.Range(0, System.Enum.GetValues(typeof(PowerUpType)).Length);
        }
    }
    
    public PowerUpType GetType()
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
        if (other.CompareTag("Ball1"))
        {
            other.GetComponent<PlayerMovementScript>().ActivatePowerUp(_type, 1);
            this.gameObject.SetActive(false);
        }
        
        else if (other.CompareTag("Ball2"))
        {
            other.GetComponent<PlayerMovementScript>().ActivatePowerUp(_type, 2);
            this.gameObject.SetActive(false);
        }
    }
}
