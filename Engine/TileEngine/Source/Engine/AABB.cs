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
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        

        // Vector versions of the raw data
        public Vector2 size { get { return new Vector2(Width, Height); } }
        public Vector2 position { get { return new Vector2(X, Y); } }

        // Pixel Positions
        public Vector2 position_TopLeft { get { return position; } }
        public Vector2 position_TopRight { get { return position + new Vector2(Width, 0); } }
        public Vector2 position_BottomLeft { get { return position + new Vector2(0, Height); } }
        public Vector2 position_BottomRight { get { return position + new Vector2(Width , Height); } }
        // Grid Positions
        public Vector2 gridPosition { get { return Engine.ConvertPosition_PixelToGrid(position); } }
        public Vector2 gridPosition_TopLeft { get { return Engine.ConvertPosition_PixelToGrid(position_TopLeft); } }
        public Vector2 gridPosition_TopRight { get { return Engine.ConvertPosition_PixelToGrid(position_TopRight); } }
        public Vector2 gridPosition_BottomLeft { get { return Engine.ConvertPosition_PixelToGrid(position_BottomLeft); } }
        public Vector2 gridPosition_BottomRight { get { return Engine.ConvertPosition_PixelToGrid(position_BottomRight); } }

        // Constructors
        public AABB(float x, float y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
        public AABB(Vector2 position, Vector2 size)
        {
            this.X = position.X;
            this.Y = position.Y;
            this.Width = (int)size.X;
            this.Height = (int)size.Y;
        }

        // Methods
        public void SetPosition(Vector2 newPosition)
        {
            this.X = position.X;
            this.Y = position.Y;
        }
        public void SetSize(Vector2 newSize)
        {
            this.Width = (int)newSize.X;
            this.Height = (int)newSize.Y;
        }
        public bool Intersects(AABB otherAABB)
        {
            try
            {
                if (Engine.GetCurrentLevel().IsTileEmpty(otherAABB.gridPosition))
                {
                    return false;
                }
                
                if (Math.Abs(position.X - otherAABB.X) * 2 < (Width + otherAABB.Width)) return true;
                if (Math.Abs(position.Y - otherAABB.Y) * 2 < (Height + otherAABB.Height)) return true;
                return false;
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
