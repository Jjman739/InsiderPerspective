public class TileRoomWallWidth : RoomModifier
{
    public TileRoomWallWidth(int weight, int weightDecay, int cost)
    {
        this.weight = weight;
        this.weightDecay = weightDecay;
        this.cost = cost;
    }
}
