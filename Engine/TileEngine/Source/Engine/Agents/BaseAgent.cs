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

        // Vars
        public bool isMoving { get; set; }
        public bool isAttacking { get; set; }
        public bool isDefending { get; set; }
        public Direction movementDirection { get; protected set; }
        public bool isGridSnapped { get; set; }
        protected Vector2 snappingVelocity { get; set; }

        // Constructors
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {

            this.healthPoints = healthPoints;
            movementSpeed = 50.0f;  // In pixels per second.
            movementDirection = Direction.Down;

            Vector2 boundingGridDelta = sourceRectangle_Size - new Vector2(10, 10);
            boundingBox_Offset = (boundingGridDelta / 2);
            boundingBox_Size = new Vector2(10, 10);

            snappingVelocity = new Vector2(0, 0);
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
            position += snappingVelocity;
            velocity = Vector2.Zero;
            snappingVelocity = Vector2.Zero;
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
                        case Direction.Down:
                            newVelocity = new Vector2(0, movementSpeed * deltaTime);
                            break;
                        case Direction.Up:
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
                    Vector2 newGridPosition = new Vector2((int)(newPosition.X / Tile.Dimensions.X), (int)(newPosition.Y / Tile.Dimensions.Y));

                    // Check the new Position is within bounds
                    if (newPosition.X >= 0 && newPosition.X < Engine.GetCurrentLevel().gridSize_Pixels.X && newPosition.Y >= 0 && newGridPosition.Y < Engine.GetCurrentLevel().gridSize_Pixels.Y)
                    {
                        AABB newBounding = new AABB(newPosition, boundingBox.size);
                        #endregion

                        #region // Grab the AABBs of the cells the player is overlapping and check if the newBoundingBox is colliding with any of them.
                        AABB boundingToCheck_TopLeft = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_TopLeft);
                        AABB boundingToCheck_TopRight = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_TopRight);
                        AABB boundingToCheck_BottomLeft = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_BottomLeft);
                        AABB boundingToCheck_BottomRight = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_BottomRight);
                        bool isCollisionOccuring_TopLeft = newBounding.CheckForCollisionWith(boundingToCheck_TopLeft);
                        bool isCollisionOccuring_TopRight = newBounding.CheckForCollisionWith(boundingToCheck_TopRight);
                        bool isCollisionOccuring_BottomLeft = newBounding.CheckForCollisionWith(boundingToCheck_BottomLeft);
                        bool isCollisionOccuring_BottomRight = newBounding.CheckForCollisionWith(boundingToCheck_BottomRight);
                        #endregion

                        // Work out if any of the overlaps involved a collision with a solid tile.
                        bool isColliding = isCollisionOccuring_TopLeft || isCollisionOccuring_TopRight || isCollisionOccuring_BottomLeft || isCollisionOccuring_BottomRight;
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
                                #region // Adjacent snapping.
                                case Direction.Down:
                                    {
                                        // Calculate the Down offset
                                        deltaY = (((((int)((int)position.Y / Tile.Dimensions.Y) + 1)) * Tile.Dimensions.Y) - (position.Y + boundingBox_Size.Y));
                                        snappingVelocity = new Vector2(snappingVelocity.X, deltaY - 0.001f);

                                        break;
                                    }
                                case Direction.Up:
                                    {
                                        // Calculate the Up offset
                                        deltaY = (((int)(((int)((int)position.Y / Tile.Dimensions.Y) - 1) * Tile.Dimensions.Y) + 16.0f) - (position.Y));
                                        snappingVelocity = new Vector2(snappingVelocity.X, deltaY);

                                        break;
                                    }
                                case Direction.Left:
                                    {
                                        // Calculate the Left offset
                                        deltaX = ((int)((((int)(position.X / Tile.Dimensions.X) - 1)) * Tile.Dimensions.X) + 16.0f) - (position.X);
                                        snappingVelocity = new Vector2(deltaX, snappingVelocity.Y);

                                        break;
                                    }
                                case Direction.Right:
                                    {
                                        // Calculate the Right offset
                                        deltaX = ((((int)(position.X / Tile.Dimensions.X)) + 1) * Tile.Dimensions.X) - (position.X + boundingBox_Size.X);
                                        snappingVelocity = new Vector2(deltaX - 0.001f, snappingVelocity.Y);

                                        break;
                                    }
                                #endregion
                                #region // Diagonal snapping.
                                case Direction.UpLeft:
                                    {
                                        bool isCollidingLeft = isCollisionOccuring_TopLeft && isCollisionOccuring_BottomLeft;
                                        bool isCollidingUp = isCollisionOccuring_TopLeft && isCollisionOccuring_TopRight;

                                        // If collision is left, snap left.
                                        if (isCollidingLeft)
                                        {
                                            // Calculate the Left offset
                                            deltaX = ((int)((((int)(position.X / Tile.Dimensions.X) - 1)) * Tile.Dimensions.X) + 16.0f) - (position.X);
                                            snappingVelocity = new Vector2(deltaX, snappingVelocity.Y);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {

                                            movementDirection = Direction.Left;
                                            CollisionHandler_Movement();
                                        }

                                        // If collision is Up, snap Up.
                                        if (isCollidingUp)
                                        {
                                            // Calculate the Up offset
                                            deltaY = (((int)(((int)((int)position.Y / Tile.Dimensions.Y) - 1) * Tile.Dimensions.Y) + 16.0f) - (position.Y));
                                            snappingVelocity = new Vector2(snappingVelocity.X, deltaY);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Up;
                                            CollisionHandler_Movement();
                                        }
                                        break;
                                    }
                                case Direction.UpRight:
                                    {
                                        bool isCollidingRight = isCollisionOccuring_TopRight && isCollisionOccuring_BottomRight;
                                        bool isCollidingUp = isCollisionOccuring_TopLeft && isCollisionOccuring_TopRight;

                                        // If collision is Right, snap Right.
                                        if (isCollidingRight)
                                        {
                                            // Calculate the Right offset
                                            deltaX = ((((int)(position.X / Tile.Dimensions.X)) + 1) * Tile.Dimensions.X) - (position.X + boundingBox_Size.X);
                                            snappingVelocity = new Vector2(deltaX - 0.001f, snappingVelocity.Y);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Right;
                                            CollisionHandler_Movement();
                                        }

                                        // If collision is Up, snap Up.
                                        if (isCollidingUp)
                                        {
                                            // Calculate the Up offset
                                            deltaY = (((int)(((int)((int)position.Y / Tile.Dimensions.Y) - 1) * Tile.Dimensions.Y) + 16.0f) - (position.Y));
                                            snappingVelocity = new Vector2(snappingVelocity.X, deltaY);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Up;
                                            CollisionHandler_Movement();
                                        }

                                        break;
                                    }
                                case Direction.DownLeft:
                                    {
                                        bool isCollidingLeft = isCollisionOccuring_TopLeft && isCollisionOccuring_BottomLeft;
                                        bool isCollidingDown = isCollisionOccuring_BottomLeft && isCollisionOccuring_BottomRight;

                                        // If collision is left, snap left.
                                        if (isCollidingLeft)
                                        {
                                            // Calculate the Left offset
                                            deltaX = ((int)((((int)(position.X / Tile.Dimensions.X) - 1)) * Tile.Dimensions.X) + 16.0f) - (position.X);
                                            snappingVelocity = new Vector2(deltaX, snappingVelocity.Y);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {

                                            movementDirection = Direction.Left;
                                            CollisionHandler_Movement();
                                        }

                                        // If collision is Down, snap Down.
                                        if (isCollidingDown)
                                        {
                                            // Calculate the Down offset
                                            deltaY = (((((int)((int)position.Y / Tile.Dimensions.Y) + 1)) * Tile.Dimensions.Y) - (position.Y + boundingBox_Size.Y));
                                            snappingVelocity = new Vector2(snappingVelocity.X, deltaY - 0.001f);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Down;
                                            CollisionHandler_Movement();
                                        }

                                        break;
                                    }
                                case Direction.DownRight:
                                    {
                                        bool isCollidingRight = isCollisionOccuring_TopRight && isCollisionOccuring_BottomRight;
                                        bool isCollidingDown = isCollisionOccuring_BottomLeft && isCollisionOccuring_BottomRight;

                                        // If collision is Right, snap Right.
                                        if (isCollidingRight)
                                        {
                                            // Calculate the Right offset
                                            deltaX = ((((int)(position.X / Tile.Dimensions.X)) + 1) * Tile.Dimensions.X) - (position.X + boundingBox_Size.X);
                                            snappingVelocity = new Vector2(deltaX - 0.001f, snappingVelocity.Y);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Right;
                                            CollisionHandler_Movement();
                                        }

                                        // If collision is Down, snap Down.
                                        if (isCollidingDown)
                                        {
                                            // Calculate the Down offset
                                            deltaY = (((((int)((int)position.Y / Tile.Dimensions.Y) + 1)) * Tile.Dimensions.Y) - (position.Y + boundingBox_Size.Y));
                                            snappingVelocity = new Vector2(snappingVelocity.X, deltaY - 0.001f);
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Down;
                                            CollisionHandler_Movement();
                                        }

                                        break;
                                    }
                                #endregion
                                default:
                                    break;
                            }
                            #endregion

                            #region // Wall Sliding
                            #endregion
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
