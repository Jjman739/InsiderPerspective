using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapCollider : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit a trap.");
            SceneManager.LoadScene("LoseScene");
        }
    }
}
