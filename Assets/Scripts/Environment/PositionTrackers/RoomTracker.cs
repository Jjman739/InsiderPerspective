using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enumerations;

public class RoomTracker : MonoBehaviour
{
    [SerializeField] private int roomIndex = -1;
    private TileFloorRandomizer tileFloorRandomizer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Minimap.Instance is not null)
        {
            Minimap.Instance.UpdatePlayerRoomLocation(roomIndex);

            if (roomIndex == 0)
            {
                DialogueManager.Instance.PlayDialogue(DialogueEvent.GAME_START);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Minimap.Instance is not null)
        {
            Minimap.Instance.ExitRoom();
        }
    }

    public int GetRoomIndex() { return roomIndex; }
}
