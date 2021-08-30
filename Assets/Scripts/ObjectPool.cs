using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem {
    public int amountToPool;
    public GameObject objectToPool;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    
    void Awake()
    {
        SharedInstance = this;
        
        // null reference check for prefabs
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool == null)
                Debug.LogError("ObjectToPool Prefab is missing!");
        }

    }

    void Start()
    {
        // create list including all the pooled objects and set them inactive
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool) {
            for (int i = 0; i < item.amountToPool; i++) {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.transform.parent = GameObject.Find("SpawnManager").transform;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObjects(string tag)
    {
        // depending on tag, get an object from the pool that is inactive and can be used
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag) {
                return pooledObjects[i];
            }
        }
        
        return null;
    }

    public List<GameObject> GetActivePooledObjects(string tag)
    {
        List<GameObject> activeObjects = new List<GameObject>();
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].tag == tag && pooledObjects[i].activeInHierarchy)
            {
                activeObjects.Add(pooledObjects[i]);
            }
        }

        return activeObjects;
    }

}
