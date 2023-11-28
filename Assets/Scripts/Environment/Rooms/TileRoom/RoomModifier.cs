using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public abstract class RoomModifier
{
    protected int weight = 5;
    protected int weightDecay = 1;
    protected int cost = 1;
    protected int points = 0;
    protected int level { get { return points / cost; } }
    public int GetWeight() { return weight; }
    public int GetCost() { return cost; }
    public int GetPoints() { return points; }
    public int GetLevel() { return points / cost; }
    public void LevelUp()
    {
        points += cost;
        weight -= weightDecay;
    }
}
