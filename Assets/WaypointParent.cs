using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointParent : MonoBehaviour
{
    [SerializeField] WaypointManager waypointManager;

    public WaypointManager GetWaypointManager()
    {
        return waypointManager;
    }
}
