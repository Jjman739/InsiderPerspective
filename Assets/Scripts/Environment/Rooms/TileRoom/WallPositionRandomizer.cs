using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallPositionRandomizer : MonoBehaviour
{
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;
    [SerializeField] private float maxSize = 6;
    private List<GameObject> walls = new();
    private List<int> wallWidths = new();
    private TileRoomModifiers tileRoomModifiers;

    void Start()
    {
        tileRoomModifiers = transform.parent.GetComponent<TileRoomModifiers>();

        foreach (Transform child in transform)
        {
            walls.Add(child.gameObject);
            wallWidths.Add(0);
        }

        List<int> possibleIndices = Enumerable.Range(0, wallWidths.Count).ToList();
        List<int> maxedOutWallIndices = new();

        for (int i = 0; i < tileRoomModifiers.GetModifierLevelByType(typeof(TileRoomWallWidth)); i++)
        {
            foreach (int index in maxedOutWallIndices)
            {
                possibleIndices.Remove(index);
            }

            maxedOutWallIndices.Clear();

            int chosenIndex = possibleIndices[Random.Range(0, possibleIndices.Count)];
            wallWidths[chosenIndex] += 1;
            if (wallWidths[chosenIndex] >= maxSize)
            {
                maxedOutWallIndices.Add(chosenIndex);
            }
        }

        for (int i = 0; i < walls.Count; i++)
        {
            GameObject blocker = walls[i].transform.GetChild(0).gameObject;
            float size = wallWidths[i];
            minZ += size / 2;
            maxZ -= size / 2;
            float z = Random.Range(minZ, maxZ);

            blocker.transform.localScale = new Vector3(blocker.transform.localScale.x, blocker.transform.localScale.y, size);
            blocker.transform.localPosition = new Vector3(blocker.transform.localPosition.x, blocker.transform.localPosition.y, z);
            
            if (size < 1)
            {
                blocker.SetActive(false);
            }

            Debug.Log($"{tileRoomModifiers.GetRoomDifficulty()} wall width: {size}");
        }
    }
}
