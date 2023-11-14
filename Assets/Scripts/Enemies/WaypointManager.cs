using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointManager : Singleton<WaypointManager>
{
    [SerializeField] private List<GameObject> guards = new List<GameObject>();
    [SerializeField] private Transform waypointParent;
    [SerializeField] private Minimap minimap;

    void Start()
    {
        foreach (GameObject guard in guards)
        {
            PatrollingGuard patrollingGuard = guard.GetComponent<PatrollingGuard>();
            patrollingGuard.Initialize(this, waypointParent);
            minimap.AddPatrollingGuard(patrollingGuard);
        }
    }

    public BaseWaypoint GetNextWaypoint(BaseWaypoint currentWaypoint)
    {
        if (currentWaypoint is null)
            return waypointParent.GetChild(Random.Range(0,waypointParent.childCount)).GetComponent<WaypointInfo>();

        if (currentWaypoint is DoorpointInfo)
        {
            return (currentWaypoint as DoorpointInfo).GetPreviousWaypoint();
        }

        List<BaseWaypoint> possibleWaypoints = (currentWaypoint as WaypointInfo).GetConnectedWaypoints();

        return possibleWaypoints[Random.Range(0,possibleWaypoints.Count)];
    }

    public WaypointInfo GetNextWaypointWithOverride(BaseWaypoint waypoint, WaypointInfo target)
    {
        Queue<(WaypointInfo, WaypointInfo)> toCheck = new Queue<(WaypointInfo, WaypointInfo)>();
        List<int> allChecked = new List<int>();

        toCheck.Enqueue((null, waypoint as WaypointInfo));

        while(toCheck.Count > 0) {
            var (parentWaypoint, nextWaypoint) = toCheck.Dequeue();
            allChecked.Add(nextWaypoint.GetIndex());

            if (nextWaypoint.GetIndex() == target.GetIndex())
            {
                if (parentWaypoint is null)
                {
                    return nextWaypoint;
                }

                return parentWaypoint;
            }

            foreach (WaypointInfo w in nextWaypoint.GetConnectedWaypointsShuffled())
            {
                if (!allChecked.Contains(w.GetIndex()) && !toCheck.Any((qw) => qw.Item2.GetIndex() == w.GetIndex()))
                {
                    if (parentWaypoint is null)
                    {
                        toCheck.Enqueue((w, w));
                    }
                    else
                    {
                        toCheck.Enqueue((parentWaypoint, w));
                    }
                }
            }
        }

        return null;
    }

    public WaypointInfo GetWaypointByIndex(int index)
    {
        return waypointParent.GetChild(index).GetComponent<WaypointInfo>();
    }

    public List<GameObject> GetGuards() { return guards; }
}
