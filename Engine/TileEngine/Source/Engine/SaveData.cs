using Microsoft.Xna.Framework;

namespace TileEngine
{
    public class SaveData
    {
        // Vars
        public string tag { get; set; }
        public Vector2 position { get; set; }
        public float health { get; set; }
        public int gold { get; set; }

        // Constructors
        public SaveData(string tag, Vector2 position, float hp, int gold)
        {
            this.tag = tag;
            this.position = position;
            this.health = hp;
            this.gold = gold;
        }
    }
}
