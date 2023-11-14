using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointInfo : BaseWaypoint
{
    private float radius = 2f;
    protected List<BaseWaypoint> attachedWaypoints = new List<BaseWaypoint>();

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, radius);
    }

    private void Start()
    {      
        foreach(Transform t in WaypointManager.Instance.transform)
        {
            float distance = Mathf.Sqrt(Mathf.Pow(t.position.x - transform.position.x, 2) + Mathf.Pow(t.position.z - transform.position.z, 2));
            if (distance < 27 && t.GetSiblingIndex() != transform.GetSiblingIndex())
                attachedWaypoints.Add(t.GetComponent<WaypointInfo>());
        }
    }

    public List<BaseWaypoint> GetConnectedWaypoints() { return attachedWaypoints; }

    public List<BaseWaypoint> GetConnectedWaypointsShuffled() { return attachedWaypoints.OrderBy((item) => UnityEngine.Random.Range(0,999)).ToList(); }

    public WaypointInfo GetConnectedWaypoint(int index) { return attachedWaypoints[index] as WaypointInfo; }
    public int GetIndex() { return transform.GetSiblingIndex(); }
}
