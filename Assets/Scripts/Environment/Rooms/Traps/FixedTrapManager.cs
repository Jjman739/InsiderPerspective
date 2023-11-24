using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTrapManager : MonoBehaviour
{
    public bool trapsDamage = true;
    public bool trapsSelfDelete = true;
    public bool trapsAlertGuard = false;

    [SerializeField] private DoorpointManager doorpointManager;
    
    void Start()
    {
        foreach (Transform trap in transform)
        {
            TrapToggle toggler = trap.gameObject.GetComponent<TrapToggle>();
            if (toggler != null)
            {
                toggler.TrapDisable();
                toggler.trapCollider.damage = trapsDamage;
                toggler.trapCollider.selfDelete = trapsSelfDelete;
                toggler.trapCollider.alertGuard = trapsAlertGuard;
                toggler.TrapEnable();
            }
            toggler.trapCollider.GetComponent<TrapCollider>().SetDoorPointManager(doorpointManager);
        }
    }
}
