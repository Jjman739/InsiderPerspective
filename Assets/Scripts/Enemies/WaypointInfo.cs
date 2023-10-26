using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointInfo : MonoBehaviour
{
    private WaypointManager waypointManager;
    private List<WaypointInfo> attachedWaypoints = new List<WaypointInfo>();
    //TODO: add enum for map location

    private void Start()
    {
        waypointManager = transform.parent.GetComponent<WaypointManager>();
        
        foreach(Transform t in waypointManager.transform)
        {
            if (t.position.x == transform.position.x || t.position.z == transform.position.z)
                attachedWaypoints.Add(t.GetComponent<WaypointInfo>());
        }
    }

    public List<WaypointInfo> GetConnectedWaypoints() { return attachedWaypoints; }
}
