using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TileEngine
{
    [DebuggerDisplay("{tag}")]
    public abstract class AbstractGameObject
    {
        // Vars
        public string src { get; set; }
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

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
    }
}
