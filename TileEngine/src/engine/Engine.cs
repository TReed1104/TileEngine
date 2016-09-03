using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using static TileEngine.Zone;

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
        public const string ConfigFileName_Engine = "engine.ini";
        public const string ConfigFileName_Tileset = "tileset.ini";
        public const string ConfigFileName_NpcRegister = "npc_register.ini";

        public const string ConfigDirectory_Engine = "config/";
        public const string ConfigDirectory_Worlds = "content/worlds/";
        public const string ConfigDirectory_SaveData = "content/data/";

        public static string ConfigFullPath_EngineConfig { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Engine; } }
        public static string ConfigFullPath_Tileset { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Tileset; } }
        public static string ConfigFullPath_NpcRegister { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_NpcRegister; } }
        #endregion
        #region // Window Vars
        public static string Window_Title { get; private set; }
        public static int FrameRate_Max { get; private set; }
        public static Matrix Window_TransformationMatrix { get; set; }
        public static float Window_Scaler { get; set; }
        public static Vector2 Window_HUD_Size_Tiles { get; set; }
        public static Vector2 Window_HUD_Size_Pixels { get { return Window_HUD_Size_Tiles * Tile.Dimensions;  } }
        public static Vector2 Window_TileGridSize { get; private set; }
        public static Vector2 Window_PixelGridSize { get { return (Engine.Window_TileGridSize * Tile.Dimensions); } }
        public static Vector2 Window_DimensionsPixels_Base { get { return (Engine.Window_TileGridSize * Tile.Dimensions); } }
        public static Vector2 Window_DimensionsPixels_Scaled { get { return Engine.Window_DimensionsPixels_Base * Engine.Window_Scaler; } }

        public static Vector2 Camera_RenderGridSize_Tiles { get { return Engine.Window_TileGridSize - Engine.Window_HUD_Size_Tiles; } }
        #endregion
        #region // Register Vars
        public static List<Tile> Register_Tiles { get; set; }
        public static List<World> Register_Worlds { get; set; }
        public static List<Player> Register_Players { get; set; }
        public static List<Entity> Register_Npc { get; set; }
        #endregion
        #region // Pointer Vars
        public static int PointerCurrent_Player { get; set; }
        public static int PointerCurrent_World { get; set; }
        #endregion
        #region // Counter Vars
        public static int Counter_Tiles { get; set; }
        public static int CounterWorld { get; set; }
        public static int Counter_Players { get; set; }
        public static int Counter_Npcs { get; set; }
        #endregion
        #region // LayerDepth Vars
        public const float LayerDepth_Debugger_Background = 0.10f;
        public const float LayerDepth_Debugger_Terrain = 0.09f;
        public const float LayerDepth_Debugger_Interactive = 0.08f;
        public const float LayerDepth_Debugger_NPC = 0.07f;
        public const float LayerDepth_Debugger_Player = 0.06f;
        public const float LayerDepth_Debugger_Foreground = 0.05f;

        public const float LayerDepth_Background = 0.50f;
        public const float LayerDepth_Terrain = 0.09f;
        public const float LayerDepth_Interactive = 0.08f;
        public const float LayerDepth_NPC = 0.07f;
        public const float LayerDepth_Player = 0.06f;
        public const float LayerDepth_Foreground = 0.05f;
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
            Engine.Window_HUD_Size_Tiles = new Vector2(0, 50);
            Engine.Window_TileGridSize = new Vector2(10, 10);

            Engine.Register_Tiles = new List<Tile>();
            Engine.Register_Worlds = new List<World>();
            Engine.Register_Players = new List<Player>();
            Engine.Register_Npc = new List<Entity>();

            Engine.PointerCurrent_Player = 0;
            Engine.PointerCurrent_World = 0;

            Engine.Counter_Tiles = 0;
            Engine.CounterWorld = 0;
            Engine.Counter_Players = 0;
            Engine.Counter_Npcs = 0;

            Engine.MainCamera = new Camera("Main Camera", Vector2.Zero);

            Engine.VisualDebugger = false;
            Engine.Load();
            Engine.Register_Worlds.Add(Generator.GenerateWorld(Generator.RandomSeed(6), Generator.RandomInt(1, 5)));
        }
        // XNA Methods
        public static void Update(GameTime gameTime)
        {
            try
            {
                if (Engine.Register_Worlds.Count > 0 && Engine.GetCurrentZone() != null)
                {
                    Engine.GetCurrentZone().Update(gameTime);
                }
                if (Engine.Register_Players.Count > 0)
                {
                    Engine.GetCurrentPlayer().Update(gameTime);
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
                if (Engine.Register_Worlds.Count > 0 && Engine.GetCurrentZone() != null)
                {
                    Engine.GetCurrentZone().Draw();
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
        public static World GetCurrentWorld()
        {
            try
            {
                return Engine.Register_Worlds[Engine.PointerCurrent_World];
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
                return null;
            }
        }
        public static Zone GetCurrentZone()
        {
            try
            {
                return Engine.Register_Worlds[Engine.PointerCurrent_World].GetCurrentZone();
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
                Engine.LoadWorlds();
                Engine.LoadPlayers();
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
                                Engine.Window_HUD_Size_Tiles = new Vector2(0, int.Parse(xmlReader.GetAttribute("hud_size")));
                            }
                            if (xmlReader.Name == "tile_set")
                            {
                                // Load the Tileset settings
                                Tile.Dimensions = new Vector2(int.Parse(xmlReader.GetAttribute("width")), int.Parse(xmlReader.GetAttribute("height")));
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
                            Engine.Register_Tiles.Add(new Tile(tag, new Vector2(src_frame_x, src_frame_y), colour, Engine.LayerDepth_Terrain, id, tileType));

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
        public static void LoadWorlds()
        {
            try
            {
                Engine.CounterWorld = 0;  // Resets the level counter

                if (!Directory.Exists(Engine.ConfigDirectory_Worlds))
                {
                    Directory.CreateDirectory(Engine.ConfigDirectory_Worlds);
                }
                string[] worldList = Directory.GetDirectories(Engine.ConfigDirectory_Worlds);

                for (int w = 0; w < worldList.Length; w++)
                {
                    string[] splitWorldDir = worldList[w].Split('/');

                    World newWorld = new World(splitWorldDir[2]);
                    string[] zoneList = Directory.GetFiles(worldList[w]);
                    for (int z = 0; z < zoneList.Length; z++)
                    {
                        string src = zoneList[z];
                        newWorld.AddZoneToWorld(new Zone(src));
                    }
                    Engine.Register_Worlds.Add(newWorld);
                    Engine.CounterWorld++;
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
                    Engine.CreateBlankSave(Generator.RandomString(10));
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
        public static void CreateBlankSave(string playerName)
        {
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(Engine.ConfigDirectory_SaveData + playerName + ".dat"))
                {
                    #region // Write a default save file
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteWhitespace("\r\n");
                    xmlWriter.WriteStartElement("save_data");
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("tag");
                    xmlWriter.WriteAttributeString("value", "Player");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("src");
                    xmlWriter.WriteAttributeString("value", "entity/player");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("src_frame_pos_x");
                    xmlWriter.WriteAttributeString("value", "0");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("src_frame_pos_y");
                    xmlWriter.WriteAttributeString("value", "0");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("src_frame_size");
                    xmlWriter.WriteAttributeString("value", "48");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("position_x");
                    xmlWriter.WriteAttributeString("value", "48");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("position_y");
                    xmlWriter.WriteAttributeString("value", "68");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("hp");
                    xmlWriter.WriteAttributeString("value", "10");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("gold");
                    xmlWriter.WriteAttributeString("value", "0");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteWhitespace("\r\n");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                    #endregion
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
    }
}