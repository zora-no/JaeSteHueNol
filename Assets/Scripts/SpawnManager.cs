using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // common variables
    
    //private float[] _xSpawn = {-12f, 13f}; // x spawn range
    //private float[] _zSpawn = {-18f, 9f}; // z spawn range
    private float _ySpawn = 45f; // y spawn point: arena height
    
    private float[] _xSpawn = {1f, 2f, 3f, 4f, 5f, 6f, 7f};
    private float[] _zSpawn = {1f, 2f, 3f, 4f, 5f};
    
    
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
                newPowerUp.transform.position = new Vector3 (
                    //Random.Range(_xSpawn[0], _xSpawn[1]),
                    _xSpawn[Random.Range(0,_xSpawn.Length)],
                    
                    _ySpawn, 
                    
                    //Random.Range(_zSpawn[0], _zSpawn[1]) );
                    _zSpawn[Random.Range(0,_zSpawn.Length)] );
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
                newPowerUp.transform.position = new Vector3 (
                    //Random.Range(_xSpawn[0], _xSpawn[1]),
                    _xSpawn[Random.Range(0,_xSpawn.Length)],
                    
                    _ySpawn, 
                    
                    //Random.Range(_zSpawn[0], _zSpawn[1]) );
                    _zSpawn[Random.Range(0,_zSpawn.Length)] );
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
                newPowerUp.transform.position = new Vector3 (
                    //Random.Range(_xSpawn[0], _xSpawn[1]),
                    _xSpawn[Random.Range(0,_xSpawn.Length)],
                    
                    _ySpawn, 
                    
                    //Random.Range(_zSpawn[0], _zSpawn[1]) );
                    _zSpawn[Random.Range(0,_zSpawn.Length)] );
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }
            
            yield return new WaitForSeconds(_scoreRate);
        }
    }
    
}
