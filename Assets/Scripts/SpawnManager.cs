using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // power up 1: freeze enemy player
    private bool _freezeCanSpawn = true;
    private float _freezeX = 4.5f; // x spawn range
    private float _freezeZ = 4.5f; // z spawn range
    [SerializeField] private float _freezeRate = 5f; // in seconds
    
    // power up 2: higher shooting frequency
    private bool _freqCanSpawn = true;
    private float _freqX = 4.5f;
    private float _freqZ = 4.5f; 
    [SerializeField] private float _freqRate = 5f;

    
    void Start()
    {
        StartCoroutine(SpawnRoutineFreeze());
        StartCoroutine(SpawnRoutineFrequency());
    }


    IEnumerator SpawnRoutineFreeze()
    {
        // while spawning is active, spawn a new freeze power up according to its spawn rate
        while (_freezeCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Freeze PowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = new Vector3(
                    Random.Range(-_freezeX, _freezeX),
                    10, 
                    Random.Range(-_freezeZ, _freezeZ));
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }
            
            yield return new WaitForSeconds(_freezeRate);
        }
    }

    IEnumerator SpawnRoutineFrequency()
    {
        // while spawning is active, spawn a new frequency power up according to its spawn rate
        while (_freqCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Frequency PowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = new Vector3(
                    Random.Range(-_freqX, _freqX),
                    10, 
                    Random.Range(-_freqZ, _freqZ));
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }
            
            yield return new WaitForSeconds(_freqRate);
        }
    }
    
}
