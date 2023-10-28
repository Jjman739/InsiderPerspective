using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private List<PatrollingGuard> patrollingGuards = new List<PatrollingGuard>();
    int overrideTargetIndex = -1;

    private void Update()
    {
        foreach (Transform t in transform)
        {
            if (overrideTargetIndex != -1 && t.GetSiblingIndex() == overrideTargetIndex)
                t.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
            else
                t.GetComponent<MeshRenderer>().materials[0].color = Color.black;
        }

        if (patrollingGuards.Count <= 0) return;

        foreach (PatrollingGuard guard in patrollingGuards)
        {
            int guardLocation = guard.GetLastWaypoint().transform.GetSiblingIndex();
            transform.GetChild(guardLocation).GetComponent<MeshRenderer>().materials[0].color = Color.red;

            if (guard.GetOverrideTarget() is null)
                overrideTargetIndex = -1;
        }
    }

    public bool OverrideGuardTarget(int targetIndex)
    {
        if (overrideTargetIndex is not -1)
            return false;

        foreach (PatrollingGuard guard in patrollingGuards)
        {
            guard.SetOverrideTarget(targetIndex);
        }

        overrideTargetIndex = targetIndex;

        return true;
    }
    public void AddPatrollingGuard(PatrollingGuard guard) { patrollingGuards.Add(guard); }
    public void SetPatrollingGuards(List<PatrollingGuard> guards) { patrollingGuards = guards; }
}
