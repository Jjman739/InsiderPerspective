using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;
using UnityEngine.Tilemaps;

public class TileRoom : MonoBehaviour
{
    [SerializeField] private RoomDifficulty difficulty;

    private List<RoomModifier> modifiers = new();
    private int availablePoints;

    private void Start()
    {
        calculateAvailablePoints();

        initializeModifiers();

        while (availablePoints > 0)
        {
            RoomModifier modifier = getRandomWeightedModifier();
            if (modifier is null)
            {
                availablePoints = 0;
                continue;
            }

            modifier.LevelUp();
            availablePoints -= modifier.GetCost();

            Debug.Log($"Current Points: {availablePoints}");
            Debug.Log($"Current Points: {modifier.GetPoints()}");
        }
    }

    private void initializeModifiers()
    {
        modifiers.Add(new TileRoomRowCount());
    }

    private RoomModifier getRandomWeightedModifier()
    {
        int totalWeight = 0;
        foreach (RoomModifier modifier in modifiers)
        {
            if (modifier.GetCost() <= availablePoints)
            {
                totalWeight += modifier.GetWeight();
            }
        }

        if (totalWeight == 0)
        {
            return null;
        }

        int randomNum = Random.Range(0, totalWeight);

        foreach (RoomModifier modifier in modifiers)
        {
            if (randomNum < modifier.GetWeight())
            {
                return modifier;
            }
            randomNum -= modifier.GetWeight();
        }

        return null;
    }

    private void calculateAvailablePoints()
    {
        switch (difficulty)
        {
            case RoomDifficulty.EASY:
                availablePoints = 15;
                break;

            case RoomDifficulty.MEDIUM:
                availablePoints = 30;
                break;

            case RoomDifficulty.HARD:
                availablePoints = 60;
                break;

            case RoomDifficulty.LEET:
                availablePoints = 100;
                break;
            
            default:
                availablePoints = 0;
                break;
        }
    }
}
