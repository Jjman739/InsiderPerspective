using System.Collections.Generic;
using UnityEngine;

public class TileFloorRandomizer : MonoBehaviour
{
    private List<int> trapRows = new();
    private List<int> trapColumns = new();
    private int trapRowCount;
    private int trapColumnCount;

    private List<TrapToggle> allTraps;

    private int strayTrapCount;

    private bool trapsDamage = true;
    private bool trapsSelfDelete = true;
    private bool trapsAlertGuard = false;
    private TileRoomModifiers tileRoomModifiers;
    [SerializeField] private DoorpointManager doorpointManager;

    void Start()
    {
        if (strayTrapCount > 30) { strayTrapCount = 30; }
        allTraps = new List<TrapToggle>();
        tileRoomModifiers = transform.parent.GetComponent<TileRoomModifiers>();
        DisableAllTraps();
        SelectTrapRows();
        SetTrapSettings();
        EnableSelectedTraps();
        EnableStrayTraps();
    }

    private void SelectTrapRows()
    {
        List<int> availableRows = new List<int>();
        List<int> availableCols = new List<int>();
        for (int i = 0; i < 8; i++)
        {
             availableRows.Add(i);
        }
        for (int i = 1; i < 7; i++)
        {
             availableCols.Add(i);
        }

        trapRowCount = tileRoomModifiers.GetModifierByType(typeof(TileRoomRowCount)).GetLevel() + 1;
        trapColumnCount = tileRoomModifiers.GetModifierByType(typeof(TileRoomColumnCount)).GetLevel() + 1;

        for (int i = 0; i < trapRowCount; i++)
        {
            int row = availableRows[Random.Range(0, availableRows.Count)];
            availableRows.Remove(row - 2);
            availableRows.Remove(row - 1);
            availableRows.Remove(row);
            availableRows.Remove(row + 1);
            availableRows.Remove(row + 2);
            trapRows.Add(row);
        }

        for (int i = 0; i < trapColumnCount; i++)
        {
            int column = availableCols[Random.Range(0, availableCols.Count - 1)];
            availableCols.Remove(column - 1);
            availableCols.Remove(column);
            availableCols.Remove(column + 1);
            trapColumns.Add(column);
        }
    }

    private void DisableAllTraps()
    {
        foreach (Transform trapRow in transform)
        {
            foreach (Transform trap in trapRow)
            {
                TrapToggle toggler = trap.gameObject.GetComponent<TrapToggle>();
                if (toggler != null)
                {
                    toggler.TrapDisable();
                    allTraps.Add(toggler);
                }
            }
        }
    }

    private void EnableSelectedTraps()
    {
        int i = 0;
        foreach (Transform trapRow in transform)
        {
            if (trapRows.Contains(i))
            {
                foreach (Transform trap in trapRow)
                {
                    TrapToggle toggler = trap.gameObject.GetComponent<TrapToggle>();
                    if (toggler != null)
                    {
                        toggler.TrapEnable();
                    }
                }
            }
            else
            {
                int j = 0;
                foreach (Transform trap in trapRow)
                {
                    if (trapColumns.Contains(j))
                    {
                         TrapToggle toggler = trap.gameObject.GetComponent<TrapToggle>();
                         if (toggler != null)
                         {
                             toggler.TrapEnable();
                         }
                    }
                    j++;
                }
            }
            i++;
        }
    }

    private void EnableStrayTraps()
    {
        for (int i = 0; i < strayTrapCount; i++)
        {
            TrapToggle trap = allTraps[Random.Range(0, allTraps.Count)];
            if (trap.isEnabled)
            {
                // If it's already on, pick a new one.
                i--;
            }
            else
            {
                trap.TrapEnable();
            }
        }
    }

    private void SetTrapSettings()
    {
        foreach (Transform trapRow in transform)
        {
            foreach (Transform trap in trapRow)
            {
                TrapToggle toggler = trap.gameObject.GetComponent<TrapToggle>();
                if (toggler != null)
                {
                    toggler.trapCollider.damage = trapsDamage;
                    toggler.trapCollider.selfDelete = trapsSelfDelete;
                    toggler.trapCollider.alertGuard = trapsAlertGuard;
                }
                toggler.trapCollider.GetComponent<TrapCollider>().SetDoorPointManager(doorpointManager);
            }
        }
    }

    public bool DoTrapsDamage() { return trapsDamage; }
    public bool DoTrapsAlertGuards() { return trapsAlertGuard; }
    public bool DoTrapsSelfDelete() { return trapsSelfDelete; }
}
