using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    [SerializeField] private Transform waypointsObject;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceThreshold = 0.1f;
    private WaypointInfo lastWaypoint;
    private WaypointInfo nextWaypoint;
    private WaypointInfo overrideTarget;
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

        nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
        transform.position = nextWaypoint.transform.position;

        lastWaypoint = nextWaypoint;

        nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
        transform.LookAt(nextWaypoint.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextWaypoint.transform.position) < distanceThreshold)
        {
            lastWaypoint = nextWaypoint;

            if (overrideTarget is not null && lastWaypoint.transform.GetSiblingIndex() == overrideTarget.transform.GetSiblingIndex())
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

    public WaypointInfo GetLastWaypoint() { return lastWaypoint; }

    public WaypointInfo GetOverrideTarget() { return overrideTarget; }
}
