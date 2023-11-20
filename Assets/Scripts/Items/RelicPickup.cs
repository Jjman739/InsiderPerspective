using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public class RelicPickup : MonoBehaviour
{
    public int Value = 1;

    void OnTriggerEnter(Collider other)
    {
        ThiefTreasure inv = other.gameObject.GetComponent<ThiefTreasure>();
        if (inv != null)
        {
            inv.treasureCount += Value;
            ScoreTracker.Instance.treasureValue++;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(transform.GetChild(0).gameObject);
            GetComponent<AudioSource>().Play();

            if (inv.treasureCount >= inv.goal)
            {
                DialogueManager.Instance.PlayDialogue(DialogueEvent.GAME_END);
            }
        }
    }
}
