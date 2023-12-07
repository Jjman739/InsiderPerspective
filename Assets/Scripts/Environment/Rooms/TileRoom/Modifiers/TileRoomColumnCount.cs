public class TileRoomColumnCount : RoomModifier
{
    public TileRoomColumnCount(int weight, int weightDecay, int cost)
    {
        this.weight = weight;
        this.weightDecay = weightDecay;
        this.cost = cost;
    }
}
