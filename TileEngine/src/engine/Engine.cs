using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

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

        public static string ConfigFullPath_EngineConfig { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Engine; } }
        public static string ConfigFullPath_Tileset { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Tileset; } }
        public static string ConfigFullPath_PlayerRegister { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_PlayerRegister; } }
        public static string ConfigFullPath_NpcRegister { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_NpcRegister; } }
        public static string ConfigFullPath_LevelRegister { get { return Engine.ConfigDirectory_Levels + Engine.ConfigFileName_LevelRegister; } }
        #endregion
        #region // Window Vars
        public static string Window_Title { get; private set; }
        public static int FrameRate_Max { get; private set; }
        public static Matrix Window_TransformationMatrix { get; set; }
        public static float Window_Scaler { get; set; }
        public static Vector2 Window_GameRender_Offset { get; set; }
        public static Vector2 Window_TileGrid { get; private set; }
        public static Vector2 Window_DimensionsPixels_Base { get { return (Engine.Window_TileGrid * Tile.TileDimensions) + Engine.Window_GameRender_Offset; } }
        public static Vector2 Window_DimensionsPixels_Scaled { get { return Engine.Window_DimensionsPixels_Base * Engine.Window_Scaler; } }
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
        #region // Counter Vars
        public static int Counter_Tiles { get; set; }
        public static int Counter_Levels { get; set; }
        public static int Counter_Players { get; set; }
        public static int Counter_Npcs { get; set; }
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

            Engine.Window_Scaler = 1.0f;
            Engine.Window_Title = "NULL";
            Engine.FrameRate_Max = 30;
            Engine.Window_GameRender_Offset = new Vector2(0, 50);
            Engine.Window_TileGrid = new Vector2(10, 10);

            Engine.Register_Tiles = new List<Tile>();
            Engine.Register_Levels = new List<Level>();
            Engine.Register_Players = new List<Player>();
            Engine.Register_Npc = new List<Entity>();

            Engine.PointerCurrent_Player = 0;
            Engine.PointerCurrent_Level = 0;

            Engine.Counter_Tiles = 0;
            Engine.Counter_Levels = 0;
            Engine.Counter_Players = 0;
            Engine.Counter_Npcs = 0;

            Engine.LayerDepth_Background = 0.10f;
            Engine.LayerDepth_Interactive = 0.09f;
            Engine.LayerDepth_NPC = 0.08f;
            Engine.LayerDepth_Player = 0.07f;
            Engine.LayerDepth_Foreground = 0.06f;

            Engine.ConfigFileName_Engine = "engine.ini";
            Engine.ConfigFileName_Tileset = "tileset.ini";
            Engine.ConfigFileName_PlayerRegister = "player_register.ini";
            Engine.ConfigFileName_NpcRegister = "npc_register.ini";
            Engine.ConfigFileName_LevelRegister = "level_register.ini";

            Engine.ConfigDirectory_Engine = "config/";
            Engine.ConfigDirectory_Levels = "content/levels/";

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
        public static void Draw(GameTime gameTime)
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
            try
            {
                return Engine.Register_Players[Engine.PointerCurrent_Player];
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
                return null;
            }
        }
        public static Level GetCurrentLevel()
        {
            try
            {
                return Engine.Register_Levels[Engine.PointerCurrent_Level];
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
                return null;
            }
        }

        // Engine loading methods
        public static void Load()
        {
            try
            {
                Engine.LoadEngineConfig();
                Engine.LoadTileset();
                Engine.LoadLevelRegister();
                Engine.LoadPlayerRegister();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void LoadEngineConfig()
        {
            try
            {
                // Check for the config file.
                if (File.Exists(Engine.ConfigFullPath_EngineConfig))
                {
                    // Read the config file.
                    XmlReader xmlReader = XmlReader.Create(Engine.ConfigFullPath_EngineConfig);
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            if (xmlReader.Name == "engine_settings")
                            {
                                // Load the Engine settings
                                Engine.EngineName = xmlReader.GetAttribute("name");
                                Engine.EngineVersion = xmlReader.GetAttribute("version");
                            }
                            if (xmlReader.Name == "window_settings")
                            {
                                // Load the Window settings
                                Engine.Window_Title = xmlReader.GetAttribute("title");
                                Engine.Window_TileGrid = new Vector2(int.Parse(xmlReader.GetAttribute("width")), int.Parse(xmlReader.GetAttribute("height")));
                                Engine.FrameRate_Max = int.Parse(xmlReader.GetAttribute("max_frame_rate"));
                                Engine.Window_Scaler = float.Parse(xmlReader.GetAttribute("scaler"));
                                Engine.Window_GameRender_Offset = new Vector2(0, int.Parse(xmlReader.GetAttribute("hud_size")));
                            }
                            if (xmlReader.Name == "tile_set")
                            {
                                // Load the Tileset settings
                                Tile.TileDimensions = new Vector2(int.Parse(xmlReader.GetAttribute("width")), int.Parse(xmlReader.GetAttribute("height")));
                                Tile.SpritesheetSource = xmlReader.GetAttribute("src");
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void LoadTileset()
        {
            try
            {
                // Check for the config file.
                if (File.Exists(Engine.ConfigFullPath_Tileset))
                {
                    Engine.Counter_Tiles = 0;
                    // Read the config file.
                    XmlReader xmlReader = XmlReader.Create(Engine.ConfigFullPath_Tileset);
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "tile")
                        {
                            // Load the tile
                            string tag = xmlReader.GetAttribute("tag");
                            int src_frame_x = int.Parse(xmlReader.GetAttribute("src_frame_x"));
                            int src_frame_y = int.Parse(xmlReader.GetAttribute("src_frame_y"));
                            Color colour = Tile.Register_ConvertColour(xmlReader.GetAttribute("colour"));
                            int id = int.Parse(xmlReader.GetAttribute("id"));
                            Tile.TileType tileType = Tile.Register_ConvertTileType(xmlReader.GetAttribute("type"));

                            // Add the tile to the register ready for use.
                            Engine.Register_Tiles.Add(new Tile(tag, new Vector2(src_frame_x, src_frame_y), colour, Engine.LayerDepth_Background, id, tileType));

                            Engine.Counter_Tiles++;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void LoadLevelRegister()
        {
            try
            {
                // Check if the level register exists.
                if (!File.Exists(Engine.ConfigFullPath_LevelRegister))
                {
                    // If the directory for the levels does not exist, create it.
                    if (!Directory.Exists(Engine.ConfigDirectory_Levels))
                    {
                        Directory.CreateDirectory(Engine.ConfigDirectory_Levels);
                    }

                    // If the register does not exist, generate it.
                    using (XmlWriter xmlWriter = XmlWriter.Create(Engine.ConfigFullPath_LevelRegister))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteWhitespace("\r\n");
                        xmlWriter.WriteStartElement("level_register");
                        xmlWriter.WriteAttributeString("level_count", "0");
                        xmlWriter.WriteWhitespace("\r\n");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }
                }
                else
                {
                    Engine.Counter_Levels = 0;  // Resets

                    // Read the register
                    XmlReader xmlReader = XmlReader.Create(Engine.ConfigFullPath_LevelRegister);
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            if (xmlReader.Name == "level")
                            {
                                int index = int.Parse(xmlReader.GetAttribute("index"));
                                string tag = xmlReader.GetAttribute("tag");
                                string src = xmlReader.GetAttribute("src");
                                Engine.Register_Levels.Add(new Level(tag, index, src));
                                Engine.Counter_Levels++;
                            }
                        }
                    }
                    xmlReader.Close();
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void LoadPlayerRegister()
        {
            try
            {

            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void LoadNpcRegister()
        {
            try
            {

            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }

        public static void InitialiseGameWindow()
        {
            try
            {
                Engine.Window_TransformationMatrix = Matrix.Identity;
                Engine.Window_TransformationMatrix *= Matrix.CreateScale(Engine.Window_Scaler);
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
