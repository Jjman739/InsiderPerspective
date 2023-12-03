using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaTracker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Minimap.Instance is not null)
        {
            Minimap.Instance.UpdatePlayerHallwayLocation(transform.GetSiblingIndex());
        }
    }
}
