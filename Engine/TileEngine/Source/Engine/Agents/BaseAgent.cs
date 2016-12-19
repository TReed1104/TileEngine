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
        public bool allowChangeInVelocity { get; set; }
        public bool isMovingForAnimation { get; set; }
        public bool isAttacking { get; set; }
        public bool isDefending { get; set; }
        public Direction movementDirection { get; protected set; }
        public bool isGridSnapped { get { return Engine.IsMovementGridSnapped; } }
        protected Vector2 snapVelocity { get; set; }
        public bool isMovementDisabled { get; set; }

        public bool hasGridMovementBeenInitialised { get; protected set; }
        public Vector2 snappedMovementStartPosition { get; protected set; }
        public Vector2 snappedMovementTargetPosition { get; protected set; }
        public float snappedMovementTimer { get; protected set; }

        // Constructors
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {

            this.healthPoints = healthPoints;
            movementDirection = Direction.Down;

            Vector2 boundingGridDelta = sourceRectangle_Size - new Vector2(10, 10);
            boundingBox_Offset_Texture = (boundingGridDelta / 2);
            boundingBox_Size = new Vector2(10, 10);
            snapVelocity = new Vector2(0, 0);
            isMovementDisabled = false;

            if (Engine.IsMovementGridSnapped)
            {
                hasGridMovementBeenInitialised = false;
                snappedMovementStartPosition = position_Grid;
                snappedMovementTargetPosition = position_Grid;
                movementSpeed = 0.2f;  // Time taken to reach next tile, in milliseconds
            }
            else
            {
                movementSpeed = 60.0f;  // In pixels per second.
            }
        }

        // Delegates
        protected delegate void MovementControl(GameTime gameTime);
        protected MovementControl MovementHandler;
        protected delegate void AgentBehaviour(GameTime gameTime);
        protected AgentBehaviour BehaviourHandler;

        // Methods
        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;   // Calculate the DeltaTime

            // Movement
            MovementHandler(gameTime);
            CollisionHandler_Movement(gameTime);

            // Behaviour
            BehaviourHandler(gameTime);

            // Animations
            AnimationHandler(gameTime);

            // Reset the Agent to its base mode.
            position += velocity;
            position += snapVelocity;
            velocity = Vector2.Zero;
            snapVelocity = Vector2.Zero;
            isMovingForAnimation = false;

            allowChangeInVelocity = false;
        }
        protected void AnimationFinder(string animationTag, GameTime gameTime)
        {
            // Work out which version of that directions animation to play
            string animationModifier = "Idle ";
            if (isMovingForAnimation)
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
        protected void CollisionHandler_Movement(GameTime gameTime)
        {
            try
            {
                if (!isGridSnapped)
                {
                    // If the agent can move pixel by pixel.
                    #region // Calculate the velocity of the movement decided by the agent.
                    Vector2 newVelocity = new Vector2(0, 0);
                    if (allowChangeInVelocity)
                    {
                        isMovingForAnimation = true;    // As the object can potentially move, allow animation

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
                    }
                    #endregion

                    #region// Calculate the new positionand create an AABB at that position, this represents the new position of the player if the movement takes palce.
                    Vector2 newPosition = position + newVelocity;
                    Vector2 newGridPosition = new Vector2((int)(newPosition.X / Tile.Dimensions.X), (int)(newPosition.Y / Tile.Dimensions.Y));
                    #endregion

                    #region // The collisions checks and movement
                    // Check the new Position is within bounds
                    if (newPosition.X >= 0 && (newPosition.X + boundingBox.width) < Engine.GetCurrentLevel().gridSize_Pixels.X && newPosition.Y >= 0 && (newPosition.Y + boundingBox.height) < Engine.GetCurrentLevel().gridSize_Pixels.Y)
                    {
                        AABB newBounding = new AABB(newPosition, boundingBox.size);

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
                                        float offsetBottomLeft = (position.Y + boundingBox.height) - boundingToCheck_BottomLeft.y;
                                        float offsetBottomRight = (position.Y + boundingBox.height) - boundingToCheck_BottomRight.y;

                                        if (offsetBottomLeft > offsetBottomRight) { deltaY = -offsetBottomLeft - 0.001f; }
                                        else { deltaY = -offsetBottomRight - 0.001f; }

                                        break;
                                    }
                                case Direction.Up:
                                    {
                                        // Calculate the Up offset
                                        float offsetTopLeft = position.Y - (boundingToCheck_TopLeft.y + boundingToCheck_TopLeft.height);
                                        float offsetTopRight = position.Y - (boundingToCheck_TopRight.y + boundingToCheck_BottomLeft.height);

                                        if (offsetTopLeft < offsetTopRight) { deltaY = -offsetTopLeft; }
                                        else { deltaY = -offsetTopRight; }

                                        break;
                                    }
                                case Direction.Left:
                                    {
                                        // Calculate the Left offset
                                        float offsetTopLeft = position.X - (boundingToCheck_TopLeft.x + boundingToCheck_TopLeft.width);
                                        float offsetBottomLeft = position.X - (boundingToCheck_BottomLeft.x + boundingToCheck_BottomLeft.width);

                                        if (offsetTopLeft < offsetBottomLeft) { deltaX = -offsetTopLeft; }
                                        else { deltaX = -offsetBottomLeft; }

                                        break;
                                    }
                                case Direction.Right:
                                    {
                                        // Calculate the Right offset
                                        float offsetTopRight = (position.X + boundingBox.width) - boundingToCheck_TopRight.x;
                                        float offsetBottomRight = (position.X + boundingBox.width) - boundingToCheck_BottomRight.x;

                                        if (offsetTopRight > offsetBottomRight) { deltaX = -offsetTopRight - 0.001f; }
                                        else { deltaX = -offsetBottomRight - 0.001f; }

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
                                            float offsetTopLeft = position.X - (boundingToCheck_TopLeft.x + boundingToCheck_TopLeft.width);
                                            float offsetBottomLeft = position.X - (boundingToCheck_BottomLeft.x + boundingToCheck_BottomLeft.width);

                                            if (offsetTopLeft < offsetBottomLeft) { deltaX = -offsetTopLeft; }
                                            else { deltaX = -offsetBottomLeft; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {

                                            movementDirection = Direction.Left;
                                            CollisionHandler_Movement(gameTime);
                                        }

                                        // If collision is Up, snap Up.
                                        if (isCollidingUp)
                                        {
                                            // Calculate the Up offset
                                            float offsetTopLeft = position.Y - (boundingToCheck_TopLeft.y + boundingToCheck_TopLeft.height);
                                            float offsetTopRight = position.Y - (boundingToCheck_TopRight.y + boundingToCheck_BottomLeft.height);

                                            if (offsetTopLeft < offsetTopRight) { deltaY = -offsetTopLeft; }
                                            else { deltaY = -offsetTopRight; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Up;
                                            CollisionHandler_Movement(gameTime);
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
                                            float offsetTopRight = (position.X + boundingBox.width) - boundingToCheck_TopRight.x;
                                            float offsetBottomRight = (position.X + boundingBox.width) - boundingToCheck_BottomRight.x;

                                            if (offsetTopRight > offsetBottomRight) { deltaX = -offsetTopRight - 0.001f; }
                                            else { deltaX = -offsetBottomRight - 0.001f; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Right;
                                            CollisionHandler_Movement(gameTime);
                                        }

                                        // If collision is Up, snap Up.
                                        if (isCollidingUp)
                                        {
                                            // Calculate the Up offset
                                            float offsetTopLeft = position.Y - (boundingToCheck_TopLeft.y + boundingToCheck_TopLeft.height);
                                            float offsetTopRight = position.Y - (boundingToCheck_TopRight.y + boundingToCheck_BottomLeft.height);

                                            if (offsetTopLeft < offsetTopRight) { deltaY = -offsetTopLeft; }
                                            else { deltaY = -offsetTopRight; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Up;
                                            CollisionHandler_Movement(gameTime);
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
                                            float offsetTopLeft = position.X - (boundingToCheck_TopLeft.x + boundingToCheck_TopLeft.width);
                                            float offsetBottomLeft = position.X - (boundingToCheck_BottomLeft.x + boundingToCheck_BottomLeft.width);

                                            if (offsetTopLeft < offsetBottomLeft) { deltaX = -offsetTopLeft; }
                                            else { deltaX = -offsetBottomLeft; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Left;
                                            CollisionHandler_Movement(gameTime);
                                        }

                                        // If collision is Down, snap Down.
                                        if (isCollidingDown)
                                        {
                                            // Calculate the Down offset
                                            float offsetBottomLeft = (position.Y + boundingBox.height) - boundingToCheck_BottomLeft.y;
                                            float offsetBottomRight = (position.Y + boundingBox.height) - boundingToCheck_BottomRight.y;

                                            if (offsetBottomLeft > offsetBottomRight) { deltaY = -offsetBottomLeft - 0.001f; }
                                            else { deltaY = -offsetBottomRight - 0.001f; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Down;
                                            CollisionHandler_Movement(gameTime);
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
                                            float offsetTopRight = (position.X + boundingBox.width) - boundingToCheck_TopRight.x;
                                            float offsetBottomRight = (position.X + boundingBox.width) - boundingToCheck_BottomRight.x;

                                            if (offsetTopRight > offsetBottomRight) { deltaX = -offsetTopRight - 0.001f; }
                                            else { deltaX = -offsetBottomRight - 0.001f; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Right;
                                            CollisionHandler_Movement(gameTime);
                                        }

                                        // If collision is Down, snap Down.
                                        if (isCollidingDown)
                                        {
                                            // Calculate the Down offset
                                            float offsetBottomLeft = (position.Y + boundingBox.height) - boundingToCheck_BottomLeft.y;
                                            float offsetBottomRight = (position.Y + boundingBox.height) - boundingToCheck_BottomRight.y;

                                            if (offsetBottomLeft > offsetBottomRight) { deltaY = -offsetBottomLeft - 0.001f; }
                                            else { deltaY = -offsetBottomRight - 0.001f; }
                                        }
                                        // If not, try to wallslide.
                                        else
                                        {
                                            movementDirection = Direction.Down;
                                            CollisionHandler_Movement(gameTime);
                                        }

                                        break;
                                    }
                                #endregion
                                default:
                                    break;
                            }
                            #endregion

                            snapVelocity = new Vector2(deltaX, deltaY);
                        }
                    }
                    #endregion

                }
                else
                {
                    // If the agent can only move between tiles.
                    // Check if the player is set to be moving, handled by "MovementHandler()" of the derived class.
                    if (hasGridMovementBeenInitialised)
                    {
                        isMovingForAnimation = true;
                        if (snappedMovementTargetPosition == snappedMovementStartPosition)
                        {
                            #region // Calculate the new target position
                            // Work out the new cell to check.
                            Vector2 newTargetPosition = position_Grid;
                            switch (movementDirection)
                            {
                                case Direction.Down:
                                    newTargetPosition += new Vector2(0, 1);
                                    break;
                                case Direction.Up:
                                    newTargetPosition += new Vector2(0, -1);
                                    break;
                                case Direction.Left:
                                    newTargetPosition += new Vector2(-1, 0);
                                    break;
                                case Direction.Right:
                                    newTargetPosition += new Vector2(1, 0);
                                    break;
                                default:
                                    break;
                            }
                            #endregion

                            #region // Do collisions checks
                            // Check if the new target cell is a valid space to move to.
                            if (Engine.GetCurrentLevel().IsTileEmpty(newTargetPosition))
                            {
                                // The target position is empty, set the target for the movement.
                                snappedMovementStartPosition = position_Grid;
                                snappedMovementTargetPosition = newTargetPosition;
                                snappedMovementTimer = 0.0f;
                            }
                            else
                            {
                                // Invalid cell, 
                                hasGridMovementBeenInitialised = false;
                            }
                            #endregion
                        }
                        #region // Movement timer calculations
                        // Increment the timer used for the lerp
                        snappedMovementTimer += deltaTime;

                        // Cap the movement speed.
                        if (snappedMovementTimer >= movementSpeed)
                        {
                            snappedMovementTimer = movementSpeed;
                        }
                        #endregion

                        // Calculate the velocity of the agent.
                        Vector2 newPositon = Vector2.Lerp(snappedMovementStartPosition * Tile.Dimensions, snappedMovementTargetPosition * Tile.Dimensions, snappedMovementTimer / movementSpeed) - boundingBox_Offset_Tile;  // Minus the tile offset of the AABB.
                        velocity = newPositon - position;

                        #region // Check if the movement is complete.
                        // Prevent any further movement
                        if ((newPositon + boundingBox_Offset_Tile) == (snappedMovementTargetPosition * Tile.Dimensions))
                        {
                            hasGridMovementBeenInitialised = false;
                            snappedMovementStartPosition = position_Grid;
                            snappedMovementTargetPosition = position_Grid;
                            snappedMovementTimer = 0.0f;
                        }
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
