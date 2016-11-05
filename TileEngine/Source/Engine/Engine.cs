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
        #region // Directory Vars
        public const string ConfigFileName_Engine = "Engine.conf";
        public const string ConfigFileName_Tileset = "Tileset.conf";

        public const string ConfigDirectory_Engine = "Config/";
        public const string ConfigDirectory_Levels = "Content/Levels/";
        public const string ConfigDirectory_Entities = "Content/Entities/";
        public const string ConfigDirectory_SaveData = "Content/Saves/";
        public const string ConfigDirectory_Textures = "Content/Textures/";

        public static string ConfigFullPath_EngineConfig { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Engine; } }
        public static string ConfigFullPath_Tileset { get { return Engine.ConfigDirectory_Engine + Engine.ConfigFileName_Tileset; } }
        #endregion
        #region // Window Vars
        public static string Window_Title { get; private set; }
        public static int FrameRate_Max { get; private set; }
        public static Matrix Window_TransformationMatrix { get; set; }
        public static float Window_Scaler { get; set; }
        public static Vector2 Window_HUD_Size_Tiles { get; set; }
        public static Vector2 Window_HUD_Size_Pixels { get { return Window_HUD_Size_Tiles * Tile.Dimensions; } }
        public static Vector2 Window_TileGridSize { get; private set; }
        public static Vector2 Window_PixelGridSize { get { return (Engine.Window_TileGridSize * Tile.Dimensions); } }
        public static Vector2 Window_DimensionsPixels_Base { get { return (Engine.Window_TileGridSize * Tile.Dimensions); } }
        public static Vector2 Window_DimensionsPixels_Scaled { get { return Engine.Window_DimensionsPixels_Base * Engine.Window_Scaler; } }

        public static Vector2 Camera_RenderGridSize_Tiles { get { return Engine.Window_TileGridSize - Engine.Window_HUD_Size_Tiles; } }
        #endregion
        #region // Register Vars
        public static List<Tile> Register_Tiles { get; set; }
        public static List<Level> Register_Levels { get; set; }
        public static List<Player> Register_Players { get; set; }
        public static List<Entity> Register_Entity { get; set; }
        public static List<Texture2D> Register_Textures { get; set; }
        #endregion
        #region // Pointer Vars
        public static int PointerCurrent_Player { get; set; }
        public static int PointerCurrent_Level { get; set; }
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
        public static Camera PlayerCamera { get; set; }
        #endregion
        #region // Debugger Vars
        public static bool VisualDebugger { get; set; }
        public static bool isEngineInTestMode { get; set; }
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
            Engine.Register_Levels = new List<Level>();
            Engine.Register_Players = new List<Player>();
            Engine.Register_Entity = new List<Entity>();
            Engine.Register_Textures = new List<Texture2D>();

            Engine.PointerCurrent_Player = 0;
            Engine.PointerCurrent_Level = 0;

            Engine.PlayerCamera = new Camera("Player Camera", Vector2.Zero);

            Engine.VisualDebugger = false;
            Engine.isEngineInTestMode = true;
        }

        // Runtime methods
        public static void Update(GameTime gameTime)
        {
            try
            {
                if (Engine.Register_Levels.Count > 0 && Engine.GetCurrentLevel() != null)
                {
                    Engine.GetCurrentLevel().Update(gameTime);
                }
                if (Engine.Register_Players.Count > 0)
                {
                    Engine.GetCurrentPlayer().Update(gameTime);
                    Engine.PlayerCamera.Update(gameTime, Engine.GetCurrentPlayer());
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void Draw(Game game)
        {
            try
            {
                game.GraphicsDevice.Clear(Color.Magenta);
                Engine.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, Engine.Window_TransformationMatrix);
                if (Engine.Register_Levels.Count > 0 && Engine.GetCurrentLevel() != null)
                {
                    Engine.GetCurrentLevel().Draw();
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

        // Engine setup
        private static void LoadEngineConfig()
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
                            if (xmlReader.Name == "tile_settings")
                            {
                                // Load the Tileset settings
                                Tile.Dimensions = new Vector2(int.Parse(xmlReader.GetAttribute("width")), int.Parse(xmlReader.GetAttribute("height")));
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
        private static void InitialiseGameWindow(Game game)
        {
            try
            {
                Engine.Window_TransformationMatrix = Matrix.Identity;
                Engine.Window_TransformationMatrix *= Matrix.CreateScale(Engine.Window_Scaler);
                Engine.GraphicsDevideManager.PreferredBackBufferWidth = (int)Engine.Window_DimensionsPixels_Scaled.X;
                Engine.GraphicsDevideManager.PreferredBackBufferHeight = (int)Engine.Window_DimensionsPixels_Scaled.Y;
                game.Window.Title = Engine.Window_Title;
                Engine.GraphicsDevideManager.ApplyChanges();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        public static void SetupEngine(Game game)
        {
            try
            {
                #region // Default MonoGame Setup
                Engine.GraphicsDevideManager = new GraphicsDeviceManager(game);
                game.Content.RootDirectory = "Content";
                game.TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / Engine.FrameRate_Max);
                game.IsFixedTimeStep = false;
                #endregion

                // Engine Setup calls
                Engine.LoadEngineConfig();
                Engine.InitialiseGameWindow(game);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }

        // Content loading
        private static void LoadTileset()
        {
            try
            {
                // Check for the config file.
                if (File.Exists(Engine.ConfigFullPath_Tileset))
                {
                    // Read the config file.
                    XmlReader xmlReader = XmlReader.Create(Engine.ConfigFullPath_Tileset);
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            if (xmlReader.Name == "tileset")
                            {
                                Tile.TileSetTags = xmlReader.GetAttribute("tag");
                            }
                            if (xmlReader.Name == "tile")
                            {
                                // Load the tile
                                string tag = xmlReader.GetAttribute("tag");
                                int src_frame_x = int.Parse(xmlReader.GetAttribute("src_frame_x"));
                                int src_frame_y = int.Parse(xmlReader.GetAttribute("src_frame_y"));
                                Color colour = Engine.ConvertStringToColour(xmlReader.GetAttribute("colour"));
                                int id = int.Parse(xmlReader.GetAttribute("id"));
                                Tile.TileType tileType = Tile.Register_ConvertTileType(xmlReader.GetAttribute("type"));

                                // Add the tile to the register ready for use.
                                Engine.Register_Tiles.Add(new Tile(tag, new Vector2(src_frame_x, src_frame_y), colour, Engine.LayerDepth_Terrain, id, tileType));
                            }
                        }
                    }
                    xmlReader.Close();

                    int indexOfTileSet = Engine.Register_Textures.FindIndex(r => r.Name == "Textures/" + Tile.TileSetTags);
                    Tile.TileSet = Engine.Register_Textures[indexOfTileSet];
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        private static void LoadLevels()
        {
            try
            {

                if (!Directory.Exists(Engine.ConfigDirectory_Levels))
                {
                    Directory.CreateDirectory(Engine.ConfigDirectory_Levels);
                }
                string[] levelList = Directory.GetFiles(Engine.ConfigDirectory_Levels);

                for (int i = 0; i < levelList.Length; i++)
                {
                    Register_Levels.Add(new Level(levelList[i]));
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        private static void ClearLevelCache()
        {
            try
            {
                if (Directory.Exists(Engine.ConfigDirectory_Levels))
                {
                    Directory.Delete(Engine.ConfigDirectory_Levels, true);
                    Engine.Register_Levels.Clear();
                    Directory.CreateDirectory(Engine.ConfigDirectory_Levels);
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        private static void CreateBlankSave(string saveID)
        {
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(Engine.ConfigDirectory_SaveData + saveID))
                {
                    #region // Write a default save file
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteWhitespace("\r\n");
                    xmlWriter.WriteStartElement("save_data");
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("tag");
                    xmlWriter.WriteAttributeString("value", "TEMP_TAG");
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
        private static SaveData LoadSave(string saveID)
        {
            try
            {
                #region // Check if the directory doesn't exist, if not then create it.
                if (!Directory.Exists(Engine.ConfigDirectory_SaveData))
                {
                    Directory.CreateDirectory(Engine.ConfigDirectory_SaveData);
                }
                #endregion

                #region // Check the Save exists, if not create one.
                if (!File.Exists(Engine.ConfigDirectory_SaveData + saveID))
                {
                    Engine.CreateBlankSave(saveID);
                }
                #endregion

                // Load the save
                string tag = "";
                int position_x = -1;
                int position_y = -1;
                float hp = -1;
                int gold = -1;

                // Read the save file.
                XmlReader xmlReader = XmlReader.Create(Engine.ConfigDirectory_SaveData + saveID);
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.Name == "tag")
                        {
                            tag = xmlReader.GetAttribute("value");
                        }
                        if (xmlReader.Name == "position_x")
                        {
                            position_x = int.Parse(xmlReader.GetAttribute("value"));
                        }
                        if (xmlReader.Name == "position_y")
                        {
                            position_y = int.Parse(xmlReader.GetAttribute("value"));
                        }
                        if (xmlReader.Name == "hp")
                        {
                            hp = float.Parse(xmlReader.GetAttribute("value"));
                        }
                        if (xmlReader.Name == "gold")
                        {
                            gold = int.Parse(xmlReader.GetAttribute("value"));
                        }
                    }
                }
                xmlReader.Close();
                return new SaveData(tag, new Vector2(position_x, position_y), hp, gold);

            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
                return null;
            }
        }
        private static void LoadEntities()
        {
            try
            {
                // Get a list of all the entity configs
                string[] listOfEntityConfigs = Directory.GetFiles(Engine.ConfigDirectory_Entities);

                // Go through each config and populate the entity and player registers accordingly
                for (int i = 0; i < listOfEntityConfigs.Length; i++)
                {
                    // Check for the config file.
                    if (File.Exists(listOfEntityConfigs[i]))
                    {
                        bool isPlayer = false;  // Stores if this entity is a player, because players are treated differently.
                        string tag = "";
                        string entityType = "";
                        string textureTag = "";
                        int sourceRectangleSize = 0;
                        Color colour = Color.White;
                        string saveID = "";

                        // Read the config file.
                        XmlReader xmlReader = XmlReader.Create(listOfEntityConfigs[i]);
                        while (xmlReader.Read())
                        {
                            if (xmlReader.NodeType == XmlNodeType.Element)
                            {
                                if (xmlReader.Name == "tag")
                                {
                                    tag = xmlReader.GetAttribute("value");
                                }
                                if (xmlReader.Name == "entity_type")
                                {
                                    entityType = xmlReader.GetAttribute("value");
                                    if (entityType == "Player")
                                    {
                                        isPlayer = true;
                                    }
                                }
                                if (xmlReader.Name == "texture_tag")
                                {
                                    textureTag = xmlReader.GetAttribute("value");
                                }
                                if (xmlReader.Name == "src_frame_size")
                                {
                                    sourceRectangleSize = int.Parse(xmlReader.GetAttribute("value"));
                                }
                                if (xmlReader.Name == "colour")
                                {
                                    colour = Engine.ConvertStringToColour(xmlReader.GetAttribute("value"));
                                }
                                if (xmlReader.Name == "save_data")
                                {
                                    saveID = xmlReader.GetAttribute("value");
                                }
                            }
                        }
                        xmlReader.Close();
                        if (isPlayer)
                        {
                            // Load the save data and create the player
                            int indexOfTexture = Engine.Register_Textures.FindIndex(r => r.Name == "Textures/" + textureTag);   // Get the correct texture for the player
                            SaveData loadedSave = Engine.LoadSave(saveID);

                            Player player = new Player(loadedSave.tag, Engine.Register_Textures[indexOfTexture], loadedSave.position, Vector2.Zero, new Vector2(sourceRectangleSize, sourceRectangleSize), colour, Engine.LayerDepth_Player, loadedSave.hp);
                            Engine.Register_Players.Add(player);
                        }
                        else
                        {

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
        public static void LoadContent(Game game)
        {
            try
            {
                #region // Default MonoGame Setup
                Engine.SpriteBatch = new SpriteBatch(game.GraphicsDevice);
                #endregion


                #region // Load textures and fonts
                string[] rawTextureDirectories = Directory.GetFiles(Engine.ConfigDirectory_Textures);

                for (int i = 0; i < rawTextureDirectories.Length; i++)
                {
                    // Split and trim the string to the required parts
                    string trimmedTexturePath = rawTextureDirectories[i].Replace("Content/", "");
                    string[] splitTexturePath = trimmedTexturePath.Split('.');
                    // Load the texture into the texture register
                    Engine.Register_Textures.Add(game.Content.Load<Texture2D>(splitTexturePath[0]));
                }
                #endregion

                Engine.LoadTileset();
                Engine.LoadLevels();
                Engine.LoadEntities();

                Engine.TestGeneration();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
            }
        }
        
        // Content unloading
        public static void UnloadContent()
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

        // Engine loading Conversion methods
        public static Color ConvertStringToColour(string stringToConvert)
        {
            try
            {
                switch (stringToConvert)
                {
                    case "White":
                        return Color.White;
                    case "Black":
                        return Color.Black;
                    default:
                        return Color.White;
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Engine", methodName, error.Message));
                return Color.White;
            }
        }

        // Testing Methods
        private static void TestGeneration()
        {
            if (Engine.isEngineInTestMode)
            {
                Engine.ClearLevelCache();   // Clear the cache for re-generation for testing.

                // Test generation
                Level temp = new Level("");
                temp.Generate(Randomiser.RandomString(6), Engine.Register_Levels.Count);
                Engine.Register_Levels.Add(temp);
            }
        }
    }
}