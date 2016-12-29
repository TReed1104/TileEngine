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
        public Keys movementKey_Down { get; set; }
        public Keys movementKey_Up { get; set; }
        public Keys movementKey_Right { get; set; }
        public Keys movementKey_Left { get; set; }
        bool isXboxControllerModeActive { get; set; }

        public PlayerAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth, healthPoints)
        {
            MovementHandler += PlayerMovementHandler;
            BehaviourHandler += PlayerBehaviourHandler;
            allowChangeInVelocity = false;
            isAttacking = false;
            isDefending = false;

            // Set the controls
            isXboxControllerModeActive = false;
            movementKey_Down = Keys.Down;
            movementKey_Up = Keys.Up;
            movementKey_Right = Keys.Right;
            movementKey_Left = Keys.Left;
        }

        protected void PlayerMovementHandler(GameTime gameTime)
        {
            // The method handling the Keyboard press used for the Player's movement.
            try
            {
                #region // Keyboard movement handling

                if (isMovementDisabled) return;
                if (hasGridMovementBeenInitialised) return;

                KeyboardState keyboardState = Keyboard.GetState();
                bool isMovementKeyPressed = keyboardState.IsKeyDown(movementKey_Down) || keyboardState.IsKeyDown(movementKey_Up) || keyboardState.IsKeyDown(movementKey_Right) || keyboardState.IsKeyDown(movementKey_Left);

                // If a movement key is pressed...
                if (isMovementKeyPressed)
                {
                    if (!isGridSnapped)
                    {
                        bool isMovementDown = keyboardState.IsKeyDown(movementKey_Down) && keyboardState.IsKeyUp(movementKey_Up) && keyboardState.IsKeyUp(movementKey_Left) && keyboardState.IsKeyUp(movementKey_Right);
                        bool isMovementUp = keyboardState.IsKeyDown(movementKey_Up) && keyboardState.IsKeyUp(movementKey_Down) && keyboardState.IsKeyUp(movementKey_Left) && keyboardState.IsKeyUp(movementKey_Right);
                        bool isMovementLeft = keyboardState.IsKeyDown(movementKey_Left) && keyboardState.IsKeyUp(movementKey_Up) && keyboardState.IsKeyUp(movementKey_Down) && keyboardState.IsKeyUp(movementKey_Right);
                        bool isMovementRight = keyboardState.IsKeyDown(movementKey_Right) && keyboardState.IsKeyUp(movementKey_Down) && keyboardState.IsKeyUp(movementKey_Up) && keyboardState.IsKeyUp(movementKey_Left);

                        bool isMovementDownLeft = keyboardState.IsKeyDown(movementKey_Down) && keyboardState.IsKeyDown(movementKey_Left) && keyboardState.IsKeyUp(movementKey_Up) && keyboardState.IsKeyUp(movementKey_Right);
                        bool isMovementDownRight = keyboardState.IsKeyDown(movementKey_Down) && keyboardState.IsKeyDown(movementKey_Right) && keyboardState.IsKeyUp(movementKey_Up) && keyboardState.IsKeyUp(movementKey_Left);
                        bool isMovementUpLeft = keyboardState.IsKeyDown(movementKey_Up) && keyboardState.IsKeyDown(movementKey_Left) && keyboardState.IsKeyUp(movementKey_Down) && keyboardState.IsKeyUp(movementKey_Right);
                        bool isMovementUpRight = keyboardState.IsKeyDown(movementKey_Up) && keyboardState.IsKeyDown(movementKey_Right) && keyboardState.IsKeyUp(movementKey_Down) && keyboardState.IsKeyUp(movementKey_Left);

                        if (isMovementDown)
                        {
                            movementDirection = EightDirectional.Down;
                        }
                        else if (isMovementUp)
                        {
                            movementDirection = EightDirectional.Up;
                        }
                        else if (isMovementLeft)
                        {
                            movementDirection = EightDirectional.Left;
                        }
                        else if (isMovementRight)
                        {
                            movementDirection = EightDirectional.Right;
                        }
                        else if (isMovementDownLeft)
                        {
                            movementDirection = EightDirectional.DownLeft;
                        }
                        else if (isMovementDownRight)
                        {
                            movementDirection = EightDirectional.DownRight;
                        }
                        else if (isMovementUpLeft)
                        {
                            movementDirection = EightDirectional.UpLeft;
                        }
                        else if (isMovementUpRight)
                        {
                            movementDirection = EightDirectional.UpRight;
                        }
                        // If not allow the collision handler to adjust the velocity for pixel-by-pixel movement
                        allowChangeInVelocity = isMovementDown || isMovementUp || isMovementLeft || isMovementRight || isMovementDownLeft || isMovementDownRight || isMovementUpLeft || isMovementUpRight;
                    }
                    // If the movement is grid snapped and the direction has been changed, lock the movement.
                    else if (isGridSnapped)
                    {
                        bool isMovementDown = keyboardState.IsKeyDown(movementKey_Down) && keyboardState.IsKeyUp(movementKey_Up);
                        bool isMovementUp = keyboardState.IsKeyDown(movementKey_Up) && keyboardState.IsKeyUp(movementKey_Down);
                        bool isMovementLeft = keyboardState.IsKeyDown(movementKey_Left) && keyboardState.IsKeyUp(movementKey_Right);
                        bool isMovementRight = keyboardState.IsKeyDown(movementKey_Right) && keyboardState.IsKeyUp(movementKey_Left);

                        if (isMovementDown)
                        {
                            movementDirection = EightDirectional.Down;
                        }
                        else if (isMovementUp)
                        {
                            movementDirection = EightDirectional.Up;
                        }
                        else if (isMovementLeft)
                        {
                            movementDirection = EightDirectional.Left;
                        }
                        else if (isMovementRight)
                        {
                            movementDirection = EightDirectional.Right;
                        }
                        hasGridMovementBeenInitialised = isMovementDown || isMovementUp || isMovementLeft || isMovementRight;
                    }
                }
                #endregion
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected void PlayerBehaviourHandler(GameTime gameTime)
        {

        }
    }
}
