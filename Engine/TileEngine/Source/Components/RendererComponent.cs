using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class RendererComponent : AbstractComponent
    {
        // Vars
        public Texture2D texture { get; set; }
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
        public RendererComponent(string entityTag, Texture2D texture, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth) : base("Render Component", entityTag)
        {
            this.texture = texture;
            this.sourceRectangle_Position = sourceRectangle_Position;
            this.sourceRectangle_Size = sourceRectangle_Size;
            this.sourceRectangle_Offset = Vector2.Zero;
            this.colour = colour;
            this.rotation = 0.0f;
            this.origin = Vector2.Zero;
            this.scale = 1.0f;
            this.spriteEffect = SpriteEffects.None;
            this.layerDepth = layerDepth;
        }

        public override void Execute(BaseGameObject gameObject)
        {
            Engine.SpriteBatch.Draw(texture, gameObject.positionComponent.position_Draw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);
        }
    }
}
