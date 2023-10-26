using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoomRandomizer : MonoBehaviour
{
    void Start()
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

        int trapRow1 = availableRows[Random.Range(0, availableRows.Count)];
        availableRows.Remove(trapRow1);
        availableRows.Remove(trapRow1 - 1);
        availableRows.Remove(trapRow1 + 1);

        int trapRow2 = availableRows[Random.Range(0, availableRows.Count)];
        availableRows.Remove(trapRow2);
        availableRows.Remove(trapRow2 - 1);
        availableRows.Remove(trapRow2 + 1);

        int trapCol1 = availableRows[Random.Range(0, availableCols.Count-1)];
        availableCols.Remove(trapCol1);
        availableCols.Remove(trapCol1 - 1);
        int trapCol2 = trapCol1 + 1;
        availableCols.Remove(trapCol2);
        availableCols.Remove(trapCol2 + 1);

        int trapCol3 = availableRows[Random.Range(0, availableCols.Count-1)];
        availableCols.Remove(trapCol3);
        availableCols.Remove(trapCol3 - 1);
        availableCols.Remove(trapCol3 + 1);
    }
}
