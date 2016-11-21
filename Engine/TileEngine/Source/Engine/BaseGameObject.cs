using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    [DebuggerDisplay("{tag}")]
    public abstract class BaseGameObject
    {
        // Vars
        public string tag { get; set; }
        public Vector2 position_Base { get; set; }
        public Vector2 position_Grid { get; set; }
        public Vector2 position_Draw { get; set; }
        public Texture2D texture { get; set; }
        public string textureTag { get; set; }
        public Color colour { get; set; }
        public Vector2 sourceRectangle_Position { get; set; }
        public Vector2 sourceRectangle_Size { get; set; }
        public Vector2 sourceRectangle_Offset { get; set; }
        public Rectangle sourceRectangle
        {
            get
            {
                int x = (int)((sourceRectangle_Position.X * sourceRectangle_Size.X) + sourceRectangle_Offset.X);
                int y = (int)((sourceRectangle_Position.Y * sourceRectangle_Size.Y) + sourceRectangle_Offset.Y);
                int width = (int)sourceRectangle_Size.X;
                int height = (int)sourceRectangle_Size.Y;
                return new Rectangle(x, y, width, height);
            }
        }
        public Vector2 origin { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public SpriteEffects spriteEffect { get; set; }
        public float layerDepth { get; set; }

        // Constructors
        static BaseGameObject()
        {

        }
        public BaseGameObject(string tag, Texture2D texture, Vector2 position_Base, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth)
        {
            try
            {
                this.tag = tag;
                this.texture = texture;
                this.position_Base = position_Base;
                int gridX = (int)(this.position_Base.X / Tile.Dimensions.X);
                int gridY = (int)(this.position_Base.Y / Tile.Dimensions.Y);
                this.position_Grid = new Vector2(gridX, gridY);
                this.position_Draw = position_Base + Engine.PlayerCamera.position_Base;
                this.sourceRectangle_Position = sourceRectangle_Position;
                this.sourceRectangle_Size = sourceRectangle_Size;
                this.sourceRectangle_Offset = Vector2.Zero;
                this.colour = colour;
                this.origin = Vector2.Zero;
                this.rotation = 0.0f;
                this.scale = 1.0f;
                this.spriteEffect = SpriteEffects.None;
                this.layerDepth = layerDepth;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // XNA Methods
        public abstract void Update(GameTime gameTime);
        public virtual void Draw()
        {
            try
            {
                Engine.SpriteBatch.Draw(texture, position_Draw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
    }
}
