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
        // Vars
        public static Texture2D TileSet { get; set;}
        public int id { get; set; }
        public enum TileType { Empty, Solid, Water, Ice, Fire, Pitfall, }
        public TileType type { get; set; }

        // Constructors
        static Tile()
        {

        }
        public Tile(string tag, Vector2 sourceRectangle_Position, Color colour, float layerDepth, int id, TileType type)
            : base (tag, null, Vector2.Zero, sourceRectangle_Position, Engine.TileDimensions, colour, layerDepth)
        {
            this.id = id;
            this.type = type;
        }
        public Tile(Tile tileToCopy) 
            : base (tileToCopy.tag, null, tileToCopy.position_Base, tileToCopy.sourceRectangle_Position, Engine.TileDimensions, tileToCopy.colour, tileToCopy.layerDepth)
        {
            this.id = tileToCopy.id;
            this.type = tileToCopy.type;
        }

        // Methods
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
    }
}
