using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    
    [SerializeField] private float _powerUpSpeed = 7f;
    
    void Update()
    {
        // move downwards
        transform.Translate(Time.deltaTime * _powerUpSpeed * Vector3.down);
        // set inactive once it reaches floor
        if (transform.position.y < 0f)
        {
            this.gameObject.SetActive(false); 
        }
    }
}
