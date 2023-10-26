using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float waypointSize = 1f;

    public WaypointInfo GetNextWaypoint(WaypointInfo currentWaypoint)
    {
        if (currentWaypoint is null)
            return transform.GetChild(0).GetComponent<WaypointInfo>();

        List<WaypointInfo> possibleWaypoints = currentWaypoint.GetConnectedWaypoints();

        return possibleWaypoints[Random.Range(0,possibleWaypoints.Count)];
    }
}
