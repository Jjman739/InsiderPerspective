using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicPickup : MonoBehaviour
{
    public int Value = 1;

    void OnTriggerEnter(Collider other)
    {
        ThiefInventory inv = other.gameObject.GetComponent<ThiefInventory>();
        if (inv != null)
        {
            inv.TreasureValue += Value;
            GameObject.Destroy(gameObject);
        }
    }
}
