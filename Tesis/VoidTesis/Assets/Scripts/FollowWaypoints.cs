using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float speed = 5f;

    private Queue<Transform> _waypointQueue = new Queue<Transform>();
    private Transform currentTarget;

    private void Start()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints assigned!");
            return;
        }
        foreach (Transform wp in waypoints)
        {
            _waypointQueue.Enqueue(wp);
        }
        currentTarget = _waypointQueue.Dequeue();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            _waypointQueue.Enqueue(currentTarget);
            currentTarget = _waypointQueue.Dequeue();
        }
    }
}