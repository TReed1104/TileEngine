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
    public abstract class GameObject
    {
        // Vars
        public string tag { get; set; }
        #region // positions
        public Vector2 position { get; set; }
        public Vector2 position_Grid { get { return Engine.ConvertPosition_PixelToGrid(position); } }
        public Vector2 position_Draw { get { return (position - boundingBox_Offset_Texture); } }
        #endregion
        #region // Sprite rendering
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
        #endregion
        #region // Bounding Box variables
        protected Vector2 boundingBox_Offset_Texture { get; set; }
        public Vector2 boundingBox_Offset_Tile { get { return -((Tile.Dimensions - boundingBox_Size) / 2); } }
        public Vector2 boundingBox_Size { get; set; }
        public AABB boundingBox { get { return new AABB(position, boundingBox_Size); } }
        #endregion
        public Vector2 velocity { get; set; }
        public float movementSpeed { get; protected set; }

        // Delegates
        protected delegate void AnimationSearch(float deltaTime, string animationTag);
        protected AnimationSearch AnimationFinder;
        protected delegate void AnimationControl(float deltaTime);
        protected AnimationControl AnimationHandler;

        // Constructors
        public GameObject(string tag, Texture2D texture, Vector2 position_Base, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth)
        {
            try
            {
                this.tag = tag;
                this.texture = texture;

                position = position_Base;
                boundingBox_Offset_Texture = Vector2.Zero;
                boundingBox_Size = Tile.Dimensions;
                
                velocity = Vector2.Zero;
                movementSpeed = 0.0f;

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
            if (Engine.GameCamera.IsObjectVisible(this))
            {
                Engine.XNA_SpriteBatch.Draw(texture, position_Draw, sourceRectangle, colour, rotation, origin, scale, spriteEffect, layerDepth);
            }
        }
        public void AddAnimations(List<Animation> newAnimations)
        {
            for (int i = 0; i < newAnimations.Count(); i++)
            {
                animations.Add(newAnimations[i]);
            }
        }
    }
}
