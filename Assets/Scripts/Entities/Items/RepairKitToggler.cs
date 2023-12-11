using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepairKitToggler : MonoBehaviour
{
    private RoomModifiers modifiers;
    private List<GameObject> repairKits = new();

    void Start()
    {
        modifiers = transform.parent.GetComponent<RoomModifiers>();

        int disabledRepairKits = modifiers.GetModifierLevelByType(typeof(RepairKitCount));

        foreach (Transform child in transform)
        {
            repairKits.Add(child.gameObject);
        }

        List<GameObject> repairKitsShuffled = repairKits.OrderBy( x => Random.value ).ToList();

		int kitsToHide = Mathf.Min(disabledRepairKits, repairKitsShuffled.Count);

        for (int i = 0; i < kitsToHide; i++)
        {
            repairKitsShuffled[i].gameObject.SetActive(false);
        }
    }
}
