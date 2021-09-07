using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // list with the transforms of the ceiling hatches 
    private Transform[] hatches;

    // power up 0: freeze enemy player
    private bool _freezeCanSpawn = true;
    [SerializeField] private float _freezeRate = 6f; // in seconds
    
    // power up 1: higher shooting frequency
    private bool _freqCanSpawn = true;
    [SerializeField] private float _freqRate = 6f;

    // power up 2: shield
    private bool _shieldCanSpawn = true;
    [SerializeField] private float _shieldRate = 6f;
    
    // power up 3: double score points 
    private bool _scoreCanSpawn = true;
    [SerializeField] private float _scoreRate = 6f;
    
    // power up 4: shield
    private bool _speedCanSpawn = true;
    [SerializeField] private float _speedRate = 6f;

    // tile power up 0: inhibitor
    private bool _tileInhibitorCanSpawn = true;
    [SerializeField] private float _tileInhibitorRate = 6f;

    // tile power up 1: slow
    private bool _tileSlowCanSpawn = true;
    [SerializeField] private float _tileSlowRate = 6f;

    // tile power up 2: clouded
    private bool _tileCloudedCanSpawn = true;
    [SerializeField] private float _tileCloudedRate = 6f;

    void Start()
    {
        // this array also includes the parent's transform at index 0 --> exclude it when spawning
        hatches = (Transform[]) GameObject.Find("ceiling_hatches").gameObject.GetComponentsInChildren<Transform>();
        
        StartCoroutine(SpawnRoutineFreeze());
        StartCoroutine(SpawnRoutineFrequency());
        StartCoroutine(SpawnRoutineScore());
        StartCoroutine(SpawnRoutineShield());
        StartCoroutine(SpawnRoutineSpeed());
        StartCoroutine(SpawnRoutineTileInhibitor());
        StartCoroutine(SpawnRoutineTileSlow());
        StartCoroutine(SpawnRoutineTileClouded());
    }

    public void activateSpawning()
    {
        _freezeCanSpawn = true;
        _freqCanSpawn = true;
        _shieldCanSpawn = true;
        _scoreCanSpawn = true;
        _speedCanSpawn = true;
    }

    public void deactivateSpawning()
    {
        _freezeCanSpawn = false;
        _freqCanSpawn = false;
        _shieldCanSpawn = false;
        _scoreCanSpawn = false;
        _speedCanSpawn = false;
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
    
    IEnumerator SpawnRoutineShield()
    {
        // while spawning is active, spawn a new double shield power up according to its spawn rate
        while (_shieldCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Shield PowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }
            
            yield return new WaitForSeconds(_shieldRate);
        }
    }
    
    IEnumerator SpawnRoutineSpeed()
    {
        // while spawning is active, spawn a new speed power up according to its spawn rate
        while (_speedCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Speed PowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }
            
            yield return new WaitForSeconds(_speedRate);
        }
    }

    IEnumerator SpawnRoutineTileInhibitor()
    {
        // while spawning is active, spawn a new inhibitor tile power up according to its spawn rate
        while (_tileInhibitorCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Inhibitor TilePowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }

            yield return new WaitForSeconds(_tileInhibitorRate);
        }
    }

    IEnumerator SpawnRoutineTileSlow()
    {
        // while spawning is active, spawn a new slow tile power up according to its spawn rate
        while (_tileSlowCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Slow TilePowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }

            yield return new WaitForSeconds(_tileSlowRate);
        }
    }

    IEnumerator SpawnRoutineTileClouded()
    {
        // while spawning is active, spawn a new clouded tile power up according to its spawn rate
        while (_tileCloudedCanSpawn)
        {
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects("Clouded TilePowerUp");
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }

            yield return new WaitForSeconds(_tileCloudedRate);
        }
    }

}
