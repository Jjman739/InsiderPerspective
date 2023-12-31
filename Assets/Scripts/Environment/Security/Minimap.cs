using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minimap : Singleton<Minimap>
{
    private List<GameObject> mapAreas = new();
    private List<GameObject> mapRooms = new();
    private bool currentlyInRoom;
    private int currentHallwayLocation;

    private void Start()
    {
        foreach (Transform child in transform.GetChild(1))
        {
            mapAreas.Add(child.gameObject);
        }

        foreach (Transform child in transform.GetChild(2))
        {
            mapRooms.Add(child.gameObject);
        }
    }

    public void UpdatePlayerRoomLocation(int roomIndex)
    {
        clearMarkedLocations();

        currentlyInRoom = true;

        mapRooms[roomIndex].GetComponent<MeshRenderer>().materials[0].color = Color.blue;
    }

    public void UpdatePlayerHallwayLocation(int areaIndex, bool exitingRoom = false)
    {
        currentHallwayLocation = areaIndex;

        if (currentlyInRoom && !exitingRoom)
        { 
            return;
        }

        currentlyInRoom = false;
    
        clearMarkedLocations();

        mapAreas[areaIndex].GetComponent<MeshRenderer>().enabled = true;
    }

    public void ExitRoom()
    {
        currentlyInRoom = false;
        UpdatePlayerHallwayLocation(currentHallwayLocation);
    }

    private void clearMarkedLocations()
    {
        foreach (GameObject area in mapAreas)
        {
            area.GetComponent<MeshRenderer>().enabled = false;
        }

        foreach (GameObject room in mapRooms)
        {
            room.GetComponent<MeshRenderer>().materials[0].color = Color.white;
        }
    }
}
