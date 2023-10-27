using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class WaypointMover : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceThreshold = 0.1f;
    private WaypointInfo lastWaypoint;
    private WaypointInfo nextWaypoint;
    private WaypointInfo overrideTarget;
    private List<WaypointInfo> waypoints = new List<WaypointInfo>();
    private WaypointManager waypointManager;
    private Transform waypointsObject;

    private bool initialized = false;

    // Start is called before the first frame update
    public void Initialize()
    {
        foreach (Transform t in waypointsObject)
        {
            waypoints.Add(t.GetComponent<WaypointInfo>());
        }

        nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
        transform.position = nextWaypoint.transform.position;

        lastWaypoint = nextWaypoint;

        nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
        transform.LookAt(nextWaypoint.transform);

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized) return;

        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextWaypoint.transform.position) < distanceThreshold)
        {
            lastWaypoint = nextWaypoint;
            
            if (overrideTarget is not null && lastWaypoint.GetIndex() == overrideTarget.GetIndex())
            {
                overrideTarget = null;
            }

            if (overrideTarget is null)
                nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
            else
                nextWaypoint = waypointManager.GetNextWaypointWithOverride(nextWaypoint, overrideTarget);
            transform.LookAt(nextWaypoint.transform);
        }
    }

    public void SetOverrideTarget(int targetIndex)
    {
        overrideTarget = waypointManager.GetWaypointByIndex(targetIndex);
    }

    public void SetWaypointManager(WaypointManager manager) { waypointManager = manager; }

    public void SetWaypoints(Transform obj) { waypointsObject = obj; }
    public WaypointInfo GetLastWaypoint() { return lastWaypoint; }
    public WaypointInfo GetOverrideTarget() { return overrideTarget; }
}
