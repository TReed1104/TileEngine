using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class Animation
    {
        public string tag { get; protected set; }
        public int startX { get; protected set; }
        public int startY { get; protected set; }
        public int currentX { get; protected set; }
        public int currentY { get; protected set; }
        public int numberOfFrames { get; protected set; }
        public float animationSpeed { get; protected set; }
        protected float animationTimer { get; set; }

        public Animation(string tag, int startX, int startY, int numberOfFrames, float animationSpeed)
        {
            this.tag = tag;
            this.startX = startX;
            this.startY = startY;
            this.currentX = this.startX;
            this.currentY = this.startY;
            this.numberOfFrames = numberOfFrames;
            this.animationSpeed = animationSpeed;
            this.animationTimer = 0.0f;
        }

        public Vector2 Run(GameTime gameTime)
        {
            Vector2 newFramePositon = new Vector2(currentX, currentY);
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            newFramePositon.Y = startY;
            if (animationTimer >= animationSpeed)
            {
                currentX = (currentX + 1) % numberOfFrames;
                currentX += startX;
                animationTimer = 0;
                newFramePositon.X = currentX;
            }
            return newFramePositon;
        }
        public void Reset()
        {
            currentX = startX;
            currentY = startY;
            animationTimer = 0;
        }
    }
}
