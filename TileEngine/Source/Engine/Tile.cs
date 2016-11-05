using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class Tile : GameObject
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
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Tile string conversion methods
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
        // XNA Methods
        public override void Update(GameTime gameTime)
        {
            try
            {
                base.Update(gameTime);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public override void Draw()
        {
            try
            {
                Engine.SpriteBatch.Draw(Tile.TileSet, position_Draw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);

                #region // Debugging
                // Ready for debugging implementation
                //if (Engine.VisualDebugger && type == TileType.Solid)
                //{
                //   // Reposition this for the right texture!
                //   Engine.SpriteBatch.Draw(Tile.TileSet, positionDraw, new Rectangle((int)(0 * Engine.TileDimensions.Y), (int)(9 * Engine.TileDimensions.Y), (int)Engine.TileDimensions.X, (int)Engine.TileDimensions.Y), Color.Purple, rotation, origin, scale, spriteEffect, 0.1f);
                //}
                #endregion
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Copy Method
        public void Copy(Tile tileToCopy)
        {
            this.tag = tileToCopy.tag;
            this.sourceRectangle_Position = tileToCopy.sourceRectangle_Position;
            this.colour = tileToCopy.colour;
            this.id = tileToCopy.id;
            this.type = tileToCopy.type;
        }
    }
}
