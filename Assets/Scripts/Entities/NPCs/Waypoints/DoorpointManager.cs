using System;
using System.Collections.Generic;
using UnityEngine;

public class DoorpointManager : MonoBehaviour
{
    List<Transform> doorpoints = new List<Transform>();

    public void Start()
    {
        foreach (Transform c in transform)
        {
            doorpoints.Add(c);
        }
    }

    public void SendGuardToRoom()
    {
        float smallestDistance = 9999f;
        Tuple<PatrollingGuard, DoorpointInfo> closestGuardPointPair = null;

        foreach (GameObject guard in WaypointManager.Instance.GetGuards())
        {
            foreach (Transform doorpoint in doorpoints)
            {
                float distance = Vector3.Distance(guard.transform.position, doorpoint.position);
                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestGuardPointPair = new Tuple<PatrollingGuard, DoorpointInfo>(guard.GetComponent<PatrollingGuard>(), doorpoint.GetComponent<DoorpointInfo>());
                }
            }
        }

        if (closestGuardPointPair != null)
        {
            closestGuardPointPair.Item1.GoToWaypointNearestToDoorpoint(closestGuardPointPair.Item2);
        }
    }
}
