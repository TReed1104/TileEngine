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
        public enum Direction { Towards, Away, Left, Right, UpLeft, UpRight, DownLeft, DownRight };

        // Vars
        public bool isMoving { get; set; }
        public bool isAttacking { get; set; }
        public bool isDefending { get; set; }
        public Direction movementDirection { get; protected set; }
        public bool isGridSnapped { get; set; }

        // Constructors
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {

            this.healthPoints = healthPoints;
            movementSpeed = 50.0f;  // In pixels per second.
            movementDirection = Direction.Towards;

            Vector2 boundingGridDelta = sourceRectangle_Size - new Vector2(10, 10);
            boundingBox_Offset = (boundingGridDelta / 2);
            boundingBox_Size = new Vector2(10, 10);
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
            MovementHandler();

            // Behaviour
            BehaviourHandler(gameTime);

            // Animations
            AnimationHandler(gameTime);

            // Reset the Agent to its base mode.
            position += velocity;
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
                    case Direction.Towards:
                        AnimationFinder("Towards", gameTime);
                        break;
                    case Direction.Away:
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
                // If the agent can only move between grid cells.
                if (isGridSnapped)
                {

                }
                // If the agent can move pixel by pixel.
                else
                {
                    #region // Calculate the velocity of the movement decided by the agent.
                    Vector2 newVelocity = new Vector2(0, 0);
                    switch (movementDirection)
                    {
                        case Direction.Towards:
                            newVelocity = new Vector2(0, movementSpeed * deltaTime);
                            break;
                        case Direction.Away:
                            newVelocity = new Vector2(0, -movementSpeed * deltaTime);
                            break;
                        case Direction.Left:
                            newVelocity = new Vector2(-movementSpeed * deltaTime, 0);
                            break;
                        case Direction.Right:
                            newVelocity = new Vector2(movementSpeed * deltaTime, 0);
                            break;
                        case Direction.UpLeft:
                            newVelocity = new Vector2(-movementSpeed * deltaTime, -movementSpeed * deltaTime);
                            break;
                        case Direction.UpRight:
                            newVelocity = new Vector2(movementSpeed * deltaTime, -movementSpeed * deltaTime);
                            break;
                        case Direction.DownLeft:
                            newVelocity = new Vector2(-movementSpeed * deltaTime, movementSpeed * deltaTime);
                            break;
                        case Direction.DownRight:
                            newVelocity = new Vector2(movementSpeed * deltaTime, movementSpeed * deltaTime);
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region// Calculate the new positionand create an AABB at that position, this represents the new position of the player if the movement takes palce.
                    Vector2 newPosition = position + newVelocity;
                    AABB newBounding = new AABB(newPosition, boundingBox.size);
                    #endregion

                    #region // Grab the AABBs of the cells the player is overlapping and check if the newBoundingBox is colliding with any of them.
                    AABB boundingToCheck_0 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_TopLeft);
                    AABB boundingToCheck_1 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_TopRight);
                    AABB boundingToCheck_2 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_BottomLeft);
                    AABB boundingToCheck_3 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_BottomRight);
                    bool isPlayerCollidingWithAABB_0 = newBounding.CheckForCollisionWith(boundingToCheck_0);
                    bool isPlayerCollidingWithAABB_1 = newBounding.CheckForCollisionWith(boundingToCheck_1);
                    bool isPlayerCollidingWithAABB_2 = newBounding.CheckForCollisionWith(boundingToCheck_2);
                    bool isPlayerCollidingWithAABB_3 = newBounding.CheckForCollisionWith(boundingToCheck_3);
                    #endregion

                    // Work out if any of the overlaps involved a collision with a solid tile.
                    bool isColliding = isPlayerCollidingWithAABB_0 || isPlayerCollidingWithAABB_1 || isPlayerCollidingWithAABB_2 || isPlayerCollidingWithAABB_3;
                    if (!isColliding)
                    {
                        // If no collisions are detected, set the velocity of the agent to the velocity decided earlier on.
                        velocity = newVelocity;
                    }
                    else
                    {
                        // If a collision is detected in the next movement.
                        #region // Calculate the offset velocity to move the player the tiny amount left between them and collisions.
                        // Calculate if the player still has an amount of distance between them, then if so set the velocity to that last amount of distance - this prevents weird offsets when near objects.
                        // **Down and Right movements have a slight offset of 0.001f this prevents the weird collisions caused by X + width and Y + height, which technically cause errorneous collisions.
                        float deltaX = 0;
                        float deltaY = 0;
                        switch (movementDirection)
                        {
                            case Direction.Towards:
                                deltaY = Math.Abs(position.Y - (int)position.Y);
                                if (deltaY > 0.001f)
                                {
                                    float newY = 1 - deltaY;
                                    velocity = new Vector2(deltaX, newY - 0.001f);
                                }
                                break;
                            case Direction.Away:
                                deltaY = Math.Abs(position.Y - (int)position.Y);
                                velocity = new Vector2(deltaX, -deltaY);
                                break;
                            case Direction.Left:
                                deltaX = Math.Abs(position.X - (int)position.X);
                                velocity = new Vector2(-deltaX, deltaY);
                                break;
                            case Direction.Right:
                                deltaX = Math.Abs(position.X - (int)position.X);
                                if (deltaX > 0.001f)
                                {
                                    float newX = 1 - deltaX;
                                    velocity = new Vector2(newX - 0.001f, deltaY);
                                }
                                break;
                            case Direction.UpLeft:
                                deltaX = Math.Abs(position.X - (int)position.X);
                                deltaY = Math.Abs(position.Y - (int)position.Y);
                                velocity = new Vector2(-deltaX, -deltaY);

                                break;
                            case Direction.UpRight:
                                deltaX = Math.Abs(position.X - (int)position.X);
                                if (deltaX > 0.001f)
                                {
                                    float newX = 1 - deltaX;
                                    velocity = new Vector2(newX - 0.001f, deltaY);
                                }

                                deltaY = Math.Abs(position.Y - (int)position.Y);
                                velocity = new Vector2(velocity.X, -deltaY);
                                break;
                            case Direction.DownLeft:
                                deltaX = Math.Abs(position.X - (int)position.X);
                                velocity = new Vector2(-deltaX, deltaY);

                                deltaY = Math.Abs(position.Y - (int)position.Y);
                                if (deltaY > 0.001f)
                                {
                                    float newY = 1 - deltaY;
                                    velocity = new Vector2(velocity.X, newY - 0.001f);
                                }
                                break;
                            case Direction.DownRight:
                                deltaX = Math.Abs(position.X - (int)position.X);
                                if (deltaX > 0.001f)
                                {
                                    float newX = 1 - deltaX;
                                    velocity = new Vector2(newX - 0.001f, velocity.Y);
                                }

                                deltaY = Math.Abs(position.Y - (int)position.Y);
                                if (deltaY > 0.001f)
                                {
                                    float newY = 1 - deltaY;
                                    velocity = new Vector2(velocity.X, newY - 0.001f);
                                }
                                break;
                            default:
                                break;
                        }
                        #endregion

                        #region // Wall Sliding
                        #endregion
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
