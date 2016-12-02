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
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // Useful self-calculating Vars of the AABB.
        public Vector2 size { get { return new Vector2(Width, Height); } }
        public Vector2 position { get { return new Vector2(X, Y); } }
        public Vector2 position_TopLeft { get { return position; } }
        public Vector2 position_TopRight { get { return position + new Vector2((Width - 1), 0); } }
        public Vector2 position_BottomLeft { get { return position + new Vector2(0, (Height - 1)); } }
        public Vector2 position_BottomRight { get { return position + new Vector2((Width - 1), (Height - 1)); } }
        public Vector2 gridPosition { get { return Engine.ConvertPosition_PixelToGrid(position); } }
        public Vector2 gridPosition_TopLeft { get { return Engine.ConvertPosition_PixelToGrid(position_TopLeft); } }
        public Vector2 gridPosition_TopRight { get { return Engine.ConvertPosition_PixelToGrid(position_TopRight); } }
        public Vector2 gridPosition_BottomLeft { get { return Engine.ConvertPosition_PixelToGrid(position_BottomLeft); } }
        public Vector2 gridPosition_BottomRight { get { return Engine.ConvertPosition_PixelToGrid(position_BottomRight); } }

        // Constructors
        public AABB(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
        public AABB(Vector2 position, Vector2 size)
        {
            this.X = (int)position.X;
            this.Y = (int)position.Y;
            this.Width = (int)size.X;
            this.Height = (int)size.Y;
        }

        // Methods
        public void SetPosition(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
        public void SetPosition(Vector2 newPosition)
        {
            X = (int)newPosition.X;
            Y = (int)newPosition.Y;
        }
        public void SetSize(int newWidth, int newHeight)
        {
            Width = newWidth;
            Height = newHeight;
        }
        public void SetSize(Vector2 newSize)
        {
            Width = (int)newSize.X;
            Height = (int)newSize.Y;
        }
        public bool Intersects(AABB otherAABB)
        {
            try
            {
                if (otherAABB.size == Vector2.Zero)
                {
                    return false;
                }

                int deltaX = X - otherAABB.X;
                int deltaY = Y - otherAABB.Y;
                if (deltaY <= otherAABB.Height &&
                    deltaX <= otherAABB.Width)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                //Rectangle thisRect = new Rectangle(X, Y, Width, Height);
                //Rectangle otherRect = new Rectangle(otherAABB.X, otherAABB.Y, otherAABB.Width, otherAABB.Height);
                //bool isColliding = thisRect.Intersects(otherRect);

                //return isColliding;
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
