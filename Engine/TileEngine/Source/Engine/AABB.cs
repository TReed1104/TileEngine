using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    [DebuggerDisplay("{x}, {y}")]
    public class AABB
    {
        // Raw Data of the AABB.
        public float x { get; set; }
        public float y { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        

        // Vector versions of the raw data
        public Vector2 size { get { return new Vector2(width, height); } }
        public Vector2 position { get { return new Vector2(x, y); } }

        // Pixel Positions
        public Vector2 position_TopLeft { get { return position; } }
        public Vector2 position_TopRight { get { return position + new Vector2(width, 0); } }
        public Vector2 position_BottomLeft { get { return position + new Vector2(0, height); } }
        public Vector2 position_BottomRight { get { return position + new Vector2(width , height); } }
        // Grid Positions
        public Vector2 gridPosition { get { return Engine.ConvertPosition_PixelToGrid(position); } }
        public Vector2 gridPosition_TopLeft { get { return Engine.ConvertPosition_PixelToGrid(position_TopLeft); } }
        public Vector2 gridPosition_TopRight { get { return Engine.ConvertPosition_PixelToGrid(position_TopRight); } }
        public Vector2 gridPosition_BottomLeft { get { return Engine.ConvertPosition_PixelToGrid(position_BottomLeft); } }
        public Vector2 gridPosition_BottomRight { get { return Engine.ConvertPosition_PixelToGrid(position_BottomRight); } }

        // Constructors
        public AABB(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public AABB(Vector2 position, Vector2 size)
        {
            this.x = position.X;
            this.y = position.Y;
            this.width = size.X;
            this.height = size.Y;
        }

        // Methods
        public void SetPosition(Vector2 newPosition)
        {
            this.x = position.X;
            this.y = position.Y;
        }
        public void SetSize(Vector2 newSize)
        {
            this.width = newSize.X;
            this.height = newSize.Y;
        }
        public bool CheckForCollisionWith(AABB otherAABB)
        {
            try
            {
                if (Engine.GetCurrentLevel().IsTileEmpty(otherAABB.gridPosition)) { return false; }

                bool isColliding = ((x < otherAABB.x + otherAABB.width) && (x + otherAABB.width > otherAABB.x) && (y < otherAABB.y + otherAABB.height) && (y + height > otherAABB.y));

                // If the cell is solid, find out if this AABB is colliding with the passed AABB.
                return isColliding;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return true;    // If something goes wrong, return that a collision has occured.
            }
        }
    }
}
