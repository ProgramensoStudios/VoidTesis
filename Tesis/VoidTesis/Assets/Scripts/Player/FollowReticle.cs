using UnityEngine;
using UnityEngine.UI;

public class FollowReticle : MonoBehaviour
{
    public float rayLength = 5f;
    public GameObject objectInSight;
    [SerializeField] private Image reticule;
    public LayerMask hitLayers;
    public Transform defaultTarget;

    void Update()
    {
        var direction = transform.forward;
        Debug.DrawRay(transform.position, direction * rayLength, Color.yellow);
        
        const int excludedLayer = 9;
        LayerMask filteredLayers = hitLayers & ~(1 << excludedLayer);
        
        objectInSight = null;
        reticule.color = Color.white;
        
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, rayLength, filteredLayers, QueryTriggerInteraction.Collide))
        {
            objectInSight = hit.transform.gameObject;
            
            if (objectInSight != null)
            {
                reticule.color = Color.red;
            }
        }
    }

    private void OnDrawGizmos()
    {
        var direction = transform.forward;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, direction * rayLength);
    }
}