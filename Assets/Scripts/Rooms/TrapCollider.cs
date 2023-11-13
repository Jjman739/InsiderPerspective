using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    [SerializeField] private TrapToggle toggler;

    public bool damage = true;
    public bool selfDelete = true;
    public bool alertGuard = false;

    void OnTriggerEnter(Collider other)
    {
        ThiefManager manager = other.gameObject.GetComponent<ThiefManager>();
        if (manager!= null)
        {
            if (damage) { manager.TakeDamage(); }
            if (selfDelete) { toggler.TrapDisable(); }
        }
    }
}
