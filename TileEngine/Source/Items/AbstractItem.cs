
namespace TileEngine
{
    public abstract class AbstractItem : AbstractGameObject
    {
        public const int minDurability = 0;
        public int maxDurability { get; private set; }
        public int currentDurability { get; private set; }
        public int buyValue { get; private set; }
        public int sellValue { get; private set; }

        public abstract void Degrade(int durabilityLoss);
        public abstract void Repair();
        public abstract void Buy();
        public abstract void Sell();
    }
}