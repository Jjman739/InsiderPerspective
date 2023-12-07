using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepairKitToggler : MonoBehaviour
{
    private RoomDifficultyBuilder modifiers;
    private List<GameObject> repairKits = new();

    void Start()
    {
        modifiers = transform.parent.GetComponent<RoomDifficultyBuilder>();

        int disabledRepairKits = modifiers.GetModifierLevelByType(typeof(RepairKitCount));

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
