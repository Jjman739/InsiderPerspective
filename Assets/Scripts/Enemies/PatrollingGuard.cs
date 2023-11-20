using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrollingGuard : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float distanceThreshold = 0.1f;
    private BaseWaypoint lastWaypoint;
    private BaseWaypoint nextWaypoint;
    private WaypointInfo overrideTarget;
    private List<WaypointInfo> waypoints = new List<WaypointInfo>();
    private WaypointManager waypointManager;
    private Transform waypointsObject;

    private bool initialized = false;
    private DoorpointInfo targetDoorpoint;
    private bool searchMode = false;
    [SerializeField] private float searchTimer = 5f;
    private float currentSearchTimer;
    private GameObject flashlight;
    [SerializeField] private float searchScaleAmount = 2f;

    // Start is called before the first frame update
    public void Initialize(WaypointManager manager, Transform wpObject)
    {
        waypointManager = manager;
        waypointsObject = wpObject;

        currentSearchTimer = searchTimer;

        flashlight = transform.GetChild(1).gameObject;

        foreach (Transform t in waypointsObject)
        {
            waypoints.Add(t.GetComponent<WaypointInfo>());
        }

        nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
        transform.position = nextWaypoint.transform.position;

        lastWaypoint = nextWaypoint;

        nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
        transform.LookAt(nextWaypoint.transform);

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized) return;

        if (searchMode)
        {
            currentSearchTimer -= Time.deltaTime;

            if (currentSearchTimer <= 0)
            {
                targetDoorpoint = null;
                BaseWaypoint oldWaypoint = nextWaypoint;
                nextWaypoint = (nextWaypoint as DoorpointInfo)?.GetPreviousWaypoint();
                lastWaypoint = oldWaypoint;
                transform.LookAt(nextWaypoint.transform);
                flashlight.transform.localPosition = new Vector3(flashlight.transform.localPosition.x, flashlight.transform.localPosition.y, flashlight.transform.localPosition.z / searchScaleAmount);
                flashlight.transform.localScale = new Vector3(flashlight.transform.localScale.x, flashlight.transform.localScale.y / searchScaleAmount, flashlight.transform.localScale.z);
                searchMode = false;
            }
            else
            {
                float rotation = 2;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + rotation, transform.rotation.eulerAngles.z);

            }
        }

        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, nextWaypoint.transform.position) < distanceThreshold)
        {
            lastWaypoint = nextWaypoint;

            if (overrideTarget != null && lastWaypoint is WaypointInfo && (lastWaypoint as WaypointInfo).GetIndex() == overrideTarget.GetIndex())
            {
                if (targetDoorpoint != null)
                {
                    targetDoorpoint.SetPreviousWaypoint(nextWaypoint as WaypointInfo);
                    nextWaypoint = targetDoorpoint;
                    overrideTarget = null;
                    transform.LookAt(nextWaypoint.transform);
                    return;
                }

                overrideTarget = null;

            }

            if (nextWaypoint is DoorpointInfo)
            {
                if (!searchMode)
                {
                    currentSearchTimer = searchTimer;
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    flashlight.transform.localPosition = new Vector3(flashlight.transform.localPosition.x, flashlight.transform.localPosition.y, flashlight.transform.localPosition.z * searchScaleAmount);
                    flashlight.transform.localScale = new Vector3(flashlight.transform.localScale.x, flashlight.transform.localScale.y * searchScaleAmount, flashlight.transform.localScale.z);
                    searchMode = true;
                }
            }
            else if (overrideTarget is null)
            {
                nextWaypoint = waypointManager.GetNextWaypoint(nextWaypoint);
            }
            else
            {
                nextWaypoint = waypointManager.GetNextWaypointWithOverride(nextWaypoint, overrideTarget);
            }

            transform.LookAt(nextWaypoint.transform);
        }
    }

    public void GoToWaypointNearestToDoorpoint(DoorpointInfo doorpoint)
    {
        float smallestDistance = 9999f;
        WaypointInfo closestWaypoint = null;

        foreach (WaypointInfo waypoint in waypoints)
        {
            float distance = Vector3.Distance(waypoint.transform.position, doorpoint.transform.position);
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                closestWaypoint = waypoint;
            }
        }

        if (closestWaypoint != null)
        {
            SetOverrideTarget(closestWaypoint.GetIndex(), doorpoint);
        }
    }

    public void SetOverrideTarget(int targetIndex, DoorpointInfo doorpoint = null)
    {
        overrideTarget = waypointManager.GetWaypointByIndex(targetIndex);

        if (doorpoint != null)
        {
            targetDoorpoint = doorpoint;
        }
    }

    public void SetWaypointManager(WaypointManager manager) { waypointManager = manager; }

    public void SetWaypoints(Transform obj) { waypointsObject = obj; }
    public BaseWaypoint GetLastWaypoint() { return lastWaypoint; }
    public WaypointInfo GetOverrideTarget() { return overrideTarget; }
    public void SetMoveSpeed(float speed) { moveSpeed = speed; }
}
