public class TileRoomRowCount : RoomModifier
{
    public TileRoomRowCount(int weight, int weightDecay, int cost)
    {
        this.weight = weight;
        this.weightDecay = weightDecay;
        this.cost = cost;
    }
}
