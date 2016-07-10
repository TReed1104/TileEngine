using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class GameObject
    {
        // Vars
        public string tag { get; set; }
        public Vector2 positionBase { get; set; }
        public Vector2 positionGrid { get; set; }
        public Vector2 positionDraw { get; set; }
        public Texture2D texture { get; set; }
        public Color colour { get; set; }
        public Vector2 sourceRectanglePosition { get; set; }
        public Vector2 sourceRectangleSize { get; set; }
        public Vector2 sourceRectangleOffset { get; set; }
        public Rectangle sourceRectangle
        {
            get
            {
                int x = (int)((sourceRectanglePosition.X * sourceRectangleSize.X) + sourceRectangleOffset.X);
                int y = (int)((sourceRectanglePosition.Y * sourceRectangleSize.Y) + sourceRectangleOffset.Y);
                int width = (int)sourceRectangleSize.X;
                int height = (int)sourceRectangleSize.Y;
                return new Rectangle(x, y, width, height);
            }
        }
        public Vector2 origin { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public SpriteEffects spriteEffect { get; set; }
        public float layerDepth { get; set; }

        // Constructors
        static GameObject()
        {

        }
        public GameObject(string tag, Texture2D texture, Vector2 positionBase, Vector2 sourceRectanglePosition, Vector2 sourceRectangleSize, Color colour, float layerDepth)
        {
            try
            {
                this.tag = tag;
                this.texture = texture;
                this.positionBase = positionBase;
                int gridX = (int)(this.positionBase.X / Engine.TileDimensions.X);
                int gridY = (int)(this.positionBase.Y / Engine.TileDimensions.Y);
                this.positionGrid = new Vector2(gridX, gridY);
                this.positionDraw = positionBase + Engine.PlayerCamera.positionBase;
                this.sourceRectanglePosition = sourceRectanglePosition;
                this.sourceRectangleSize = sourceRectangleSize;
                this.sourceRectangleOffset = Vector2.Zero;
                this.colour = colour;
                this.origin = Vector2.Zero;
                this.rotation = 0.0f;
                this.scale = 0.0f;
                this.spriteEffect = SpriteEffects.None;
                this.layerDepth = layerDepth;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }

        // Methods
        public virtual void Update(GameTime gameTime)
        {
            try
            {
                positionDraw = positionBase + Engine.PlayerCamera.positionBase;
                int gridX = (int)(this.positionBase.X / Engine.TileDimensions.X);
                int gridY = (int)(this.positionBase.Y / Engine.TileDimensions.Y);
                this.positionGrid = new Vector2(gridX, gridY);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public virtual void Draw()
        {
            try
            {
                Engine.SpriteBatch.Draw(texture, positionDraw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
    }
}
