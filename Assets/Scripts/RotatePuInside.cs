using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RotatePuInside : MonoBehaviour
{

    public float turnSpeed = 50f;
    
    // rotates inside of PU
    void Update()
    {
        transform.Rotate(Vector3.forward,turnSpeed * Time.deltaTime);
    }
}
