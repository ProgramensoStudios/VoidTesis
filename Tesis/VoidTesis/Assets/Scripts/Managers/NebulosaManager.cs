using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NebulosaManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enems;
    [SerializeField] private Material nebulosaMat;
    [SerializeField] private GameObject splines;
    [SerializeField] private int NebulosaId;
    private FilmGrain grain;
    private DepthOfField depth;
    [SerializeField] private Volume vol;

    private void Awake()
    {
        vol.profile.TryGet(out grain);
        vol.profile.TryGet(out depth);
    }


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
            grain.active = true;
            depth.active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
           splines.SetActive(false);
           grain.active = false;
           depth.active = false;
        }
    }

    public void RemoveEnem(GameObject enem)
    {
        enems.Remove(enem);
        if (enems.Count <= 0)
        {
            NebulosaCleared();
            grain.active = false;
           depth.active= false;
        }
    }

    private void NebulosaCleared()
    {
        Debug.Log("Cleared");
        var alpha = nebulosaMat.GetFloat("_Alpha");
        alpha = Mathf.Lerp(0.013f, 0f, 2f);
    }

}
