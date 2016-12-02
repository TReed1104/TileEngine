using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class Tile : BaseGameObject
    {
        // Enums
        public enum TileType { Empty, Solid, Water, Ice, Fire, Pitfall, }
        // Vars
        public static Vector2 Dimensions { get; set; }
        public static Texture2D TileSet { get; set;}
        public static string TileSetTags { get; set; }
        public int id { get; set; }
        public TileType type { get; set; }

        // Constructors
        static Tile()
        {
            Tile.Dimensions = new Vector2(16, 16);
            Tile.TileSet = null;
            Tile.TileSetTags = "";
        }
        public Tile(string tag, Vector2 sourceRectangle_Position, Color colour, float layerDepth, int id, TileType type)
            : base (tag, null, Vector2.Zero, sourceRectangle_Position, Tile.Dimensions, colour, layerDepth)
        {
            try
            {
                this.id = id;
                this.type = type;
                texture = TileSet;
                if (type == TileType.Solid)
                {
                    boundingBox_Size = new Vector2(Tile.Dimensions.X, Tile.Dimensions.Y);
                }
                else
                {
                    boundingBox_Size = new Vector2(0, 0);
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }

        // Methods
        public override void Update(GameTime gameTime)
        {
        }
        public static TileType Register_ConvertTileType(string stringToConvert)
        {
            try
            {
                switch (stringToConvert)
                {
                    case "00":
                        return TileType.Empty;
                    case "01":
                        return TileType.Solid;
                    case "02":
                        return TileType.Water;
                    case "03":
                        return TileType.Ice;
                    case "04":
                        return TileType.Fire;
                    case "05":
                        return TileType.Pitfall;
                    default:
                        return TileType.Empty;
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Tile", methodName, error.Message));
                return TileType.Empty;
            }
        }
        public void Copy(Tile tileToCopy)
        {
            this.tag = tileToCopy.tag;
            this.sourceRectangle_Position = tileToCopy.sourceRectangle_Position;
            this.colour = tileToCopy.colour;
            this.id = tileToCopy.id;
            this.type = tileToCopy.type;
            if (type == TileType.Solid)
            {
                boundingBox_Size = new Vector2(Tile.Dimensions.X, Tile.Dimensions.Y);
            }
            else
            {
                boundingBox_Size = new Vector2(0, 0);
            }
        }

    }
}
