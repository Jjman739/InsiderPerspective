using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Transform waypointsObject;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceThreshold = 0.1f;
    private WaypointInfo currentWaypoint;
    private List<WaypointInfo> waypoints = new List<WaypointInfo>();
    private WaypointManager waypointManager;

    // Start is called before the first frame update
    void Start()
    {
        waypointManager = waypointsObject.GetComponent<WaypointManager>();
        foreach (Transform t in waypointsObject)
        {
            waypoints.Add(t.GetComponent<WaypointInfo>());
        }

        currentWaypoint = waypointManager.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.transform.position;

        currentWaypoint = waypointManager.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < distanceThreshold)
        {
            currentWaypoint = waypointManager.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint.transform);
        }
    }
}
