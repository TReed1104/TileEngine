using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using static TileEngine.Level;

namespace TileEngine
{
    public static class Engine
    {
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
        public static string ConfigFileName_NpcRegister { get; set; }

        public static string ConfigDirectory_Engine { get; private set; }
        public static string ConfigDirectory_Levels { get; private set; }
        public static string ConfigDirectory_SaveData { get; private set; }

        public static string ConfigFullPath_EngineConfig { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Engine; } }
        public static string ConfigFullPath_Tileset { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Tileset; } }
        public static string ConfigFullPath_NpcRegister { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_NpcRegister; } }
        #endregion
        #region // Window Vars
        public static string Window_Title { get; private set; }
        public static int FrameRate_Max { get; private set; }
        public static Matrix Window_TransformationMatrix { get; set; }
        public static float Window_Scaler { get; set; }
        public static Vector2 Window_GameRender_Offset { get; set; }
        public static Vector2 Window_TileGridSize { get; private set; }
        public static Vector2 Window_PixelGridSize { get { return (Engine.Window_TileGridSize * Tile.TileDimensions); } }
        public static Vector2 Window_DimensionsPixels_Base { get { return (Engine.Window_TileGridSize * Tile.TileDimensions) + Engine.Window_GameRender_Offset; } }
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
        #region // Camera Vars
        public static Camera MainCamera { get; set; }
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
            Engine.Window_TileGridSize = new Vector2(10, 10);

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

            Engine.MainCamera = new Camera("Main Camera", Vector2.Zero);

            Engine.ConfigFileName_Engine = "engine.ini";
            Engine.ConfigFileName_Tileset = "tileset.ini";
            Engine.ConfigFileName_NpcRegister = "npc_register.ini";

            Engine.ConfigDirectory_Engine = "config/";
            Engine.ConfigDirectory_Levels = "content/levels/";
            Engine.ConfigDirectory_SaveData = "content/data/";

            Engine.VisualDebugger = false;
            Engine.Load();
        }
        // XNA Methods
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
                Engine.MainCamera.Update(gameTime, Engine.GetCurrentPlayer());
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
        // Get methods for ease of access.
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
                Engine.LoadLevels();
                Engine.LoadPlayers();
                Generate_NewWorld(WorldType.Plains);
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
                                Engine.Window_TileGridSize = new Vector2(int.Parse(xmlReader.GetAttribute("width")), int.Parse(xmlReader.GetAttribute("height")));
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
        public static void LoadLevels()
        {
            try
            {
                Engine.Counter_Levels = 0;  // Resets the level counter

                if (!Directory.Exists(Engine.ConfigDirectory_Levels))
                {
                    Directory.CreateDirectory(Engine.ConfigDirectory_Levels);
                }

                string[] levelList = Directory.GetFiles(Engine.ConfigDirectory_Levels);
                for (int i = 0; i < levelList.Length; i++)
                {
                    string src = levelList[i];
                    Engine.Register_Levels.Add(new Level(src));
                    Engine.Counter_Levels++;
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void LoadPlayers()
        {
            try
            {
                #region // Check if the directory doesn't exist, if not then create it.
                if (!Directory.Exists(Engine.ConfigDirectory_SaveData))
                {
                    Directory.CreateDirectory(Engine.ConfigDirectory_SaveData);
                }
                #endregion

                #region // Find all the save data files.
                string[] playerSaveList = Directory.GetFiles(Engine.ConfigDirectory_SaveData);
                Engine.Counter_Players = 0;  // Resets the level counter

                // If there is no save data files, create one and reload the folder.
                if (playerSaveList.Length == 0)
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(Engine.ConfigDirectory_SaveData + "data_" + Generator.RandomString(10) + ".dat"))
                    {
                        #region // Write a default save file
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteWhitespace("\r\n");
                        xmlWriter.WriteStartElement("save_data");
                        xmlWriter.WriteWhitespace("\r\n\t");

                        xmlWriter.WriteStartElement("tag");
                        xmlWriter.WriteAttributeString("value", "Player");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("src");
                        xmlWriter.WriteAttributeString("value", "entity/player");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("src_frame_pos_x");
                        xmlWriter.WriteAttributeString("value", "0");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("src_frame_pos_y");
                        xmlWriter.WriteAttributeString("value", "0");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("src_frame_size");
                        xmlWriter.WriteAttributeString("value", "48");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("position_x");
                        xmlWriter.WriteAttributeString("value", "48");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("position_y");
                        xmlWriter.WriteAttributeString("value", "68");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("hp");
                        xmlWriter.WriteAttributeString("value", "10");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteStartElement("gold");
                        xmlWriter.WriteAttributeString("value", "0");
                        xmlWriter.WriteWhitespace("\r\n\t");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteWhitespace("\r\n");
                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                        #endregion
                    }
                    playerSaveList = Directory.GetFiles(Engine.ConfigDirectory_SaveData);
                }
                #endregion

                #region // Read all the found save data files.
                for (int i = 0; i < playerSaveList.Length; i++)
                {
                    XmlReader xmlReader = XmlReader.Create(playerSaveList[i]);
                    string tag = "";
                    string src = "";
                    int src_frame_pos_x = -1;
                    int src_frame_pos_y = -1;
                    int src_frame_size = -1;
                    int position_x = -1;
                    int position_y = -1;
                    int hp = -1;
                    int gold = -1;

                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            if (xmlReader.Name == "tag") { tag = xmlReader.GetAttribute("value"); }
                            if (xmlReader.Name == "src") { src = xmlReader.GetAttribute("value"); }
                            if (xmlReader.Name == "src_frame_pos_x") { src_frame_pos_x = int.Parse(xmlReader.GetAttribute("value")); }
                            if (xmlReader.Name == "src_frame_pos_y") { src_frame_pos_y = int.Parse(xmlReader.GetAttribute("value")); }
                            if (xmlReader.Name == "src_frame_size") { src_frame_size = int.Parse(xmlReader.GetAttribute("value")); }
                            if (xmlReader.Name == "position_x") { position_x = int.Parse(xmlReader.GetAttribute("value")); }
                            if (xmlReader.Name == "position_y") { position_y = int.Parse(xmlReader.GetAttribute("value")); }
                            if (xmlReader.Name == "hp") { hp = int.Parse(xmlReader.GetAttribute("value")); }
                            if (xmlReader.Name == "gold") { gold = int.Parse(xmlReader.GetAttribute("value")); }
                        }
                    }
                    xmlReader.Close();

                    Vector2 position = new Vector2(position_x, position_y);
                    Vector2 sourceRectangle_Position = new Vector2(src_frame_pos_x, src_frame_pos_y);
                    Vector2 sourceRectangle_Size = new Vector2(src_frame_size, src_frame_size);

                    Player newPlayer = new Player(tag, null, position, sourceRectangle_Position, sourceRectangle_Size, Color.White, Engine.LayerDepth_Player, hp);
                    newPlayer.spriteSheetSource = src;

                    Engine.Register_Players.Add(newPlayer);
                    Engine.Counter_Players++;
                }
                #endregion
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
        // Engine Setup
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
        // Generation
        public static void Generate_NewWorld(WorldType worldType)
        {
            Engine.Register_Levels.Add(Generator.GenerateWorld(worldType));
        }
    }
}