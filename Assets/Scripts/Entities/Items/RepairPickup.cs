using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ThiefManager thief = other.gameObject.GetComponent<ThiefManager>();
        if (thief != null)
        {
            if (thief.needsRepair)
            {
                thief.Repair();
                DialogueManager.Instance.PlayDialogue(Enumerations.DialogueEvent.REPAIR_KIT);
                Destroy(gameObject);
            }
        }
    }
}
