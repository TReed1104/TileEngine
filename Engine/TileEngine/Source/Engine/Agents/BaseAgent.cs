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
        public bool isMoving { get; set; }
        public bool isAttacking { get; set; }
        public bool isDefending { get; set; }
        public Direction movementDirection { get; protected set; }
        public WallSlide wallSlide { get; set; }

        // Constructors
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {

            this.healthPoints = healthPoints;
            movementSpeed = 1.0f;
            movementDirection = Direction.Down;
            wallSlide = WallSlide.None;

            boundingBox_Size = new Vector2(10, 10);
            Vector2 boundingGridDelta = sourceRectangle_Size - boundingBox_Size;
            boundingBox_Offset = (boundingGridDelta / 2);
        }

        // Delegates
        protected delegate void MovementControl();
        protected MovementControl MovementHandler;
        protected delegate void AgentBehaviour(GameTime gameTime);
        protected AgentBehaviour BehaviourHandler;

        // Methods
        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;   // Calculate the DeltaTime

            // Movement
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                movementSpeed = 2.0f;
            }
            else
            {
                movementSpeed = 1.0f;
            }
            MovementHandler();

            // Behaviour
            BehaviourHandler(gameTime);

            // Animations
            AnimationHandler(gameTime);

            // Reset the Agent to its base mode.
            position_Base += velocity;
            velocity = Vector2.Zero;
            isMoving = false;
        }
        protected void AnimationFinder(string animationTag, GameTime gameTime)
        {
            // Work out which version of that directions animation to play
            string animationModifier = "Idle ";
            if (isMoving)
            {
                animationModifier = "Walking ";
            }
            if (isAttacking)
            {
                animationModifier = "Attacking ";
            }
            else if (isDefending)
            {
                animationModifier = "Defending ";
            }
            animationTag = animationModifier + animationTag;

            // Check the animation exists
            if (animations.Exists(r => r.tag == animationTag))
            {
                int indexOfAnimation = animations.FindIndex(r => r.tag == animationTag);    // Find the animation
                if (previousAnimationTag != animationTag)                                   // If the animation was not the previous animation, reset its progress to prevent strange behaviour
                {
                    animations[indexOfAnimation].Reset();
                    previousAnimationTag = animationTag;
                }

                // Play the animation
                animations[indexOfAnimation].Run(gameTime, this);
            }
        }
        protected void AnimationHandler(GameTime gameTime)
        {
            try
            {
                // Works out what direction variation of a animation to play.
                switch (movementDirection)
                {
                    case Direction.Down:
                        AnimationFinder("Towards", gameTime);
                        break;
                    case Direction.Up:
                        AnimationFinder("Away", gameTime);
                        break;
                    case Direction.Left:
                        AnimationFinder("Left", gameTime);
                        break;
                    case Direction.Right:
                        AnimationFinder("Right", gameTime);
                        break;
                    case Direction.UpLeft:
                        AnimationFinder("Left", gameTime);
                        break;
                    case Direction.UpRight:
                        AnimationFinder("Right", gameTime);
                        break;
                    case Direction.DownLeft:
                        AnimationFinder("Left", gameTime);
                        break;
                    case Direction.DownRight:
                        AnimationFinder("Right", gameTime);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected void CollisionHandler_Movement()
        {
            try
            {
                Vector2 minVelocity = new Vector2(0, 0);
                Vector2 maxVelocity = new Vector2(0, 0);
                Vector2 velocityIncrement = new Vector2(0, 0);
                switch (movementDirection)
                {
                    case Direction.Down:
                        minVelocity = new Vector2(0, 1);
                        maxVelocity = new Vector2(0, movementSpeed);
                        velocityIncrement = new Vector2(0, 1);
                        break;
                    case Direction.Up:
                        minVelocity = new Vector2(0, -1);
                        maxVelocity = new Vector2(0, -movementSpeed);
                        velocityIncrement = new Vector2(0, -1);
                        break;
                    case Direction.Left:
                        minVelocity = new Vector2(-1, 0);
                        maxVelocity = new Vector2(-movementSpeed, 0);
                        velocityIncrement = new Vector2(-1, 0);
                        break;
                    case Direction.Right:
                        minVelocity = new Vector2(1, 0);
                        maxVelocity = new Vector2(movementSpeed, 0);
                        velocityIncrement = new Vector2(1, 0);
                        break;
                    default:
                        break;
                }

                Vector2 currentVelocity = new Vector2(0, 0);
                Vector2 previousVelocity = new Vector2(0, 0);

                bool isComplete = false;
                while (!isComplete)
                {
                    bool isColliding = false;
                    // Check for collision
                    AABB newBoundingBox = new AABB(boundingBox.position, boundingBox.size);
                    newBoundingBox.SetPosition(newBoundingBox.position + currentVelocity);

                    Vector2 gridPositionToCheck_0 = new Vector2(-1, -1);
                    Vector2 gridPositionToCheck_1 = new Vector2(-1, -1);

                    if (newBoundingBox.gridPosition_TopLeft != newBoundingBox.gridPosition_BottomLeft)
                    {
                        gridPositionToCheck_1 = newBoundingBox.gridPosition_BottomLeft;
                    }

                    switch (movementDirection)
                    {
                        case Direction.Down:
                            gridPositionToCheck_0 = newBoundingBox.gridPosition_BottomLeft;
                            gridPositionToCheck_1 = newBoundingBox.gridPosition_BottomLeft;
                            if (newBoundingBox.gridPosition_BottomLeft != newBoundingBox.gridPosition_BottomRight)
                            {
                                gridPositionToCheck_1 = newBoundingBox.gridPosition_BottomRight;
                            }
                            break;
                        case Direction.Up:
                            gridPositionToCheck_0 = newBoundingBox.gridPosition_TopLeft;
                            gridPositionToCheck_1 = newBoundingBox.gridPosition_TopLeft;
                            if (newBoundingBox.gridPosition_TopLeft != newBoundingBox.gridPosition_TopRight)
                            {
                                gridPositionToCheck_1 = newBoundingBox.gridPosition_TopRight;
                            }
                            break;
                        case Direction.Left:
                            gridPositionToCheck_0 = newBoundingBox.gridPosition_TopLeft;
                            gridPositionToCheck_1 = newBoundingBox.gridPosition_TopLeft;
                            if (newBoundingBox.gridPosition_TopLeft != newBoundingBox.gridPosition_BottomLeft)
                            {
                                gridPositionToCheck_1 = newBoundingBox.gridPosition_BottomLeft;
                            }
                            break;
                        case Direction.Right:
                            gridPositionToCheck_0 = newBoundingBox.gridPosition_TopRight;
                            gridPositionToCheck_1 = newBoundingBox.gridPosition_TopRight;
                            if (newBoundingBox.gridPosition_TopRight != newBoundingBox.gridPosition_BottomRight)
                            {
                                gridPositionToCheck_1 = newBoundingBox.gridPosition_BottomRight;
                            }
                            break;
                        default:
                            break;
                    }

                    AABB boundingToCheck_0 = Engine.GetCurrentLevel().GetTileBoundingBox(gridPositionToCheck_0);
                    AABB boundingToCheck_1 = Engine.GetCurrentLevel().GetTileBoundingBox(gridPositionToCheck_1);

                    isColliding = newBoundingBox.Intersects(boundingToCheck_0) || newBoundingBox.Intersects(boundingToCheck_1);

                    if (isColliding)
                    {
                        velocity = previousVelocity;
                        isComplete = true;
                    }
                    else
                    {
                        if (currentVelocity == maxVelocity)
                        {
                            velocity = currentVelocity;
                            isComplete = true;
                        }
                        else
                        {
                            previousVelocity = currentVelocity;
                            currentVelocity += velocityIncrement;
                        }
                    }
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
