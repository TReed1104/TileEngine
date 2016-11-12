using System.Collections.Generic;

namespace TileEngine
{
    public class Inventory
    {
        public int size { get; set; }
        public List<AbstractItem> contents { get; set; }

        static Inventory()
        {
            
        }
        public Inventory(int baseSize)
        {
            this.size = baseSize;
            this.contents = new List<AbstractItem>();
        }

        public bool storeItem(AbstractItem item)
        {
            if (contents.Count < size)
            {
                contents.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void removeItem(string itemTag)
        {

        }
    }
}