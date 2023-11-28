using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoomColumnCount : RoomModifier
{
    public TileRoomColumnCount()
    {
        weight = 20;
        weightDecay = 10;
        cost = 5;
    }
}
