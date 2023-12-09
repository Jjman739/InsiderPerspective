public class TileRoomModifiers : RoomModifiers
{
    protected override void initializeModifiers()
    {
        modifiers.Add(new TileRoomRowCount(20, 10, 10));
        modifiers.Add(new TileRoomColumnCount(20, 10, 10));
        modifiers.Add(new RepairKitCount(15, 5, 1));
        modifiers.Add(new ExtraTrapEffect(5, 5, 15));
        modifiers.Add(new TileRoomStrayTilesCount(10, 2, 2));
        modifiers.Add(new TileRoomDoorwayWidth(18, 3, 3));
        modifiers.Add(new TileRoomWallWidth(24, 2, 5));
        modifiers.Add(new EasyShaders(20, 2, 2));
        modifiers.Add(new HardShaders(8, 4, 10));
    }
}
