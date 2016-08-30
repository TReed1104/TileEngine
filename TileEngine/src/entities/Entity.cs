using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public abstract class Entity : GameObject
    {
        // Enums
        public enum Direction { Down, Up, Left, Right, UpLeft, UpRight, DownLeft, DownRight };
        public enum WallSlide { None, WallSlideLeft, WallSlideRight, WallSlideUp, WallSlideDown, };
        // Vars
        public string spriteSheetSource { get; set; }
        public float healthPoints { get; protected set; }
        public float damagePower { get; protected set; }
        public float movementSpeed { get; protected set; }
        public Vector2 velocity { get; set; }
        public Direction direction { get; protected set; }
        public Direction spriteDirection { get; protected set; }
        public WallSlide wallSlide { get; set; }
        protected Vector2 boundingBox_Offset { get; set; }
        public Rectangle boundingBox_AABB { get; set; }
        protected Vector2 AABB_TopLeft_GridPosition;
        protected Vector2 AABB_TopRight_GridPosition;
        protected Vector2 AABB_BottomLeft_GridPosition;
        protected Vector2 AABB_BottomRight_GridPosition;
        protected Vector2 AABB_Center_GridPosition;
        protected Vector2 newGridPosition;
        protected Vector2 newGridPositionOffset;
        protected Vector2 newGridPositionOffsetDiagonal;
        protected float deltaTime { get; set; }

        // Constructors
        static Entity()
        {

        }
        public Entity(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base (tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {
            deltaTime = 0;
            this.healthPoints = healthPoints;
            velocity = Vector2.Zero;
            movementSpeed = 1.0f;
            damagePower = 1.0f;
            direction = Direction.Down;
            wallSlide = WallSlide.None;

            Vector2 boundingSize = new Vector2(10, 10);
            Vector2 boundingGridDelta = Tile.TileDimensions - boundingSize;
            boundingBox_Offset = (boundingGridDelta / 2) + Tile.TileDimensions;

            position_Grid = new Vector2((int)(position_Draw.X / Tile.TileDimensions.X), (int)(position_Draw.Y / Tile.TileDimensions.Y));
            boundingBox_AABB = new Rectangle((int)(position_Grid.X * Tile.TileDimensions.X) + (int)boundingBox_Offset.X, (int)(position_Grid.Y * Tile.TileDimensions.Y) + (int)boundingBox_Offset.Y, (int)boundingSize.X, (int)boundingSize.Y);
            newGridPosition = Vector2.Zero;
            newGridPositionOffset = Vector2.Zero;
            newGridPositionOffsetDiagonal = Vector2.Zero;
            layerDepth = Engine.LayerDepth_NPC;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateAABBCorners();
            MovementControl();
            velocity = Vector2.Zero;
            UpdateSpriteDirection();
            position_Draw = new Vector2(boundingBox_AABB.X - boundingBox_Offset.X, boundingBox_AABB.Y - boundingBox_Offset.Y) + Engine.Window_GameRender_Offset;
            position_Base = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y);
        }
        public override void Draw()
        {
            Engine.SpriteBatch.Draw(texture, position_Draw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);
        }
        protected void UpdateSpriteDirection()
        {
            try
            {
                float sourceY = sourceRectangle_Position.Y;
                switch (spriteDirection)
                {
                    case Direction.Down: { sourceRectangle_Position = new Vector2(1, sourceY); break; }
                    case Direction.Up: { sourceRectangle_Position = new Vector2(0, sourceY); break; }
                    case Direction.Right: { sourceRectangle_Position = new Vector2(3, sourceY); break; }
                    case Direction.Left: { sourceRectangle_Position = new Vector2(2, sourceY); break; }
                    case Direction.DownRight: { sourceRectangle_Position = new Vector2(7, sourceY); break; }
                    case Direction.DownLeft: { sourceRectangle_Position = new Vector2(6, sourceY); break; }
                    case Direction.UpRight: { sourceRectangle_Position = new Vector2(5, sourceY); break; }
                    case Direction.UpLeft: { sourceRectangle_Position = new Vector2(4, sourceY); break; }
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected void UpdateAABBCorners()
        {
            try
            {
                AABB_TopLeft_GridPosition = new Vector2((float)Math.Floor(boundingBox_AABB.X / Tile.TileDimensions.X), (float)Math.Floor(boundingBox_AABB.Y / Tile.TileDimensions.Y));
                AABB_TopRight_GridPosition = new Vector2((float)Math.Floor((boundingBox_AABB.X + (boundingBox_AABB.Width - 1)) / Tile.TileDimensions.X), (float)Math.Floor(boundingBox_AABB.Y / Tile.TileDimensions.Y));
                AABB_BottomLeft_GridPosition = new Vector2((float)Math.Floor(boundingBox_AABB.X / Tile.TileDimensions.X), (float)Math.Floor((boundingBox_AABB.Y + (boundingBox_AABB.Height - 1)) / Tile.TileDimensions.Y));
                AABB_BottomRight_GridPosition = new Vector2((float)Math.Floor((boundingBox_AABB.X + (boundingBox_AABB.Width - 1)) / Tile.TileDimensions.X), (float)Math.Floor((boundingBox_AABB.Y + (boundingBox_AABB.Height - 1)) / Tile.TileDimensions.Y));
                AABB_Center_GridPosition = new Vector2((float)Math.Floor((boundingBox_AABB.X + (boundingBox_AABB.Width / 2) - 1) / Tile.TileDimensions.X), (float)Math.Floor((boundingBox_AABB.Y + (boundingBox_AABB.Height / 2) - 1) / Tile.TileDimensions.Y));
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected virtual void CollisionCheckTileMap(Vector2 newPosition)
        {
            try
            {
                // The method which sets the next grid position of the Player.
                // The new position post movement.
                newGridPosition = new Vector2((float)Math.Floor(newPosition.X / Tile.TileDimensions.X), (float)Math.Floor(newPosition.Y / Tile.TileDimensions.Y));

                // The Offset of the overlapped cells compared to the new position.
                Vector2 cellOffSet = Vector2.Zero;
                Vector2 cellOffSetDiagonal = Vector2.Zero;

                // Cell Overlap handling.
                #region Left and Right Overlap Checks hidden here.
                if ((direction == Direction.Left || direction == Direction.Right))
                {
                    if (AABB_TopLeft_GridPosition.Y != AABB_BottomLeft_GridPosition.Y)
                    {
                        cellOffSet = new Vector2(0, 1);
                    }

                }
                #endregion
                #region Up and Down Overlap Checks hidden here.
                if ((direction == Direction.Up || direction == Direction.Down))
                {
                    if (AABB_TopLeft_GridPosition.X != AABB_TopRight_GridPosition.X)
                    {
                        cellOffSet = new Vector2(1, 0);
                    }
                }
                #endregion

                // Wall Sliding.
                #region Up-Left Overlap Checks hidden here.
                // Up-Left
                if (direction == Direction.UpLeft)
                {
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopLeft_GridPosition.Y == AABB_BottomLeft_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_TopLeft_GridPosition + new Vector2(0, -1)))
                        {
                            cellOffSet = new Vector2(-1, 0);
                            cellOffSetDiagonal = new Vector2(-1, 1);
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                wallSlide = WallSlide.WallSlideUp;
                            }
                            else
                            {
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                        }
                        if (newGridPosition == (AABB_TopLeft_GridPosition + new Vector2(-1, -1)))
                        {
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(1, 0)) &&
                                !Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideUp;
                            }
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(1, 0)) &&
                                !Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                        }
                        if (newGridPosition == (AABB_TopLeft_GridPosition + new Vector2(-1, 0)))
                        {
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                cellOffSet = new Vector2(0, -1);
                                cellOffSetDiagonal = new Vector2(1, -1);
                                wallSlide = WallSlide.WallSlideUp;
                            }
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X != AABB_TopRight_GridPosition.X && AABB_TopLeft_GridPosition.Y == AABB_BottomLeft_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_TopLeft_GridPosition + new Vector2(0, -1)))
                        {
                            cellOffSet = new Vector2(1, 0);
                            cellOffSetDiagonal = new Vector2(0, 1);
                            wallSlide = WallSlide.WallSlideLeft;
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopLeft_GridPosition.Y != AABB_BottomLeft_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_TopLeft_GridPosition + new Vector2(-1, 0)))
                        {
                            cellOffSet = new Vector2(1, 0);
                            cellOffSetDiagonal = new Vector2(0, 1);
                            wallSlide = WallSlide.WallSlideUp;
                        }
                    }
                }
                #endregion
                #region Up-Right Overlap Checks hidden here.
                // Up-Right
                if (direction == Direction.UpRight)
                {
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopRight_GridPosition.Y == AABB_BottomRight_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_TopRight_GridPosition + new Vector2(0, -1)))
                        {
                            cellOffSet = new Vector2(1, 0);
                            cellOffSetDiagonal = new Vector2(1, 1);
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                wallSlide = WallSlide.WallSlideUp;
                            }
                            else
                            {
                                wallSlide = WallSlide.WallSlideRight;
                            }
                        }
                        if (newGridPosition == (AABB_TopRight_GridPosition + new Vector2(1, -1)))
                        {
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(-1, 0)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideUp;
                            }
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(-1, 0)) &&
                                !Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                        }
                        if (newGridPosition == (AABB_TopRight_GridPosition + new Vector2(1, 0)))
                        {
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                cellOffSet = new Vector2(0, -1);
                                cellOffSetDiagonal = new Vector2(-1, -1);
                                wallSlide = WallSlide.WallSlideUp;
                            }
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X != AABB_TopRight_GridPosition.X && AABB_TopRight_GridPosition.Y == AABB_BottomRight_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_TopRight_GridPosition + new Vector2(0, -1)))
                        {
                            cellOffSet = new Vector2(-1, 0);
                            cellOffSetDiagonal = new Vector2(0, 1);
                            wallSlide = WallSlide.WallSlideRight;
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopRight_GridPosition.Y != AABB_BottomRight_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_TopRight_GridPosition + new Vector2(1, 0)))
                        {
                            cellOffSet = new Vector2(-1, 0);
                            cellOffSetDiagonal = new Vector2(0, 1);
                            wallSlide = WallSlide.WallSlideUp;
                        }
                    }
                }
                #endregion
                #region Down-Left Overlap Checks hidden here.
                // Down-Left
                if (direction == Direction.DownLeft)
                {
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopLeft_GridPosition.Y == AABB_BottomLeft_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_BottomLeft_GridPosition + new Vector2(0, 1)))
                        {
                            cellOffSet = new Vector2(-1, 0);
                            cellOffSetDiagonal = new Vector2(-1, -1);
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                wallSlide = WallSlide.WallSlideDown;
                            }
                            else
                            {
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                        }
                        if (newGridPosition == (AABB_BottomLeft_GridPosition + new Vector2(-1, 1)))
                        {
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(1, 0)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideDown;
                            }
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(1, 0)) &&
                                !Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                        }
                        if (newGridPosition == (AABB_BottomLeft_GridPosition + new Vector2(-1, 0)))
                        {
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                cellOffSet = new Vector2(0, 1);
                                cellOffSetDiagonal = new Vector2(1, -1);
                                wallSlide = WallSlide.WallSlideDown;
                            }
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X != AABB_TopRight_GridPosition.X && AABB_TopLeft_GridPosition.Y == AABB_BottomLeft_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_BottomLeft_GridPosition + new Vector2(0, 1)))
                        {
                            cellOffSet = new Vector2(1, 0);
                            cellOffSetDiagonal = new Vector2(0, -1);
                            wallSlide = WallSlide.WallSlideLeft;
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopLeft_GridPosition.Y != AABB_BottomLeft_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_BottomLeft_GridPosition + new Vector2(-1, 0)))
                        {
                            cellOffSet = new Vector2(1, 0);
                            cellOffSetDiagonal = new Vector2(0, -1);
                            wallSlide = WallSlide.WallSlideDown;
                        }
                    }
                }
                #endregion
                #region Down-Right Overlap Checks hidden here.
                // Down-Right
                if (direction == Direction.DownRight)
                {
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopRight_GridPosition.Y == AABB_BottomRight_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_BottomRight_GridPosition + new Vector2(0, 1)))
                        {
                            cellOffSet = new Vector2(1, 0);
                            cellOffSetDiagonal = new Vector2(1, -1);
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                wallSlide = WallSlide.WallSlideDown;
                            }
                            else
                            {
                                wallSlide = WallSlide.WallSlideRight;
                            }
                        }
                        if (newGridPosition == (AABB_BottomRight_GridPosition + new Vector2(1, 1)))
                        {
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(-1, 0)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideDown;
                            }
                            if (Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(-1, 0)) &&
                                !Engine.GetCurrentZone().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                        }
                        if (newGridPosition == (AABB_BottomRight_GridPosition + new Vector2(1, 0)))
                        {
                            if (!Engine.GetCurrentZone().IsTileSolid(newGridPosition))
                            {
                                cellOffSet = new Vector2(0, 1);
                                cellOffSetDiagonal = new Vector2(-1, 1);
                                wallSlide = WallSlide.WallSlideDown;
                            }
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X != AABB_TopRight_GridPosition.X && AABB_TopRight_GridPosition.Y == AABB_BottomRight_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_BottomRight_GridPosition + new Vector2(0, 1)))
                        {
                            cellOffSet = new Vector2(-1, 0);
                            cellOffSetDiagonal = new Vector2(0, -1);
                            wallSlide = WallSlide.WallSlideRight;
                        }
                    }
                    if (AABB_TopLeft_GridPosition.X == AABB_TopRight_GridPosition.X && AABB_TopRight_GridPosition.Y != AABB_BottomRight_GridPosition.Y)
                    {
                        if (newGridPosition == (AABB_BottomRight_GridPosition + new Vector2(1, 0)))
                        {
                            cellOffSet = new Vector2(-1, 0);
                            cellOffSetDiagonal = new Vector2(0, -1);
                            wallSlide = WallSlide.WallSlideDown;
                        }
                    }
                }
                #endregion

                newGridPositionOffset = newGridPosition + cellOffSet;
                newGridPositionOffsetDiagonal = newGridPosition + cellOffSetDiagonal;

                // Check the collisions.
                if (Engine.GetCurrentZone().IsTileSolid(newGridPosition) &&
                    Engine.GetCurrentZone().IsTileSolid(newGridPositionOffset) &&
                    Engine.GetCurrentZone().IsTileSolid(newGridPositionOffsetDiagonal))
                {
                    boundingBox_AABB = new Rectangle((int)(boundingBox_AABB.X + velocity.X), (int)(boundingBox_AABB.Y + velocity.Y), boundingBox_AABB.Width, boundingBox_AABB.Height);
                    position_Grid = new Vector2((float)Math.Floor(boundingBox_AABB.X / Tile.TileDimensions.X), (float)Math.Floor(boundingBox_AABB.Y / Tile.TileDimensions.Y));
                }
                // If there are collisions, check if the movement is diagonal.
                else if (direction == Direction.UpLeft || direction == Direction.UpRight || direction == Direction.DownLeft || direction == Direction.DownRight)
                {
                    #region // Diagonal movement
                    // Handles wallsliding method calls
                    Vector2 newPositionWallslide = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y);
                    if (wallSlide == WallSlide.WallSlideLeft)
                    {
                        velocity = new Vector2(-movementSpeed, 0);     // Sets the Player's Velocity.
                        direction = Direction.Left;
                        newPositionWallslide = new Vector2(boundingBox_AABB.X + (velocity.X * deltaTime), boundingBox_AABB.Y);
                        wallSlide = WallSlide.None;
                        CollisionCheckTileMap(newPositionWallslide);
                    }
                    if (wallSlide == WallSlide.WallSlideRight)
                    {
                        velocity = new Vector2(movementSpeed, 0);     // Sets the Player's Velocity.
                        direction = Direction.Right;
                        newPositionWallslide = new Vector2((boundingBox_AABB.X + boundingBox_AABB.Width) + (velocity.X * deltaTime), boundingBox_AABB.Y);
                        wallSlide = WallSlide.None;
                        CollisionCheckTileMap(newPositionWallslide);
                    }
                    if (wallSlide == WallSlide.WallSlideUp)
                    {
                        velocity = new Vector2(0, -movementSpeed);     // Sets the Player's Velocity.
                        direction = Direction.Up;
                        newPositionWallslide = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y + (velocity.Y * deltaTime));
                        wallSlide = WallSlide.None;
                        CollisionCheckTileMap(newPositionWallslide);
                    }
                    if (wallSlide == WallSlide.WallSlideDown)
                    {
                        velocity = new Vector2(0, movementSpeed);     // Sets the Player's Velocity.
                        direction = Direction.Down;
                        newPositionWallslide = new Vector2(boundingBox_AABB.X, (boundingBox_AABB.Y + boundingBox_AABB.Height) + (velocity.Y * deltaTime));
                        wallSlide = WallSlide.None;
                        CollisionCheckTileMap(newPositionWallslide);
                    }
                    #endregion
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected abstract void MovementControl();
    }
}
