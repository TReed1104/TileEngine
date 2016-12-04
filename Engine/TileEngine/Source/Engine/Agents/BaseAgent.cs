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

        // Constructors
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {

            this.healthPoints = healthPoints;
            movementSpeed = 50.0f;
            movementDirection = Direction.Down;

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
        protected bool CollisionHandler_Movement()
        {
            try
            {
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

                Vector2 newPosition = position + newVelocity;
                Vector2 newGridPosition = Engine.ConvertPosition_PixelToGrid(newPosition);
                AABB newBounding = new AABB(newPosition, boundingBox.size);

                float deltaNewX = newPosition.X - position.X;
                float deltaNewY = newPosition.Y - position.Y;
                float deltaVelocityX = Math.Abs(position.X - boundingBox.position.X);
                float deltaVelocityY = Math.Abs(position.Y - boundingBox.position.Y);
                Vector2 deltaGrid = newGridPosition - position_Grid;



                AABB boundingToCheck_0 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_TopLeft);
                AABB boundingToCheck_1 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_TopRight);
                AABB boundingToCheck_2 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_BottomLeft);
                AABB boundingToCheck_3 = Engine.GetCurrentLevel().GetTileBoundingBox(newBounding.gridPosition_BottomRight);

                bool isCollidiing_0 = newBounding.Intersects(boundingToCheck_0);
                bool isCollidiing_1 = newBounding.Intersects(boundingToCheck_1);
                bool isCollidiing_2 = newBounding.Intersects(boundingToCheck_2);
                bool isCollidiing_3 = newBounding.Intersects(boundingToCheck_3);

                bool isColliding = isCollidiing_0 || isCollidiing_1 || isCollidiing_2 || isCollidiing_3;

                if(!isColliding)
                {
                    velocity = newVelocity;
                    return true;
                }
                else
                {
                    // If a collision occured
                    if (movementDirection == Direction.UpLeft)
                    {
                        // If the player is moving diagonally up and left
                        // Make them move Left instead.
                        movementDirection = Direction.Left;
                        bool isMovementPossible = CollisionHandler_Movement();
                        // If they can't move Left, try moving up.
                        if (!isMovementPossible)
                        {
                            movementDirection = Direction.Up;
                            CollisionHandler_Movement();
                        }
                    }
                    else if (movementDirection == Direction.UpRight)
                    {
                        // If the player is moving diagonally up and left
                        // Make them move Right instead.
                        movementDirection = Direction.Right;
                        bool isMovementPossible = CollisionHandler_Movement();
                        // If they can't move Right, try moving up.
                        if (!isMovementPossible)
                        {
                            movementDirection = Direction.Up;
                            CollisionHandler_Movement();
                        }
                    }
                    else if (movementDirection == Direction.DownLeft)
                    {
                        // If the player is moving diagonally up and left
                        // Make them move Left instead.
                        movementDirection = Direction.Left;
                        bool isMovementPossible = CollisionHandler_Movement();
                        // If they can't move Left, try moving up.
                        if (!isMovementPossible)
                        {
                            movementDirection = Direction.Down;
                            CollisionHandler_Movement();
                        }
                    }
                    else if (movementDirection == Direction.DownRight)
                    {
                        // If the player is moving diagonally up and left
                        // Make them move left instead.
                        movementDirection = Direction.Right;
                        bool isMovementPossible = CollisionHandler_Movement();
                        // If they can't move Right, try moving up.
                        if (!isMovementPossible)
                        {
                            movementDirection = Direction.Down;
                            CollisionHandler_Movement();
                        }
                    }
                    return false;
                }


            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return false;
            }
        }
    }
}
