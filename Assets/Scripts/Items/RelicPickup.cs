using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPickup : MonoBehaviour
{
    public int Value = 1;

    void OnTriggerEnter(Collider other)
    {
        ThiefTreasure inv = other.gameObject.GetComponent<ThiefTreasure>();
        if (inv != null)
        {
            inv.treasureCount += Value;
            ScoreTracker.control.treasureValue++;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(transform.GetChild(0).gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
