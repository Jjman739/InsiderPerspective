namespace Enumerations
{
    public enum DialogueEvent
    {
        GAME_START,
        MOVE_ROBOT,
        VIEW_START_ROOM,
        VIEW_HALLWAY,
        VIEW_TILE_ROOM,
        VIEW_PLATFORMER_ROOM,
        TRAP_SHOCK,
        TRAP_GUARD,
        TRAP_CAMERA,
        REPAIR_KIT,
        RELIC,
        ALL_RELICS,
        GAME_END_LOSE,
        GAME_END_WIN
    }

    public enum RoomDifficulty
    {
        EASY,
        MEDIUM,
        HARD,
        LEET
    }

    public enum GameMode
    {
        NORMAL,
        SURVIVAL
    }

    public enum TrapType
    {
        SHOCK,
        ALERT_GUARD,
        ADD_SHADER
    }
}