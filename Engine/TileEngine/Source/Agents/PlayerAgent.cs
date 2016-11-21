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
    public class PlayerAgent : BaseAgent
    {
        static PlayerAgent()
        {

        }
        public PlayerAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth, healthPoints)
        {

        }

        public override void Update(GameTime gameTime)
        {
            inputComponent.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            inputComponent.Execute(this);
            base.Update(gameTime);
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
