using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    [SerializeField] private int roomIndex = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Minimap.Instance is not null)
        {
            Minimap.Instance.UpdatePlayerRoomLocation(roomIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Minimap.Instance is not null)
        {
            Minimap.Instance.ExitRoom();
        }
    }
}
