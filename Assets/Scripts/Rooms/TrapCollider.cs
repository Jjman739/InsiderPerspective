using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    [SerializeField] private TrapToggle toggler;

    void OnTriggerEnter(Collider other)
    {
        ThiefManager manager = other.gameObject.GetComponent<ThiefManager>();
        if (manager!= null)
        {
            manager.TakeDamage();
            toggler.TrapDisable();
        }
    }
}
