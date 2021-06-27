using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    [SerializeField] private float _powerUpSpeed = 7f;

    private float _yFloor = -10f; // y coordinate where the power ups disappear
    
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
}
