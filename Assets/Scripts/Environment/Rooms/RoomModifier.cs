public abstract class RoomModifier
{
    protected int weight;
    protected int weightDecay;
    protected int cost;
    protected int points = 0;
    protected int level { get { return points / cost; } }
    public int GetWeight() { return weight; }
    public int GetCost() { return cost; }
    public int GetPoints() { return points; }
    public int GetLevel() { return points / cost; }
    public void LevelUp()
    {
        points += cost;
        weight -= weightDecay;
    }
}
