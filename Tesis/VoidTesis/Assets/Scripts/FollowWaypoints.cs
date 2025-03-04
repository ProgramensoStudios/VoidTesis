using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] public float speed = 5f;
    [SerializeField] public float currentSpeed;

    private Queue<Transform> _waypointQueue = new Queue<Transform>();
    private Transform currentTarget;

    private void Awake()
    {
        var wpCount = GameObject.Find("WayPoints").transform.childCount;
        for (var indexChild = 0; indexChild < wpCount; indexChild++)
        {
            var child = GameObject.Find("WayPoints").transform.GetChild(indexChild);
            waypoints.Add(child);
        }

        currentSpeed = speed;
    }

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
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, currentSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            _waypointQueue.Enqueue(currentTarget);
            currentTarget = _waypointQueue.Dequeue();
        }
    }
}