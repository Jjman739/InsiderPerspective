using UnityEngine;
using Enumerations;

public class GameModeManager : Singleton<GameModeManager>
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private RoomDifficulty difficulty;

    public GameMode GetCurrentGameMode() { return gameMode; }
    public RoomDifficulty GetCurrentDifficulty() { return difficulty; }

    public void SetGameMode(int gameMode) { this.gameMode = (GameMode)gameMode; }
    public void SetDifficulty(int difficulty) { this.difficulty = (RoomDifficulty)difficulty; } 
}