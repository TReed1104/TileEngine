using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    [DebuggerDisplay("{X}, {Y}")]
    public class AABB
    {
        // Raw Data of the AABB.
        private Rectangle baseRectangle { get; set; }
        public int X { get { return baseRectangle.X; } }
        public int Y { get { return baseRectangle.Y; } }
        public int Width { get { return baseRectangle.Width; } }
        public int Height { get { return baseRectangle.Height; } }
        public int LeftPixel { get { return baseRectangle.Left; } }
        public int RightPixel { get { return baseRectangle.Right; } }
        public int TopPixel { get { return baseRectangle.Top; } }
        public int BottomPixel { get { return baseRectangle.Bottom; } }
        public bool isEmpty { get { return baseRectangle.IsEmpty; } }
        

        // Vector versions of the raw data
        public Vector2 size { get { return new Vector2(Width, Height); } }
        public Vector2 position { get { return new Vector2(X, Y); } }

        // Pixel Positions
        public Vector2 position_TopLeft { get { return position; } }
        public Vector2 position_TopRight { get { return position + new Vector2(Width - 1, 0); } }
        public Vector2 position_BottomLeft { get { return position + new Vector2(0, Height - 1); } }
        public Vector2 position_BottomRight { get { return position + new Vector2(Width - 1, Height - 1); } }
        // Grid Positions
        public Vector2 gridPosition { get { return Engine.ConvertPosition_PixelToGrid(position); } }
        public Vector2 gridPosition_TopLeft { get { return Engine.ConvertPosition_PixelToGrid(position_TopLeft); } }
        public Vector2 gridPosition_TopRight { get { return Engine.ConvertPosition_PixelToGrid(position_TopRight); } }
        public Vector2 gridPosition_BottomLeft { get { return Engine.ConvertPosition_PixelToGrid(position_BottomLeft); } }
        public Vector2 gridPosition_BottomRight { get { return Engine.ConvertPosition_PixelToGrid(position_BottomRight); } }

        // Constructors
        public AABB(int x, int y, int width, int height)
        {
            baseRectangle = new Rectangle(x, y, width, height);
        }
        public AABB(Vector2 position, Vector2 size)
        {
            baseRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }

        // Methods
        public void SetPosition(int newX, int newY)
        {
            baseRectangle = new Rectangle(newX, newY, baseRectangle.Width, baseRectangle.Height);
        }
        public void SetPosition(Vector2 newPosition)
        {
            baseRectangle = new Rectangle((int)Math.Round(newPosition.X), (int)Math.Round(newPosition.Y), baseRectangle.Width, baseRectangle.Height);
        }
        public void SetSize(int newWidth, int newHeight)
        {
            baseRectangle = new Rectangle(baseRectangle.X, baseRectangle.Y, newWidth, newHeight);
        }
        public void SetSize(Vector2 newSize)
        {
            baseRectangle = new Rectangle(baseRectangle.X, baseRectangle.Y, (int)newSize.X, (int)newSize.Y);
        }
        public bool Intersects(AABB otherAABB)
        {
            try
            {
                if (Engine.GetCurrentLevel().IsTileEmpty(otherAABB.gridPosition))
                {
                    return false;
                }
                return baseRectangle.Intersects(otherAABB.baseRectangle);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return true;
            }
        }
    }
}
