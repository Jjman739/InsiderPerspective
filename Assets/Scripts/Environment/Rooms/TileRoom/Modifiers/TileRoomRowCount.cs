using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoomRowCount : RoomModifier
{
    public TileRoomRowCount()
    {
        weight = 20;
        weightDecay = 10;
        cost = 5;
    }
}