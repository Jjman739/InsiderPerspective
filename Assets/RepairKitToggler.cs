using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepairKitToggler : MonoBehaviour
{
    private TileRoomModifiers tileRoomModifiers;
    private List<GameObject> repairKits = new();

    void Start()
    {
        tileRoomModifiers = transform.parent.GetComponent<TileRoomModifiers>();

        int disabledRepairKits = tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomRepairKitCount));

        foreach (Transform child in transform)
        {
            repairKits.Add(child.gameObject);
        }

        List<GameObject> repairKitsShuffled = repairKits.OrderBy( x => Random.value ).ToList();

        for (int i = 0; i < disabledRepairKits; i++)
        {
            repairKitsShuffled[i].gameObject.SetActive(false);
        }
    }
}
