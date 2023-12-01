using System.Collections.Generic;
using UnityEngine;
using Enumerations;
using System.Linq;

public class TileFloorRandomizer : MonoBehaviour
{
    private List<int> trapRows = new();
    private List<int> trapColumns = new();
    private int trapRowCount;
    private int trapColumnCount;
    private List<TrapToggle> allTraps;
    private bool trapsSelfDelete = true;

    private Dictionary<TrapType, bool> trapTypes = new Dictionary<TrapType, bool>
    {
        {TrapType.SHOCK, false},
        {TrapType.ALERT_GUARD, false},
        {TrapType.ADD_SHADER, false}
    };
    private TileRoomModifiers tileRoomModifiers;
    [SerializeField] private DoorpointManager doorpointManager;

    void Start()
    {
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

        trapRowCount = tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomRowCount)) + 1;
        trapColumnCount = tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomColumnCount)) + 1;

        for (int i = 0; i < trapRowCount; i++)
        {
            if (availableRows.Count == 0) break;
            int row = availableRows[Random.Range(0, availableRows.Count)];
            availableRows.Remove(Mathf.Max(row - 2, 0));
            availableRows.Remove(Mathf.Max(row - 1, 0));
            availableRows.Remove(row);
            availableRows.Remove(Mathf.Min(row + 1, availableRows.Count - 1));
            availableRows.Remove(Mathf.Min(row + 2, availableRows.Count - 1));
            trapRows.Add(row);
        }

        for (int i = 0; i < trapColumnCount; i++)
        {
            if (availableCols.Count == 0) break;
            int column = availableCols[Random.Range(0, availableCols.Count)];
            availableCols.Remove(Mathf.Max(column - 1, 0));
            availableCols.Remove(column);
            availableCols.Remove(Mathf.Min(column + 1, availableCols.Count - 1));
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
        int strayTrapCount = tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomStrayTilesCount));
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
        bool extraEffect = tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomExtraEffect)) > 0;

        switch(Random.Range(0, trapTypes.Count))
        {
            case 0:
                trapTypes[TrapType.SHOCK] = true;
                break;

            case 1:
                trapTypes[TrapType.ALERT_GUARD] = true;
                break;

            case 2:
                trapTypes[TrapType.ADD_SHADER] = true;
                break;
            
            default:
                break;
        }

        if (extraEffect)
        {
            Dictionary<TrapType, bool> inactiveTrapTypes = trapTypes.Where(d => d.Value == false).ToDictionary(d => d.Key, d => d.Value);
            TrapType extraTrapType = inactiveTrapTypes.ElementAt(Random.Range(0,inactiveTrapTypes.Count)).Key;
            trapTypes[extraTrapType] = true;
        }

        foreach (Transform trapRow in transform)
        {
            foreach (Transform trap in trapRow)
            {
                TrapToggle toggler = trap.gameObject.GetComponent<TrapToggle>();
                if (toggler != null)
                {
                    toggler.trapCollider.damage = trapTypes[TrapType.SHOCK];
                    toggler.trapCollider.selfDelete = trapsSelfDelete;
                    toggler.trapCollider.alertGuard = trapTypes[TrapType.ALERT_GUARD];
                    toggler.trapCollider.addShader = trapTypes[TrapType.ADD_SHADER];
                }
                toggler.trapCollider.GetComponent<TrapCollider>().SetDoorPointManager(doorpointManager);
            }
        }
    }

    public bool DoTrapsDamage() { return trapTypes[TrapType.SHOCK]; }
    public bool DoTrapsAlertGuards() { return trapTypes[TrapType.ALERT_GUARD]; }
    public bool DoTrapsSelfDelete() { return trapsSelfDelete; }
}
