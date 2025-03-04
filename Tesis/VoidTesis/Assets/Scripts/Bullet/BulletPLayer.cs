using System;
using System.Collections;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private FollowReticle reticule;

    private Transform target;
    private Vector3 fixedDirection; // Dirección fija en caso de que no haya objetivo

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Awake()
    {
        reticule = FindAnyObjectByType<FollowReticle>();
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = fixedDirection * speed; // Usa la dirección almacenada
        }
    }

    private void OnEnable()
    {
        if (reticule.objectInSight != null)
        {
            SetTarget(reticule.objectInSight.transform);
        }
        else if (reticule.defaultTarget != null)
        {
            fixedDirection = (reticule.defaultTarget.position - transform.position).normalized; // Guarda la dirección inicial
            target = null; // No hay target, solo va en esa dirección
        }
        else
        {
            fixedDirection = transform.forward; // Dirección por defecto si no hay nada
        }

        StartCoroutine(KillBullet());
    }

    private IEnumerator KillBullet()
    {
        yield return new WaitForEndOfFrame();
        trailRenderer.enabled = true;
        yield return new WaitForSeconds(lifetime / 3f);
        trailRenderer.enabled = false;
        yield return new WaitForSeconds(lifetime / 2);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
       // gameObject.SetActive(false);
    }

    private void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
