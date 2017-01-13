using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileEngine
{
    public abstract class Item : GameObject
    {
        public bool isActive { get; protected set; }
        protected delegate void ItemBehaviourHandler(float deltaTime);
        protected ItemBehaviourHandler behaviourHandler;

        public Item(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth) : base (tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {

        }


        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;   // Calculate the DeltaTime

            behaviourHandler(deltaTime);
            AnimationHandler(deltaTime);
        }
        public override void Draw()
        {
            base.Draw();
        }
        public void Use()
        {
            if (!isActive)
            {
                isActive = true;
            }
        }
    }
}
