using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public static class Engine
    {
        public static string EngineName { get; private set; }
        public static string EngineVersion { get; private set; }
        

        public static GraphicsDeviceManager GraphicsDevideManager { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }

        public static Vector2 TileDimensions { get; set; }

        public static float WindowScaleModifier { get; set; }
        public static Matrix WindowTransformationMatrix { get; set; }
        public static string WindowTitle { get; private set; }
        public static Vector2 WindowTileGridSize { get; private set; }
        public static Vector2 WindowDimensionsBase { get { return (Engine.WindowTileGridSize * Engine.TileDimensions); } }
        public static Vector2 WindowDimensionsScaled { get { return Engine.WindowDimensionsBase * Engine.WindowScaleModifier; } }
        public static int MaxFrameRate { get; private set; }

        public static Camera PlayerCamera { get; set; }

        public static List<Tile> RegisterTiles { get; set; }
        public static List<Level> RegisterLevels { get; set; }
        public static List<Player> RegisterPlayers { get; set; }

        public static int PointerCurrentPlayer { get; set; }
        public static int PointerCurrentLevel { get; set; }

        static Engine()
        {
            Engine.EngineName = "";
            Engine.EngineVersion = "";
            Engine.TileDimensions = new Vector2(16, 16);
            Engine.WindowScaleModifier = 1.0f;
            Engine.WindowTitle = "";
            Engine.WindowTileGridSize = Vector2.Zero;
            Engine.MaxFrameRate = 60;
            Engine.PlayerCamera = new Camera("Player", Vector2.Zero);
            Engine.RegisterTiles = new List<Tile>();
            Engine.RegisterLevels = new List<Level>();
            Engine.RegisterPlayers = new List<Player>();
            Engine.PointerCurrentPlayer = 0;
            Engine.PointerCurrentLevel = 0;
        }

        public static Player GetCurrentPlayer()
        {
            return Engine.RegisterPlayers[Engine.PointerCurrentPlayer];
        }
        public static Level GetCurrentLevel()
        {
            return Engine.RegisterLevels[Engine.PointerCurrentLevel];
        }

        public static void LoadEngine()
        {

        }
        public static void LoadTileset()
        {

        }
        public static void LoadLevels()
        {

        }
        public static void LoadSaves()
        {

        }
        public static void Update(GameTime gameTime)
        {

        }
        public static void Draw()
        {
            Engine.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Engine.WindowTransformationMatrix);

            GetCurrentLevel().Draw();
            GetCurrentPlayer().Draw();

            Engine.SpriteBatch.End();
        }
    }
}
