using UnityEngine;

public class FollowReticle : MonoBehaviour
{
    public float rayLength = 5f;
    public LayerMask hitLayers;

    void Update()
    {
        Vector3 direction = transform.forward;
        Debug.DrawRay(transform.position, direction * rayLength, Color.yellow);
        
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, rayLength, hitLayers))
        {
            
           // Debug.Log("Raycast golpeó: " + hit.collider.name);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 direction = transform.forward;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, direction * rayLength);
    }
}
