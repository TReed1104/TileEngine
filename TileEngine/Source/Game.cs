using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TileEngine
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public Game()
        {
            Engine.SetupEngine(this);
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Engine.LoadContent(this);
        }
        protected override void UnloadContent()
        {
            Engine.UnloadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            Engine.Draw(this);
            base.Draw(gameTime);
        }
    }
}
