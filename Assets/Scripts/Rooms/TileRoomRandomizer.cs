using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TileRoomRandomizer : MonoBehaviour
{
    private int trapRow1;
    private int trapRow2;
    private int trapCol1;
    private int trapCol2;
    private int trapCol3;

    void Start()
    {
        SelectTrapRows();
        DisableAllTraps();
        EnableSelectedTraps();
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

        trapRow1 = availableRows[Random.Range(0, availableRows.Count)];
        availableRows.Remove(trapRow1);
        availableRows.Remove(trapRow1 - 2);
        availableRows.Remove(trapRow1 - 1);
        availableRows.Remove(trapRow1 + 1);
        availableRows.Remove(trapRow1 + 2);

        trapRow2 = availableRows[Random.Range(0, availableRows.Count)];
        availableRows.Remove(trapRow2);
        availableRows.Remove(trapRow2 - 1);
        availableRows.Remove(trapRow2 - 2);
        availableRows.Remove(trapRow2 + 1);
        availableRows.Remove(trapRow2 + 2);

        trapCol1 = availableCols[Random.Range(0, availableCols.Count-1)];
        availableCols.Remove(trapCol1);
        availableCols.Remove(trapCol1 - 1);
        trapCol2 = trapCol1 + 1;
        availableCols.Remove(trapCol2);
        availableCols.Remove(trapCol2 + 1);

        trapCol3 = availableCols[Random.Range(0, availableCols.Count-1)];
        availableCols.Remove(trapCol3);
        availableCols.Remove(trapCol3 - 1);
        availableCols.Remove(trapCol3 + 1);
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
                }
            }
        }
    }

    private void EnableSelectedTraps()
    {
          int i = 0;
          foreach (Transform trapRow in transform)
          {
              if ((i == trapRow1) || (i == trapRow2))
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
                         if ((j == trapCol1) || (j == trapCol2) || (j == trapCol3))
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
}
