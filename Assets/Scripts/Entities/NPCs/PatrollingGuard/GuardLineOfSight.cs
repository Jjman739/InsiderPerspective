using UnityEngine;

public class GuardLineOfSight : MonoBehaviour
{
    [SerializeField] private bool doRaycast = true;
    [SerializeField] private Transform eyePoint;

    private bool colliding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (doRaycast)
            {
                bool hit = Physics.Raycast(other.gameObject.transform.position, eyePoint.position);
                if (hit)
                {
                    return;
                }
            }

            other.gameObject.GetComponent<ThiefMovementScript>().SetInLineOfSight(true);
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colliding && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ThiefMovementScript>().SetInLineOfSight(false);
        }
    }
}
