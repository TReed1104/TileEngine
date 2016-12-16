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
        protected Vector2 pixelSnapVelocity { get; set; }
        public bool isMovementDisabled { get; set; }

        protected bool isSnapMovementOccuring { get; set; }
        protected bool isTargetPositionSet { get; set; }
        protected Vector2 startPositionOfGridSnap { get; set; }
        protected Vector2 targetPositionOfGridSnap { get; set; }
        protected Vector2 distanceTraveledInSnap { get; set; }
        protected float movementSpeed_Snapped { get; set; }

        // Constructors
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {

            this.healthPoints = healthPoints;
            movementSpeed_FreeMovement = 60.0f;  // In pixels per second.
            movementDirection = Direction.Down;

            Vector2 boundingGridDelta = sourceRectangle_Size - new Vector2(10, 10);
            boundingBox_Offset_Texture = (boundingGridDelta / 2);
            boundingBox_Size = new Vector2(10, 10);
            pixelSnapVelocity = new Vector2(0, 0);
            isMovementDisabled = false;

            // Snapping
            isSnapMovementOccuring = false;
            isTargetPositionSet = false;
            startPositionOfGridSnap = position_Grid;
            targetPositionOfGridSnap = position_Grid;
            distanceTraveledInSnap = new Vector2(0, 0);
            movementSpeed_Snapped = 250f;  // Time taken to reach next tile, in milliseconds
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
            position += pixelSnapVelocity;
            velocity = Vector2.Zero;
            pixelSnapVelocity = Vector2.Zero;
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
                        switch (movementDirection)
                        {
                            case Direction.Down:
                                newVelocity = new Vector2(0, movementSpeed_FreeMovement * deltaTime);
                                break;
                            case Direction.Up:
                                newVelocity = new Vector2(0, -movementSpeed_FreeMovement * deltaTime);
                                break;
                            case Direction.Left:
                                newVelocity = new Vector2(-movementSpeed_FreeMovement * deltaTime, 0);
                                break;
                            case Direction.Right:
                                newVelocity = new Vector2(movementSpeed_FreeMovement * deltaTime, 0);
                                break;
                            case Direction.UpLeft:
                                newVelocity = new Vector2(-movementSpeed_FreeMovement * deltaTime, -movementSpeed_FreeMovement * deltaTime);
                                break;
                            case Direction.UpRight:
                                newVelocity = new Vector2(movementSpeed_FreeMovement * deltaTime, -movementSpeed_FreeMovement * deltaTime);
                                break;
                            case Direction.DownLeft:
                                newVelocity = new Vector2(-movementSpeed_FreeMovement * deltaTime, movementSpeed_FreeMovement * deltaTime);
                                break;
                            case Direction.DownRight:
                                newVelocity = new Vector2(movementSpeed_FreeMovement * deltaTime, movementSpeed_FreeMovement * deltaTime);
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

                            pixelSnapVelocity = new Vector2(deltaX, deltaY);
                        }
                    }
                    #endregion

                }
                else
                {
                    // If the agent can only move between tiles.
                    // Check if the player is set to be moving, handled by "MovementHandler()" of the derived class.
                    if (isSnapMovementOccuring)
                    {
                        isMovingForAnimation = true;                // Used to tell the animation handler that the movement variation of the animation should be played.
                        float distanceOfMovementInTiles = 1.0f;     // Distance in tiles the agent can move per movement.

                        #region // If not already done, Calculate the grid cell that the movement is targetting.
                        if (!isTargetPositionSet)
                        {
                            // Calculate the new cell position and check if the movement is possible.
                            Vector2 newTarget = position_Grid;
                            switch (movementDirection)
                            {
                                case Direction.Down:
                                    newTarget += new Vector2(0, distanceOfMovementInTiles);
                                    break;
                                case Direction.Up:
                                    newTarget += new Vector2(0, -distanceOfMovementInTiles);
                                    break;
                                case Direction.Left:
                                    newTarget += new Vector2(-distanceOfMovementInTiles, 0);
                                    break;
                                case Direction.Right:
                                    newTarget += new Vector2(distanceOfMovementInTiles, 0);
                                    break;
                                default:
                                    break;
                            }
                            if (Engine.GetCurrentLevel().IsTileEmpty(newTarget))
                            {
                                startPositionOfGridSnap = position_Grid;
                                targetPositionOfGridSnap = newTarget;
                                isTargetPositionSet = true;
                                distanceTraveledInSnap = new Vector2(0, 0);
                            }
                            else
                            {
                                startPositionOfGridSnap = position_Grid;
                                targetPositionOfGridSnap = position_Grid;
                                isTargetPositionSet = false;
                                isSnapMovementOccuring = false;
                                distanceTraveledInSnap = new Vector2(0, 0);
                            }

                            // ***** Need to implement checks for movements longer than one tile.
                        }
                        #endregion

                        #region // Calculate the velocity of the movement decided by the agent.
                        Vector2 distanceOfMovement = (targetPositionOfGridSnap - startPositionOfGridSnap) * Tile.Dimensions;
                        Vector2 newVelocity = ((targetPositionOfGridSnap - startPositionOfGridSnap) * Tile.Dimensions) * (gameTime.ElapsedGameTime.Milliseconds / movementSpeed_Snapped);
                        #endregion

                        #region // Work out if the new velocity will overshoot the target cell, and adjust the velocity accordingly.
                        if ((distanceTraveledInSnap + newVelocity).Length() < distanceOfMovement.Length())
                        {
                            // If the distance travelled is less than the maximum distance the agent can move, set their velocity to the new value.
                            velocity = newVelocity;
                            distanceTraveledInSnap = (distanceTraveledInSnap + newVelocity);
                        }
                        else
                        {
                            #region // Tile snap and reset.

                            // If the distance travelled is greater than the maximum distance the agent can move, snap them to the nearest grid position.
                            Vector2 deltaPosition = ((targetPositionOfGridSnap * Tile.Dimensions) - boundingBox_Offset_Tile) - position;
                            velocity = deltaPosition;

                            // Reset for next movement.
                            startPositionOfGridSnap = position_Grid;
                            targetPositionOfGridSnap = position_Grid;
                            isTargetPositionSet = false;
                            isSnapMovementOccuring = false;
                            distanceTraveledInSnap = new Vector2(0, 0);
                            #endregion
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
