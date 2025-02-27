using System.Collections;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
      
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }
    private void OnEnable()
    {
        StartCoroutine(RoutineYaPorFavor());
    }

    IEnumerator RoutineYaPorFavor()
    {

        yield return new WaitForEndOfFrame();
        trailRenderer.enabled = true;
        yield return new WaitForSeconds(lifetime/3f);
        trailRenderer.enabled = false;
        yield return new WaitForSeconds(lifetime / 2);
        gameObject.SetActive(false);
        
    }

}
