using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    [DebuggerDisplay("{tag}")]
    public abstract class BaseGameObject
    {
        // Vars
        public string tag { get; set; }
        public Vector2 position_Base { get; set; }
        public Vector2 position_Grid { get { return new Vector2((int)(position_Base.X / Tile.Dimensions.X), (int)(position_Base.Y / Tile.Dimensions.Y)); } }
        public Vector2 position_Draw { get { return position_Base + Engine.Window_HUD_Size_Pixels; } }
        protected Vector2 boundingBox_Offset { get; set; }
        public Vector2 boundingBox_Size { get; set; }
        public Rectangle boundingBox_AABB { get { return new Rectangle((int)position_Base.X + (int)boundingBox_Offset.X, (int)position_Base.Y + (int)boundingBox_Offset.Y, (int)boundingBox_Size.X, (int)boundingBox_Size.Y); } }
        protected Vector2 AABB_TopLeft_GridPosition { get { return new Vector2((int)(boundingBox_AABB.X / Tile.Dimensions.X), (int)(boundingBox_AABB.Y / Tile.Dimensions.Y)); } }
        protected Vector2 AABB_TopRight_GridPosition { get { return new Vector2((int)((boundingBox_AABB.X + (boundingBox_AABB.Width - 1)) / Tile.Dimensions.X), (int)(boundingBox_AABB.Y / Tile.Dimensions.Y)); } }
        protected Vector2 AABB_BottomLeft_GridPosition { get { return new Vector2((int)(boundingBox_AABB.X / Tile.Dimensions.X), (int)((boundingBox_AABB.Y + (boundingBox_AABB.Height - 1)) / Tile.Dimensions.Y)); } }
        protected Vector2 AABB_BottomRight_GridPosition { get { return new Vector2((int)((boundingBox_AABB.X + (boundingBox_AABB.Width - 1)) / Tile.Dimensions.X), (int)((boundingBox_AABB.Y + (boundingBox_AABB.Height - 1)) / Tile.Dimensions.Y)); } }
        protected Vector2 AABB_Center_GridPosition { get { return new Vector2((int)((boundingBox_AABB.X + (boundingBox_AABB.Width / 2) - 1) / Tile.Dimensions.X), (int)((boundingBox_AABB.Y + (boundingBox_AABB.Height / 2) - 1) / Tile.Dimensions.Y)); } }
        protected Vector2 newGridPosition { get; set; }
        protected Vector2 newGridPositionOffset { get; set; }
        protected Vector2 newGridPositionOffsetDiagonal { get; set; }
        public Vector2 velocity { get; set; }
        public float movementSpeed { get; protected set; }
        public float healthPoints { get; protected set; }
        public bool isAlive {  get { return healthPoints > 0.0f;  } }
        public float damagePower { get; protected set; }
        protected float deltaTime { get; set; }
        public Texture2D texture { get; set; }
        public string textureTag { get; set; }
        public Color colour { get; set; }
        public Vector2 sourceRectangle_Position { get; set; }
        public Vector2 sourceRectangle_Size { get; set; }
        public Vector2 sourceRectangle_Offset { get; set; }
        public Rectangle sourceRectangle
        {
            get
            {
                int x = (int)((sourceRectangle_Position.X * sourceRectangle_Size.X) + sourceRectangle_Offset.X);
                int y = (int)((sourceRectangle_Position.Y * sourceRectangle_Size.Y) + sourceRectangle_Offset.Y);
                int width = (int)sourceRectangle_Size.X;
                int height = (int)sourceRectangle_Size.Y;
                return new Rectangle(x, y, width, height);
            }
        }
        public Vector2 origin { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public SpriteEffects spriteEffect { get; set; }
        public float layerDepth { get; set; }
        public List<Animation> animations { get; set; }
        public string previousAnimationTag { get; set; }

        // Constructors
        public BaseGameObject(string tag, Texture2D texture, Vector2 position_Base, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth)
        {
            try
            {
                this.tag = tag;
                this.texture = texture;

                this.position_Base = position_Base;
                boundingBox_Size = Vector2.Zero;
                boundingBox_Offset = Vector2.Zero;

                newGridPosition = Vector2.Zero;
                newGridPositionOffset = Vector2.Zero;
                newGridPositionOffsetDiagonal = Vector2.Zero;

                healthPoints = 1.0f;
                velocity = Vector2.Zero;
                movementSpeed = 0.0f;
                damagePower = 1.0f;

                deltaTime = 0;

                this.sourceRectangle_Position = sourceRectangle_Position;
                this.sourceRectangle_Size = sourceRectangle_Size;
                this.sourceRectangle_Offset = Vector2.Zero;
                this.colour = colour;
                this.origin = Vector2.Zero;
                this.rotation = 0.0f;
                this.scale = 1.0f;
                this.spriteEffect = SpriteEffects.None;
                this.layerDepth = layerDepth;

                animations = new List<Animation>();
                previousAnimationTag = "";
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }

        // Methods
        public abstract void Update(GameTime gameTime);
        public virtual void Draw()
        {
            try
            {
                if (isAlive)
                {
                    Engine.SpriteBatch.Draw(texture, position_Draw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public void AttachAnimations(List<Animation> newAnimations)
        {
            for (int i = 0; i < newAnimations.Count(); i++)
            {
                animations.Add(newAnimations[i]);
            }
        }
    }
}
