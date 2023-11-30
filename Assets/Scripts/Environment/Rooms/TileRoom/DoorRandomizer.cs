using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorRandomizer : MonoBehaviour
{
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private GameObject openWall;
    [SerializeField] private int doorMinWidth = 2; 
    [SerializeField] private int doorMaxWidth = 6;
    private List<int> doorWidths = new();
    private List<GameObject> closedWalls;
    private TileRoomModifiers tileRoomModifiers;

    void Start()
    {
        tileRoomModifiers = transform.parent.GetComponent<TileRoomModifiers>();

        for (int i = 0; i < doors.Count; i++)
        {
            doorWidths.Add(doorMaxWidth);
        }

        List<int> possibleIndices = Enumerable.Range(0, doorWidths.Count).ToList();
        List<int> maxedOutDoorIndices = new();

        for (int i = 0; i < tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomDoorwayWidth)); i++)
        {
            foreach (int index in maxedOutDoorIndices)
            {
                possibleIndices.Remove(index);
            }

            maxedOutDoorIndices.Clear();

            int chosenIndex = possibleIndices[Random.Range(0, possibleIndices.Count)];
            doorWidths[chosenIndex] -= 1;
            if (doorWidths[chosenIndex] <= doorMinWidth)
            {
                maxedOutDoorIndices.Add(chosenIndex);
            }
        }

        for (int i = 0; i < doors.Count; i++)
        {
            closedWalls = new List<GameObject>();

            foreach (Transform child in doors[i].transform)
            {
                closedWalls.Add(child.gameObject);
            }

            int openingWidth = doorWidths[i];
            int openStart = Random.Range(1, closedWalls.Count - (1 + openingWidth));
            
            for (int j = openStart; j < openStart + openingWidth; j++)
            {
                GameObject wall = closedWalls[j];
                Instantiate(openWall, wall.transform.position, wall.transform.rotation, doors[i].transform);
                Destroy(wall);
            }
        }
    }
}
