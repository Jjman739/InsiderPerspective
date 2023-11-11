using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityDoor : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        ThiefManager thief = other.gameObject.GetComponent<ThiefManager>();
        if (thief != null)
        {
            // Try winning before repairing. No need to repair if you won.
            if (!thief.AttemptWin())
            {
                if (thief.needsRepair && thief.repairsRemaining > 0) { 
                    thief.Repair();
                }
            }
        }
    }
}
