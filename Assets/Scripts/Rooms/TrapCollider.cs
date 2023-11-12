using UnityEngine;

public class TrapCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ThiefManager manager = other.gameObject.GetComponent<ThiefManager>();
        if (manager!= null)
        {
            manager.TakeDamage();
        }
    }
}
