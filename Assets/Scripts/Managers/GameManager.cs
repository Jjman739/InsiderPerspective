using UnityEngine;
using Enumerations;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private RoomDifficulty difficulty;

    public GameMode GetCurrentGameMode() { return gameMode; }
    public RoomDifficulty GetCurrentDifficulty() { return difficulty; }

    public static System.Random RNG = new System.Random();

    public void SetGameMode(int gameMode) { this.gameMode = (GameMode)gameMode; }
    public void SetDifficulty(int difficulty) { this.difficulty = (RoomDifficulty)difficulty; } 
}