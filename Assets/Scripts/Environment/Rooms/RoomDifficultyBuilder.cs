using System;
using System.Collections.Generic;
using UnityEngine;
using Enumerations;

public abstract class RoomDifficultyBuilder : MonoBehaviour
{
    [SerializeField] protected RoomDifficulty difficulty;
    protected List<RoomModifier> modifiers = new();
    private int availablePoints;

    private void Awake()
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
        }
    }

    protected abstract void initializeModifiers();

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

        int randomNum = UnityEngine.Random.Range(0, totalWeight);

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

    public RoomModifier GetModifierByType(Type modifierType)
    {
        foreach (RoomModifier modifier in modifiers)
        {
            if (modifier.GetType() == modifierType)
            {
                return modifier;
            }
        }

        return null;
    }

    public int GetModifierLevelByType(Type modifierType)
    {
        foreach (RoomModifier modifier in modifiers)
        {
            if (modifier.GetType() == modifierType)
            {
                return modifier.GetLevel();
            }
        }

        return -1;
    }

    public RoomDifficulty GetRoomDifficulty() { return difficulty; }
}
