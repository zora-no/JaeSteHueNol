using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // list with the transforms of the ceiling hatches 
    private Transform[] hatches;

    private bool _spawningOn = false;
    [SerializeField] private int _powerUpRate = 2;
    [SerializeField] private int _firstWait = 5; // how long until first power up spawns

    private int nTags = 8;

    private string[] tags = new string[]
    {
        "Freeze PowerUp", "Frequency PowerUp", "Score PowerUp",
        "Shield PowerUp", "Speed PowerUp", "Inhibitor TilePowerUp",
        "Slow TilePowerUp", "Cloud TilePowerUp"
    };

    void Start()
    {
        // this array also includes the parent's transform at index 0 --> exclude it when spawning
        hatches = (Transform[]) GameObject.Find("ceiling_hatches").gameObject.GetComponentsInChildren<Transform>();

        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnPowerup()
    {
        // wait X seconds until first power up is spawned
        yield return new WaitForSeconds(_firstWait);

        // while spawning is active, spawn a random new power up according to spawn rate
        while (_spawningOn)
        {
            string thisTag = tags[Random.Range(0, nTags)];
            GameObject newPowerUp = ObjectPool.SharedInstance.GetPooledObjects(thisTag);
            if (newPowerUp != null)
            {
                newPowerUp.transform.position = hatches[Random.Range(1, hatches.Length)].position;
                newPowerUp.transform.rotation = Quaternion.identity;
                newPowerUp.SetActive(true);
            }

            yield return new WaitForSeconds(_powerUpRate);
        }

    }

    public void activateSpawning()
    {
        _spawningOn = true;
    }

    public void deactivateSpawning()
    {
        _spawningOn = false;
    }
    
}
    