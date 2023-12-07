public class PlatformerRoomModifiers : RoomDifficultyBuilder
{
    protected override void initializeModifiers()
    {
        modifiers.Add(new RepairKitCount(15, 5, 3));
        modifiers.Add(new EasyShaders(10, 2, 10));
        modifiers.Add(new HardShaders(4, 4, 10));
    }
}
