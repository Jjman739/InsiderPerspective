using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Enumerations;

public class RoomTracker : MonoBehaviour
{
    [SerializeField] private int roomIndex = -1;

    private TileFloorRandomizer tileRoomRandomizer;
    private bool isTileRoom = false;
    private bool isShockTileRoom = false;
    private bool isGuardTileRoom = false;
    
    private void Start()
    {
        tileRoomRandomizer = transform.parent.GetComponentInChildren<TileFloorRandomizer>();
        if (tileRoomRandomizer is not null)
        {
            isTileRoom = true;
            isShockTileRoom = tileRoomRandomizer.DoTrapsDamage();
            isGuardTileRoom = tileRoomRandomizer.DoTrapsAlertGuards();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Minimap.Instance is not null)
        {
            Minimap.Instance.UpdatePlayerRoomLocation(roomIndex);

            if (isTileRoom)
            {
                if (isShockTileRoom)
                {
                    DialogueManager.Instance.PlayDialogue(DialogueEvent.ENTER_TILE_ROOM_SHOCK);
                }
                else if (isGuardTileRoom)
                {
                    DialogueManager.Instance.PlayDialogue(DialogueEvent.ENTER_TILE_ROOM_GUARD);
                }
            }

            else if (roomIndex == 0)
            {
                DialogueManager.Instance.PlayDialogue(DialogueEvent.GAME_START);
            }

            else
            {
                DialogueManager.Instance.PlayDialogue(DialogueEvent.PLATFORMER_ROOM);
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
}
