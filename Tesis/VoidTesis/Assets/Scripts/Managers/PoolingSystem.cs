using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> createdObjects;
    public int poolLimit = 10;

    private void Start()
    {
        createdObjects = new List<GameObject>();
        InitializePool();
    }
    
    private void InitializePool()
    {
        for (int i = 0; i < poolLimit; i++)
        {
            var obj = Instantiate(prefab, transform.position, Quaternion.identity);
            obj.SetActive(false);
            createdObjects.Add(obj);
        }
    }

    public GameObject AskForObject(Transform posToSpawn)
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            if (!createdObjects[i].activeInHierarchy)
            {
                createdObjects[i].transform.SetPositionAndRotation(posToSpawn.position, posToSpawn.rotation);
                createdObjects[i].SetActive(true);
                return createdObjects[i];
            }
        }
        var newObject = Instantiate(prefab, posToSpawn.position, posToSpawn.rotation);
        createdObjects.Add(newObject);
        return newObject;
        
    }
}
