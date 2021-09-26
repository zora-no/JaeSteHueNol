using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public int rotationSpeed = 5;
    
    // rotates object script is attached to
    void Update()
    {
        
        transform.Rotate(Vector3.right * 5 *rotationSpeed * Time.deltaTime);
    }
}
