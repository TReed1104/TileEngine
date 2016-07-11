using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class Entity : GameObject
    {
        // Enums
        public enum Direction { Down, Up, Left, Right, UpLeft, UpRight, DownLeft, DownRight };
        public enum WallSlide { None, WallSlideLeft, WallSlideRight, WallSlideUp, WallSlideDown, };
        // Vars
        public float healthPoints { get; protected set; }
        public float damagePower { get; protected set; }
        public float movementSpeed { get; protected set; }
        public Vector2 velocity { get; set; }
        public Direction direction { get; protected set; }
        public Direction spriteDirection { get; protected set; }
        public WallSlide wallSlide { get; set; }
        protected Vector2 boundingBox_Offset { get; set; }
        public Rectangle boundingBox_AABB { get; set; }
        protected Vector2 AABB_PlayerTopLeft_GridPosition;
        protected Vector2 AABB_PlayerTopRight_GridPosition;
        protected Vector2 AABB_PlayerBottomLeft_GridPosition;
        protected Vector2 AABB_PlayerBottomRight_GridPosition;
        protected Vector2 AABB_PlayerCenter_GridPosition;
        protected Vector2 newGridPosition;
        protected Vector2 newGridPositionOffset;
        protected Vector2 newGridPositionOffsetDiagonal;
        protected float deltaTime { get; set; }

        // Constructors
        static Entity()
        {

        }
        public Entity(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth)
            : base (tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {
            deltaTime = 0;
            this.healthPoints = healthPoints;
            velocity = Vector2.Zero;
            movementSpeed = 1.0f;
            damagePower = 1.0f;
            direction = Direction.Down;
            wallSlide = WallSlide.None;
            Vector2 boundingSize = new Vector2(10, 10);
            Vector2 boundingGridDelta = Engine.TileDimensions - boundingSize;
            boundingBox_Offset = boundingGridDelta / 2;
            position_Grid = new Vector2((int)(position_Draw.X / Engine.TileDimensions.X), (int)(position_Draw.Y / Engine.TileDimensions.Y));
            boundingBox_AABB = new Rectangle((int)(position_Grid.X * Engine.TileDimensions.X) + (int)boundingBox_Offset.X, (int)(position_Grid.Y * Engine.TileDimensions.Y) + (int)boundingBox_Offset.Y, (int)boundingSize.X, (int)boundingSize.Y);
            newGridPosition = Vector2.Zero;
            newGridPositionOffset = Vector2.Zero;
            newGridPositionOffsetDiagonal = Vector2.Zero;
            layerDepth = Engine.LayerDepth_NPC;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateAABBCorners();
            MovementControl();
            velocity = Vector2.Zero;
            UpdateSpriteDirection();
            position_Draw = new Vector2(boundingBox_AABB.X - boundingBox_Offset.X, boundingBox_AABB.Y - boundingBox_Offset.Y) + Engine.Window_GameRender_Offset;
            position_Base = new Vector2(boundingBox_AABB.X, boundingBox_AABB.Y);

        }
        public override void Draw()
        {
            Engine.SpriteBatch.Draw(texture, position_Draw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);
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
        protected void UpdateAABBCorners()
        {
            try
            {
                AABB_PlayerTopLeft_GridPosition = new Vector2((float)Math.Floor(boundingBox_AABB.X / Engine.TileDimensions.X), (float)Math.Floor(boundingBox_AABB.Y / Engine.TileDimensions.Y));
                AABB_PlayerTopRight_GridPosition = new Vector2((float)Math.Floor((boundingBox_AABB.X + (boundingBox_AABB.Width - 1)) / Engine.TileDimensions.X), (float)Math.Floor(boundingBox_AABB.Y / Engine.TileDimensions.Y));
                AABB_PlayerBottomLeft_GridPosition = new Vector2((float)Math.Floor(boundingBox_AABB.X / Engine.TileDimensions.X), (float)Math.Floor((boundingBox_AABB.Y + (boundingBox_AABB.Height - 1)) / Engine.TileDimensions.Y));
                AABB_PlayerBottomRight_GridPosition = new Vector2((float)Math.Floor((boundingBox_AABB.X + (boundingBox_AABB.Width - 1)) / Engine.TileDimensions.X), (float)Math.Floor((boundingBox_AABB.Y + (boundingBox_AABB.Height - 1)) / Engine.TileDimensions.Y));
                AABB_PlayerCenter_GridPosition = new Vector2((float)Math.Floor((boundingBox_AABB.X + (boundingBox_AABB.Width / 2) - 1) / Engine.TileDimensions.X), (float)Math.Floor((boundingBox_AABB.Y + (boundingBox_AABB.Height / 2) - 1) / Engine.TileDimensions.Y));
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected virtual void MovementControl()
        {
            // The method handling the Keyboard press used for the Player's movement.
            try
            {

            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
    }
}
