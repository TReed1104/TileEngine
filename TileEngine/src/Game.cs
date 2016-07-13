using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TileEngine
{
    public class Game : Microsoft.Xna.Framework.Game
    {

        public Game()
        {
            Engine.GraphicsDevideManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "content";
            Window.Title = Engine.Window_Title;
            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / Engine.FrameRate_Max);
            IsFixedTimeStep = false;
            Engine.InitialiseGameWindow();
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Engine.SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Engine.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
