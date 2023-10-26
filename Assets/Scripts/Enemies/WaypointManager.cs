using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [Range(0f, 2f)]
    [SerializeField] private float waypointSize = 1f;

    public WaypointInfo GetNextWaypoint(WaypointInfo currentWaypoint)
    {
        if (currentWaypoint is null)
            return transform.GetChild(Random.Range(0,transform.childCount)).GetComponent<WaypointInfo>();

        List<WaypointInfo> possibleWaypoints = currentWaypoint.GetConnectedWaypoints();

        return possibleWaypoints[Random.Range(0,possibleWaypoints.Count)];
    }

    public WaypointInfo GetNextWaypointWithOverride(WaypointInfo waypoint, WaypointInfo target)
    {
        Queue<(WaypointInfo, WaypointInfo)> toCheck = new Queue<(WaypointInfo, WaypointInfo)>();
        List<int> allChecked = new List<int>();

        toCheck.Enqueue((null, waypoint));

        while(toCheck.Count > 0) {
            var (parentWaypoint, nextWaypoint) = toCheck.Dequeue();
            allChecked.Add(nextWaypoint.GetIndex());

            if(nextWaypoint.GetIndex() == target.GetIndex()) {
                Debug.Log($"you will never see this run, so i will say the n word here {parentWaypoint.GetIndex()} times");
                if(parentWaypoint is null) {
                    return nextWaypoint;
                }

                return parentWaypoint;
            }

            foreach(WaypointInfo w in nextWaypoint.GetConnectedWaypointsShuffled()) {
                if(!allChecked.Contains(w.GetIndex()) && !toCheck.Any((qw) => qw.Item2.GetIndex() == w.GetIndex())) {
                    if(parentWaypoint is null) {
                        toCheck.Enqueue((w, w));
                    }else {
                        toCheck.Enqueue((parentWaypoint, w));
                    }
                }
            }
        }

        return null;
    }

    public WaypointInfo GetWaypointByIndex(int index)
    {
        return transform.GetChild(index).GetComponent<WaypointInfo>();
    }
}
