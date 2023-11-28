public class TileRoomModifiers : RoomDifficultyBuilder
{
    protected override void initializeModifiers()
    {
        modifiers.Add(new TileRoomRowCount());
        modifiers.Add(new TileRoomColumnCount());
    }
}
