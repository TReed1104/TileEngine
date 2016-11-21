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
    public class PlayerAgent : BaseAgent
    {
        public PlayerAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth, healthPoints)
        {
            MovementController += PlayerMovement;
            BehaviourController += PlayerBehaviour;
        }

        protected void PlayerMovement()
        {
            // The method handling the Keyboard press used for the Player's movement.
            try
            {
                Vector2 newPosition = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y);        // Takes a copy of the current position.
                bool keyPressed = false;

                #region // Left
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
                {
                    velocity = new Vector2(-movementSpeed, 0);     // Sets the Player's Velocity.
                    direction = Direction.Left;
                    spriteDirection = Direction.Left;
                    newPosition = new Vector2(boundingBox_AABB.X + (velocity.X * deltaTime), boundingBox_AABB.Y);
                    keyPressed = true;
                }
                #endregion
                #region // Right
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
                {
                    velocity = new Vector2(movementSpeed, 0);     // Sets the Player's Velocity.
                    direction = Direction.Right;
                    spriteDirection = Direction.Right;
                    newPosition = new Vector2((boundingBox_AABB.X + boundingBox_AABB.Width) + (velocity.X * deltaTime), boundingBox_AABB.Y);
                    keyPressed = true;
                }
                #endregion
                #region // Up
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down))
                {
                    velocity = new Vector2(0, -movementSpeed);     // Sets the Player's Velocity.
                    direction = Direction.Up;
                    spriteDirection = Direction.Up;
                    newPosition = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                #endregion
                #region // Down
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.Up))
                {
                    velocity = new Vector2(0, movementSpeed);     // Sets the Player's Velocity.
                    direction = Direction.Down;
                    spriteDirection = Direction.Down;
                    newPosition = new Vector2(boundingBox_AABB.X, (boundingBox_AABB.Y + boundingBox_AABB.Height) + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                #endregion
                #region // Up-Left
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    velocity = new Vector2(-movementSpeed, -movementSpeed);
                    direction = Direction.UpLeft;
                    spriteDirection = Direction.UpLeft;
                    newPosition = new Vector2(boundingBox_AABB.X + (velocity.X * deltaTime), boundingBox_AABB.Y + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                #endregion
                #region // Up-Right
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    velocity = new Vector2(movementSpeed, -movementSpeed);
                    direction = Direction.UpRight;
                    spriteDirection = Direction.UpRight;
                    newPosition = new Vector2((boundingBox_AABB.X + boundingBox_AABB.Width) + (velocity.X * deltaTime), boundingBox_AABB.Y + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                #endregion
                #region // Down-Left
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    velocity = new Vector2(-movementSpeed, movementSpeed);
                    direction = Direction.DownLeft;
                    spriteDirection = Direction.DownLeft;
                    newPosition = new Vector2(boundingBox_AABB.X + (velocity.X * deltaTime), (boundingBox_AABB.Y + boundingBox_AABB.Height) + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                #endregion
                #region // Down-Right
                if (Keyboard.GetState().IsKeyDown(Keys.Down) && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    velocity = new Vector2(movementSpeed, movementSpeed);
                    direction = Direction.DownRight;
                    spriteDirection = Direction.DownRight;
                    newPosition = new Vector2((boundingBox_AABB.X + boundingBox_AABB.Width) + (velocity.X * deltaTime), (boundingBox_AABB.Y + boundingBox_AABB.Height) + (velocity.Y * deltaTime));
                    keyPressed = true;
                }
                #endregion

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
        protected void PlayerBehaviour(GameTime gameTime)
        {

        }
    }
}
