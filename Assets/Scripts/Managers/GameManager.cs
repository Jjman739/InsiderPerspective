using UnityEngine;
using Enumerations;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private RoomDifficulty difficulty;
    private float mouseSensitivity;
    public static System.Random RNG = new System.Random();

    public GameMode GetCurrentGameMode() { return gameMode; }
    public RoomDifficulty GetCurrentDifficulty() { return difficulty; }
    public float GetMouseSensitivity() { return mouseSensitivity; }

    public void SetGameMode(int gameMode) { this.gameMode = (GameMode)gameMode; }
    public void SetDifficulty(int difficulty) { this.difficulty = (RoomDifficulty)difficulty; } 
    public void SetMouseSensitivity(float mouseSensitivity)
    {
        this.mouseSensitivity = mouseSensitivity;
        PlayerPrefs.SetFloat("MouseSensitivity", mouseSensitivity);
    }
}