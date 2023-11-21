using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRandomizer : MonoBehaviour
{
    [SerializeField] private GameObject openWall;
    private List<GameObject> closedWalls;

    public int openingWidth;

    void Start()
    {
        closedWalls = new List<GameObject>();

        foreach (Transform child in transform)
        {
            closedWalls.Add(child.gameObject);
        }

        int openStart = Random.Range(1, closedWalls.Count - (1 + openingWidth));
        
        for (int i = openStart; i < openStart + openingWidth; i++)
        {
            GameObject wall = closedWalls[i];
            Instantiate(openWall, wall.transform.position, wall.transform.rotation, transform);
            Destroy(wall);
        }
    }
}
