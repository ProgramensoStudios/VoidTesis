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

    // Inicializa el pool con el número de objetos definidos en poolLimit
    public void InitializePool()
    {
        for (int i = 0; i < poolLimit; i++)
        {
            GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            obj.SetActive(false); // Desactiva los objetos para que estén disponibles
            createdObjects.Add(obj);
        }
    }

    public GameObject AskForObject(Transform posToSpawn)
    {
        for (int i = 0; i < createdObjects.Count; i++)
        {
            // Limpia cualquier referencia nula (por si se destruyó un objeto)
            if (createdObjects[i] == null)
            {
                createdObjects.RemoveAt(i);
                i--; // Ajusta el índice después de eliminar
                continue;
            }

            // Si hay un objeto inactivo, lo reutiliza
            if (!createdObjects[i].activeInHierarchy)
            {
                createdObjects[i].transform.SetPositionAndRotation(posToSpawn.position, posToSpawn.rotation);
                createdObjects[i].SetActive(true);
                return createdObjects[i];
            }
        }

           GameObject newObject = Instantiate(prefab, posToSpawn.position, posToSpawn.rotation);
           createdObjects.Add(newObject);
           return newObject;
        
    }
}
