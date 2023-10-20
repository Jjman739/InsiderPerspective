using UnityEngine;

public class TileTrap : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Dead");
            Destroy(other.gameObject);
        }
    }
}
