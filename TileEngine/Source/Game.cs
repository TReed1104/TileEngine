using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace TileEngine
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public Game()
        {
            Engine.GraphicsDevideManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            // Load textures and fonts
            string[] rawTextureDirectories = Directory.GetFiles(Engine.ConfigDirectory_Textures);

            for (int i = 0; i < rawTextureDirectories.Length; i++)
            {
                // Load the texture into the texture register
                string trimmedTexturePath = rawTextureDirectories[i].Replace("Content/", "");
                string[] splitTexturePath = trimmedTexturePath.Split('.');
                Engine.Register_Textures.Add(Content.Load<Texture2D>(splitTexturePath[0]));

                // Assign the textures tag, used for finding the textures in the register
                string[] splitPathForTag = splitTexturePath[0].Split('/');
                Engine.Register_Textures[Engine.Register_Textures.Count - 1].Tag = splitPathForTag[1];
            }

            Engine.LoadContent();
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
            GraphicsDevice.Clear(Color.Magenta);
            Engine.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
