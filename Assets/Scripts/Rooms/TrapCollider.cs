using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    [SerializeField] private TrapToggle toggler;

    private DoorpointManager doorpointManager;

    public bool damage = true;
    public bool selfDelete = true;
    public bool alertGuard = false;

    void OnTriggerEnter(Collider other)
    {
        ThiefManager thiefManager = other.gameObject.GetComponent<ThiefManager>();
        if (thiefManager != null)
        {
            if (damage) { thiefManager.TakeDamage(); }
            if (selfDelete) { toggler.TrapDisable(); }
            if (alertGuard) { doorpointManager.SendGuardToRoom(); }
        }
    }

    public void SetDoorPointManager(DoorpointManager dm)
    {
        doorpointManager = dm;
    }
}
