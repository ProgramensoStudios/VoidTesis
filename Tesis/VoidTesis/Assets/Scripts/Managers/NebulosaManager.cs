using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NebulosaManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enems;
    [SerializeField] private Material nebulosaMat;
    [SerializeField] private GameObject splines;
    [SerializeField] private int NebulosaId;


    private void OnEnable()
    {
        DestroyObj.OnDeath += RemoveEnem;
    }
    private void OnDisable()
    {
        DestroyObj.OnDeath -= RemoveEnem;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            splines.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
           splines.SetActive(false);
        }
    }

    public void RemoveEnem(GameObject enem)
    {
        enems.Remove(enem);
        if (enems.Count <= 0)
        {
            NebulosaCleared();
        }
    }

    private void NebulosaCleared()
    {
        Debug.Log("Cleared");
        var alpha = nebulosaMat.GetFloat("_Alpha");
        alpha = Mathf.Lerp(0.013f, 0f, 2f);
    }

}
