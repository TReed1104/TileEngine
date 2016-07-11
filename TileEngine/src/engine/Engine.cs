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
        // Vars
        #region // Engine Vars
        public static string EngineName { get; private set; }
        public static string EngineVersion { get; private set; }
        #endregion
        #region // XNA Vars
        public static GraphicsDeviceManager GraphicsDevideManager { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        #endregion
        #region // Config Vars
        public static string ConfigFileName_Engine { get; set; }
        public static string ConfigFileName_Tileset { get; set; }
        public static string ConfigFileName_PlayerRegister { get; set; }
        public static string ConfigFileName_NpcRegister { get; set; }
        public static string ConfigFileName_LevelRegister { get; set; }

        public static string ConfigDirectory_Engine { get; set; }
        public static string ConfigDirectory_Levels { get; set; }

        public static string ConfigFullPath_EngineConfig{ get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Engine; } }
        public static string ConfigFullPath_Tileset { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Tileset; } }
        public static string ConfigFullPath_PlayerRegister { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_PlayerRegister; } }
        public static string ConfigFullPath_NpcRegister { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_NpcRegister; } }
        public static string ConfigFullPath_LevelRegister { get { return Engine.ConfigDirectory_Levels + Engine.ConfigFileName_LevelRegister; } }
        #endregion
        #region // Tile Vars
        public static Vector2 TileDimensions { get; set; }
        #endregion
        #region // Window Vars
        public static string Window_Title { get; private set; }
        public static int FrameRate_Max { get; private set; }
        public static Matrix Window_TransformationMatrix { get; set; }
        public static float WindowScaler { get; set; }
        public static Vector2 Window_GameRender_Offset { get; set; }
        public static Vector2 Window_TileGrid { get; private set; }
        public static Vector2 Window_DimensionsPixels_Base { get { return (Engine.Window_TileGrid * Engine.TileDimensions) + Engine.Window_GameRender_Offset; } }
        public static Vector2 Window_DimensionsPixels_Scaled { get { return Engine.Window_DimensionsPixels_Base * Engine.WindowScaler; } }
        #endregion
        #region // Register Vars
        public static List<Tile> Register_Tiles { get; set; }
        public static List<Level> Register_Levels { get; set; }
        public static List<Player> Register_Players { get; set; }
        public static List<Entity> Register_Npc { get; set; }
        #endregion
        #region // Pointer Vars
        public static int PointerCurrent_Player { get; set; }
        public static int PointerCurrent_Level { get; set; }
        #endregion
        #region // LayerDepth Vars
        public static float LayerDepth_Background { get; set; }
        public static float LayerDepth_Interactive { get; set; }
        public static float LayerDepth_NPC { get; set; }
        public static float LayerDepth_Player { get; set; }
        public static float LayerDepth_Foreground { get; set; }
        #endregion
        #region // Debugger Vars
        public static bool VisualDebugger { get; set; }
        #endregion

        // Constructors
        static Engine()
        {
            Engine.EngineName = "NULL";
            Engine.EngineVersion = "NULL";

            Engine.TileDimensions = new Vector2(16, 16);

            Engine.WindowScaler = 1.0f;
            Engine.Window_Title = "NULL";
            Engine.FrameRate_Max = 60;
            Engine.Window_GameRender_Offset = new Vector2(0, 50);
            Engine.Window_TileGrid = new Vector2(30, 20);

            Engine.Register_Tiles = new List<Tile>();
            Engine.Register_Levels = new List<Level>();
            Engine.Register_Players = new List<Player>();
            Engine.Register_Npc = new List<Entity>();
            Engine.PointerCurrent_Player = 0;
            Engine.PointerCurrent_Level = 0;

            Engine.LayerDepth_Background = 0.10f;
            Engine.LayerDepth_Interactive = 0.09f;
            Engine.LayerDepth_NPC = 0.08f;
            Engine.LayerDepth_Player = 0.07f;
            Engine.LayerDepth_Foreground = 0.06f;

            Engine.ConfigFileName_Engine = "engine.ini";
            Engine.ConfigFileName_Tileset = "tile_set.ini";
            Engine.ConfigFileName_PlayerRegister = "playe_register.ini";
            Engine.ConfigFileName_NpcRegister = "npc_register.ini";
            Engine.ConfigFileName_LevelRegister = "level_register.ini";

            Engine.ConfigDirectory_Engine = "config/";
            Engine.ConfigDirectory_Levels = "content/levels";

            Engine.VisualDebugger = false;

            Engine.Load();
        }

        // Methods
        public static void Update(GameTime gameTime)
        {
            try
            {
                if (Engine.Register_Levels.Count > 0)
                {
                    GetCurrentLevel().Update(gameTime);
                }
                if (Engine.Register_Players.Count > 0)
                {
                    GetCurrentPlayer().Update(gameTime);
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void Draw()
        {
            try
            {
                Engine.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Engine.Window_TransformationMatrix);
                if (Engine.Register_Levels.Count > 0)
                {
                    GetCurrentLevel().Draw();
                }
                if (Engine.Register_Players.Count > 0)
                {
                    GetCurrentPlayer().Draw();
                }
                Engine.SpriteBatch.End();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static Player GetCurrentPlayer()
        {
            return Engine.Register_Players[Engine.PointerCurrent_Player];
        }
        public static Level GetCurrentLevel()
        {
            return Engine.Register_Levels[Engine.PointerCurrent_Level];
        }
        public static void Load()
        {
            try
            {
                Engine.LoadEngine();
                Engine.LoadTileset();
                Engine.LoadLevels();
                Engine.LoadSaves();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
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
        public static void InitialiseGameWindow()
        {
            try
            {
                Engine.Window_TransformationMatrix = Matrix.Identity;
                Engine.Window_TransformationMatrix *= Matrix.CreateScale(Engine.WindowScaler);
                Engine.GraphicsDevideManager.PreferredBackBufferWidth = (int)Engine.Window_DimensionsPixels_Scaled.X;
                Engine.GraphicsDevideManager.PreferredBackBufferHeight = (int)Engine.Window_DimensionsPixels_Scaled.Y;
                Engine.GraphicsDevideManager.ApplyChanges();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
    }
}
