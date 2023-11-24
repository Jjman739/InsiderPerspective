using UnityEngine;

public class GuardLineOfSight : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ThiefMovementScript>().SetInLineOfSight(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ThiefMovementScript>().SetInLineOfSight(false);
        }
    }
}
