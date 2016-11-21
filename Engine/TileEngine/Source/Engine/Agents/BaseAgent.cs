using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public abstract class BaseAgent : BaseGameObject
    {
        // Enums
        public enum Direction { Down, Up, Left, Right, UpLeft, UpRight, DownLeft, DownRight };
        public enum WallSlide { None, WallSlideLeft, WallSlideRight, WallSlideUp, WallSlideDown, };
        // Vars
        public float healthPoints { get; protected set; }
        public float damagePower { get; protected set; }
        public Direction direction { get; protected set; }
        public Direction spriteDirection { get; protected set; }
        public WallSlide wallSlide { get; set; }
        
        // Constructors
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base (tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {
            deltaTime = 0;
            this.healthPoints = healthPoints;
            velocity = Vector2.Zero;
            movementSpeed = 1.0f;
            damagePower = 1.0f;
            direction = Direction.Down;
            wallSlide = WallSlide.None;

            boundingBox_Size = new Vector2(10, 10);
            Vector2 boundingGridDelta = sourceRectangle_Size - boundingBox_Size;
            boundingBox_Offset = (boundingGridDelta / 2);
            

            newGridPosition = Vector2.Zero;
            newGridPositionOffset = Vector2.Zero;
            newGridPositionOffsetDiagonal = Vector2.Zero;
        }

        // Delegates
        protected delegate void MovementControl();
        protected MovementControl MovementController;
        protected delegate void AgentBehaviour(GameTime gameTime);
        protected AgentBehaviour BehaviourController;

        // Methods
        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Movement
            MovementController();
            velocity = Vector2.Zero;
            UpdateSpriteDirection();

            // Behaviour
            BehaviourController(gameTime);
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
        protected virtual void CollisionCheckTileMap(Vector2 newPosition)
        {
            try
            {
                // The method which sets the next grid position of the Player.
                // The new position post movement.
                newGridPosition = new Vector2((float)Math.Floor(newPosition.X / Tile.Dimensions.X), (float)Math.Floor(newPosition.Y / Tile.Dimensions.Y));

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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(1, 0)) &&
                                !Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideUp;
                            }
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(1, 0)) &&
                                !Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                        }
                        if (newGridPosition == (AABB_TopLeft_GridPosition + new Vector2(-1, 0)))
                        {
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(-1, 0)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideUp;
                            }
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(-1, 0)) &&
                                !Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, 1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, 1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                        }
                        if (newGridPosition == (AABB_TopRight_GridPosition + new Vector2(1, 0)))
                        {
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(1, 0)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideDown;
                            }
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(1, 0)) &&
                                !Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideLeft;
                            }
                        }
                        if (newGridPosition == (AABB_BottomLeft_GridPosition + new Vector2(-1, 0)))
                        {
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(-1, 0)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideDown;
                            }
                            if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(-1, 0)) &&
                                !Engine.GetCurrentLevel().IsTileSolid(newGridPosition + new Vector2(0, -1)))
                            {
                                cellOffSet = new Vector2(-1, 0);
                                cellOffSetDiagonal = new Vector2(0, -1);
                                wallSlide = WallSlide.WallSlideRight;
                            }
                        }
                        if (newGridPosition == (AABB_BottomRight_GridPosition + new Vector2(1, 0)))
                        {
                            if (!Engine.GetCurrentLevel().IsTileSolid(newGridPosition))
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
                if (Engine.GetCurrentLevel().IsTileSolid(newGridPosition) &&
                    Engine.GetCurrentLevel().IsTileSolid(newGridPositionOffset) &&
                    Engine.GetCurrentLevel().IsTileSolid(newGridPositionOffsetDiagonal))
                {
                    position_Base += velocity;
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
    }
}
