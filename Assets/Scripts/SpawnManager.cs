using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // list with the transforms of the ceiling hatches 
    private Transform[] hatches;

    // power up 0: freeze enemy player
    private bool _freezeCanSpawn = true;
    [SerializeField] private float _freezeRate = 5f; // in seconds
    
    // power up 1: higher shooting frequency
    private bool _freqCanSpawn = true;
    [SerializeField] private float _freqRate = 5f;

    // power up 2: shield
    
    // power up 3: double score points 
    private bool _scoreCanSpawn = true;
    [SerializeField] private float _scoreRate = 5f;
    
    void Start()
    {
        // this also includes the parent's transform at index 0
        hatches = (Transform[]) GameObject.Find("ceiling_hatches").gameObject.GetComponentsInChildren<Transform>();
        
        StartCoroutine(SpawnRoutineFreeze());
        StartCoroutine(SpawnRoutineFrequency());
        StartCoroutine(SpawnRoutineScore());
    }
    
    
    IEnumerator SpawnRoutineFreeze()
    {
        // while spawning is active, spawn a new freeze power up according to its spawn rate
        while (_freezeCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Freeze PowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
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
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }
            
            yield return new WaitForSeconds(_freqRate);
        }
    }
    
    IEnumerator SpawnRoutineScore()
    {
        // while spawning is active, spawn a new double score power up according to its spawn rate
        while (_scoreCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Score PowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }
            
            yield return new WaitForSeconds(_scoreRate);
        }
    }
    
}
