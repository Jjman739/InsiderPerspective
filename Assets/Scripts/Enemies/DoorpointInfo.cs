using UnityEngine;

public class DoorpointInfo : BaseWaypoint
{
    private WaypointInfo previousWaypoint;
    [SerializeField] private int direction; // 0 = north, 1 = east, 2 = south, 3 = west

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, 1f);
    }

    public WaypointInfo GetPreviousWaypoint() { return previousWaypoint; }
    public void SetPreviousWaypoint(WaypointInfo wp) { previousWaypoint = wp; }
    public int GetDirection() { return direction; }
}
