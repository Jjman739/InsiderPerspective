public class PlatformerRoomModifiers : RoomModifiers
{
    protected override void initializeModifiers()
    {
        modifiers.Add(new RepairKitCount(15, 5, 3));
        modifiers.Add(new EasyShaders(10, 2, 10));
        modifiers.Add(new HardShaders(4, 4, 10));
        modifiers.Add(new ExtraTrapEffect(5, 5, 10));
    }
}
