using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointInfo : MonoBehaviour
{
    private WaypointManager waypointManager;
    private List<WaypointInfo> attachedWaypoints = new List<WaypointInfo>();

    private void Awake()
    {
        waypointManager = transform.parent.GetComponent<WaypointManager>();
        
        foreach(Transform t in waypointManager.transform)
        {
            float distance = Mathf.Sqrt(Mathf.Pow(t.position.x - transform.position.x, 2) + Mathf.Pow(t.position.z - transform.position.z, 2));
            if (distance < 27 && t.GetSiblingIndex() != transform.GetSiblingIndex())
                attachedWaypoints.Add(t.GetComponent<WaypointInfo>());
        }
    }

    public List<WaypointInfo> GetConnectedWaypoints() { return attachedWaypoints; }

    public List<WaypointInfo> GetConnectedWaypointsShuffled() { return attachedWaypoints.OrderBy((item) => UnityEngine.Random.Range(0,999)).ToList(); }

    public WaypointInfo GetConnectedWaypoint(int index) { return attachedWaypoints[index]; }
    public int GetIndex() { return transform.GetSiblingIndex(); }
}
