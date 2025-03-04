using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class DestroyObj : MonoBehaviour
{
    private MeshRenderer mr;
    [SerializeField] private ParticleSystem boom;
    [SerializeField] private Collider col;
    [SerializeField] private SplineAnimate spline;
    
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        spline = GetComponent<SplineAnimate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var layer = LayerMask.NameToLayer("Bullet");
        if (other.gameObject.layer != layer) return;
        col.enabled = false;
        spline.enabled = false;
        StartCoroutine(WaitToDie());
        boom.Play();
        mr.enabled = false;
    }

    private IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(boom.main.duration);
        gameObject.SetActive(false);
    }

}
