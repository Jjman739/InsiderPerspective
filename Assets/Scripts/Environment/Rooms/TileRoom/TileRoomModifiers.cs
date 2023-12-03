public class TileRoomModifiers : RoomDifficultyBuilder
{
    protected override void initializeModifiers()
    {
        modifiers.Add(new TileRoomRowCount());
        modifiers.Add(new TileRoomColumnCount());
        modifiers.Add(new TileRoomRepairKitCount());
        modifiers.Add(new TileRoomExtraEffect());
        modifiers.Add(new TileRoomStrayTilesCount());
        modifiers.Add(new TileRoomDoorwayWidth());
        modifiers.Add(new TileRoomWallWidth());
        modifiers.Add(new TileRoomEasyShaders());
        modifiers.Add(new TileRoomHardShaders());
    }
}
