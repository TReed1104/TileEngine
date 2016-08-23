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
    public class Player : Entity
    {
        static Player()
        {

        }
        public Player(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth, healthPoints)
        {
            layerDepth = Engine.LayerDepth_Player;
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
        public override void Draw()
        {
            base.Draw();
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
                    boundingBox_AABB = new Rectangle((int)(boundingBox_AABB.X + velocity.X), (int)(boundingBox_AABB.Y + velocity.Y), boundingBox_AABB.Width, boundingBox_AABB.Height);
                    position_Grid = new Vector2((float)Math.Floor(boundingBox_AABB.X / Tile.TileDimensions.X), (float)Math.Floor(boundingBox_AABB.Y / Tile.TileDimensions.Y));
                }
                // If there are collisions, check if the movement is diagonal.
                else if (direction == Direction.UpLeft || direction == Direction.UpRight || direction == Direction.DownLeft || direction == Direction.DownRight)
                {
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
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected override void MovementControl()
        {
            // The method handling the Keyboard press used for the Player's movement.
            try
            {
                Vector2 newPosition = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y);        // Takes a copy of the current position.
                bool keyPressed = false;
                // Left
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
                {
                    velocity = new Vector2(-movementSpeed, 0);     // Sets the Player's Velocity.
                    direction = Direction.Left;
                    spriteDirection = Direction.Left;
                    newPosition = new Vector2(boundingBox_AABB.X + (velocity.X * deltaTime), boundingBox_AABB.Y);
                    keyPressed = true;
                }
                // Right
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
                {
                    velocity = new Vector2(movementSpeed, 0);     // Sets the Player's Velocity.
                    direction = Direction.Right;
                    spriteDirection = Direction.Right;
                    newPosition = new Vector2((boundingBox_AABB.X + boundingBox_AABB.Width) + (velocity.X * deltaTime), boundingBox_AABB.Y);
                    keyPressed = true;
                }
                // Up
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down))
                {
                    velocity = new Vector2(0, -movementSpeed);     // Sets the Player's Velocity.
                    direction = Direction.Up;
                    spriteDirection = Direction.Up;
                    newPosition = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                // Down
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up))
                {
                    velocity = new Vector2(0, movementSpeed);     // Sets the Player's Velocity.
                    direction = Direction.Down;
                    spriteDirection = Direction.Down;
                    newPosition = new Vector2(boundingBox_AABB.X, (boundingBox_AABB.Y + boundingBox_AABB.Height) + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                // Up-Left
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    velocity = new Vector2(-movementSpeed, -movementSpeed);
                    direction = Direction.UpLeft;
                    spriteDirection = Direction.UpLeft;
                    newPosition = new Vector2(boundingBox_AABB.X + (velocity.X * deltaTime), boundingBox_AABB.Y + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                // Up-Right
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    velocity = new Vector2(movementSpeed, -movementSpeed);
                    direction = Direction.UpRight;
                    spriteDirection = Direction.UpRight;
                    newPosition = new Vector2((boundingBox_AABB.X + boundingBox_AABB.Width) + (velocity.X * deltaTime), boundingBox_AABB.Y + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                // Down-Left
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    velocity = new Vector2(-movementSpeed, movementSpeed);
                    direction = Direction.DownLeft;
                    spriteDirection = Direction.DownLeft;
                    newPosition = new Vector2(boundingBox_AABB.X + (velocity.X * deltaTime), (boundingBox_AABB.Y + boundingBox_AABB.Height) + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                // Down-Right
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    velocity = new Vector2(movementSpeed, movementSpeed);
                    direction = Direction.DownRight;
                    spriteDirection = Direction.DownRight;
                    newPosition = new Vector2((boundingBox_AABB.X + boundingBox_AABB.Width) + (velocity.X * deltaTime), (boundingBox_AABB.Y + boundingBox_AABB.Height) + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                if (keyPressed)                 // If a movement key has been pressed.
                {
                    CollisionCheckTileMap(newPosition);    // Calls the method for setting the next position.
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
