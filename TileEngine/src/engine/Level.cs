using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TileEngine
{
    public class Level
    {
        // Vars
        public int index { get; set; }
        public string tag { get; set; }
        public Vector2 gridSize_Tiles { get; protected set; }
        public Vector2 gridSize_Pixels { get { return gridSize_Tiles * Tile.TileDimensions; } }
        public Vector2 positionPlayerStart_Grid { get; set; }
        public Vector2 positionPlayerStart_Pixel { get { return positionPlayerStart_Grid * Tile.TileDimensions; } }
        protected Tile[,] map_Base { get; set; }
        public Tile[,] map_Copy { get; private set; }
        protected List<Entity> registerNPC { get; set; }

        // Constructors
        static Level()
        {

        }
        public Level()
        {
            try
            {
                this.tag = "";
                this.index = -1;

                this.gridSize_Tiles = Vector2.Zero;
                this.positionPlayerStart_Grid = Vector2.Zero;
                this.registerNPC = new List<Entity>();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public Level(string tag, int index, string src)
        {
            try
            {
                this.tag = tag;
                this.index = index;

                this.gridSize_Tiles = Vector2.Zero;
                this.positionPlayerStart_Grid = Vector2.Zero;
                this.registerNPC = new List<Entity>();
                Load(src);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // XNA Methods
        public virtual void Update(GameTime gameTime)
        {
            try
            {
                // Update all the Entities.
                for (int i = 0; i < registerNPC.Count; i++)
                {
                    registerNPC[i].Update(gameTime);
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public virtual void Draw()
        {
            try
            {
                for (int y = 0; y < Engine.Window_TileGridSize.Y; y++)
                {
                    for (int x = 0; x < Engine.Window_TileGridSize.X; x++)
                    {
                        int drawX = (int)(Engine.MainCamera.position_Grid.X + x);
                        int drawY = (int)(Engine.MainCamera.position_Grid.Y + y);
                        map_Copy[drawX, drawY].Draw();
                        if (drawX + 1 < gridSize_Tiles.X)
                        {
                            map_Copy[drawX + 1, drawY].Draw();
                        }
                        if (drawY + 1 < gridSize_Tiles.Y)
                        {
                            map_Copy[drawX, drawY + 1].Draw();
                        }
                        if (drawX + 1 < gridSize_Tiles.X && drawY + 1 < gridSize_Tiles.Y)
                        {
                            map_Copy[drawX + 1, drawY + 1].Draw();
                        }
                    }
                }
                // Update all the Entities.
                for (int i = 0; i < registerNPC.Count; i++)
                {
                    registerNPC[i].Draw();
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // TileGrid methods
        public bool CheckCell(Vector2 gridPositionToCheck)
        {
            try
            {
                if (gridPositionToCheck.X < 0) return false;
                if (gridPositionToCheck.X >= gridSize_Tiles.X) return false;
                if (gridPositionToCheck.Y < 0) return false;
                if (gridPositionToCheck.Y >= gridSize_Tiles.Y) return false;
                return (map_Copy[(int)(gridPositionToCheck.X), (int)(gridPositionToCheck.Y)].type == Tile.TileType.Empty);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return false;
            }
        }
        public bool SetTile(Vector2 gridPositionToSet, Tile newTile)
        {
            try
            {
                map_Base[(int)(gridPositionToSet.X), (int)(gridPositionToSet.Y)] = newTile;
                map_Copy = (Tile[,])map_Base.Clone();
                return true;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return false;
            }
        }
        public void SetTileGridSize(Vector2 newGridSize)
        {
            try
            {
                gridSize_Tiles = newGridSize;
                InitialiseMap();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Load methods
        protected void InitialiseMap()
        {
            try
            {
                // This prevents there being unassigned tile cells and makes sure each cell has the correct positioning in the grid
                map_Base = new Tile[(int)gridSize_Tiles.X, (int)gridSize_Tiles.Y];
                for (int y = 0; y < gridSize_Tiles.Y; y++)
                {
                    for (int x = 0; x < gridSize_Tiles.X; x++)
                    {
                        map_Base[x, y] = new Tile("NULL", Vector2.Zero, Color.White, Engine.LayerDepth_Background, 00, Tile.TileType.Empty);
                        map_Base[x, y].position_Base = new Vector2(x * Tile.TileDimensions.X, y * Tile.TileDimensions.Y);
                        map_Base[x, y].position_Grid = new Vector2(x, y);
                        map_Base[x, y].position_Draw = new Vector2(x * Tile.TileDimensions.X, y * Tile.TileDimensions.Y) + Engine.Window_GameRender_Offset;

                    }
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected void Load(string levelSrc)
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create(levelSrc);
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.Name == "level")
                        {
                            index = int.Parse(xmlReader.GetAttribute("index"));
                            tag = xmlReader.GetAttribute("tag");
                        }
                        if (xmlReader.Name == "tile_map")
                        {
                            string rawLevel = xmlReader.ReadElementString();
                            // Remove the formatting
                            rawLevel = rawLevel.Replace("\n", "");
                            rawLevel = rawLevel.Replace("\t", "");
                            rawLevel = rawLevel.Replace("\r", "");
                            rawLevel = rawLevel.Replace(" ", "");

                            // Calculate the dimensions of the map
                            string[] counterY = rawLevel.Split(';');
                            string[] counterX = counterY[0].Split(',');
                            gridSize_Tiles = new Vector2(counterX.Length - 1, counterY.Length - 1);

                            // Initialise a blank map
                            InitialiseMap();

                            // Populate the level using map data taken from the .lvl file
                            string[] levelY = rawLevel.Split(';');
                            for (int y = 0; y < gridSize_Tiles.Y; y++)
                            {
                                string[] levelX = levelY[y].Split(',');
                                for (int x = 0; x < gridSize_Tiles.X; x++)
                                {
                                    // populates the current cell with the tile specified in the .lvl file
                                    int pointer_TileRegister = int.Parse(levelX[x]);
                                    map_Base[x, y].Copy(Engine.Register_Tiles[pointer_TileRegister]);
                                }
                            }
                            map_Copy = (Tile[,])map_Base.Clone();
                        }
                    }
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Save methods
        protected void AddLevelToRegister()
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.PreserveWhitespace = true;          // prevents strange formatting, but then needs new whitespaces.
                xmlDocument.Load(Engine.ConfigFullPath_LevelRegister);

                // Amend the level count
                XmlNode xmlNodeLevels = xmlDocument.SelectSingleNode("level_register");
                XmlAttribute xmlAttribute_levelCounter = xmlNodeLevels.Attributes["level_count"];
                int numberOfLevels = int.Parse(xmlAttribute_levelCounter.Value);
                int updatedNumberOfLevels = numberOfLevels + 1;
                xmlAttribute_levelCounter.Value = updatedNumberOfLevels.ToString();

                // Creates the new Node.
                XmlNode newNode = xmlDocument.CreateNode(XmlNodeType.Element, "level", null);

                XmlAttribute levelIndex = xmlDocument.CreateAttribute("index");
                levelIndex.Value = (updatedNumberOfLevels - 1).ToString();
                newNode.Attributes.Append(levelIndex);

                XmlAttribute levelTag = xmlDocument.CreateAttribute("tag");
                levelTag.Value = tag;
                newNode.Attributes.Append(levelTag);

                XmlAttribute levelSource = xmlDocument.CreateAttribute("src");
                levelSource.Value = Engine.ConfigDirectory_Levels + tag + ".lvl";
                newNode.Attributes.Append(levelSource);

                // Formatting whitespaces - Used because XMLDocument has flaws with its ability to preserve whitespace.
                XmlWhitespace tabs = xmlDocument.CreateWhitespace("\t");
                XmlWhitespace carriageReturn = xmlDocument.CreateWhitespace("\r\n");

                // Edits the XML file with the formatting and new Node.
                xmlNodeLevels.AppendChild(tabs);
                xmlNodeLevels.AppendChild(newNode);
                xmlNodeLevels.AppendChild(carriageReturn);

                // Saves the XML file.
                xmlDocument.Save(Engine.ConfigFullPath_LevelRegister);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Level", methodName, error.Message));
            }
        }
        protected bool SaveLevelToFile()
        {
            try
            {
                // If the register does not exist, generate it.
                
                string randomisedName = Generator.RandomString(4,15);
                string levelName = tag + "_" + randomisedName + ".lvl";
                using (XmlWriter xmlWriter = XmlWriter.Create(levelName))
                {
                    xmlWriter.WriteStartDocument();

                    xmlWriter.Close();
                }
                return true;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return false;
            }
        }
        public void Save()
        {
            try
            {
                if (SaveLevelToFile())
                {
                    // If the level was saved, add it to the register.
                    AddLevelToRegister();
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
    }
}
