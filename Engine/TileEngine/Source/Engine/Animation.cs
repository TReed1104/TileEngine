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
        public int animationSpeed { get; protected set; }
        protected float animationTimer { get; set; }
        protected bool isAnimationActive { get; set; }

        public Animation(string tag, int startX, int startY, int numberOfFrames, int animationSpeed)
        {
            this.tag = tag;
            this.startX = startX;
            this.startY = startY;
            this.numberOfFrames = numberOfFrames;
            this.animationSpeed = animationSpeed;
            this.animationTimer = 0.0f;
        }

        public void Run(GameTime gameTime, Vector2 framePosition)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            framePosition.Y = startY;
            if (animationTimer >= animationSpeed)
            {
                framePosition = new Vector2(currentX, currentY);

                //frameX = (frameX + 1) % numberOfFrame;  //Calculates the frame to use
                //frameX += startFrame;   //Changes the frame
                //frameTimer = 0; //Resets the timer
            }
        }
        public void Reset()
        {

        }
    }
}
