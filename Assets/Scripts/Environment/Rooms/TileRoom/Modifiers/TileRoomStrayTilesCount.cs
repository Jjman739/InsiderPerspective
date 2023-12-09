public class TileRoomStrayTilesCount : RoomModifier
{
    public TileRoomStrayTilesCount(int weight, int weightDecay, int cost)
    {
        this.weight = weight;
        this.weightDecay = weightDecay;
        this.cost = cost;
    }
}
