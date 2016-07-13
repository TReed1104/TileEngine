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
        public string src { get; set; }
        public Vector2 gridSize_Tiles { get; set; }
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
        public Level(string tag, int index, string src)
        {
            try
            {
                this.tag = tag;
                this.index = index;

                this.gridSize_Tiles = Vector2.Zero;
                this.positionPlayerStart_Grid = Vector2.Zero;
                this.registerNPC = new List<Entity>();

                InitialiseMap();
                Load(src);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }

        // Methods
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
                for (int y = 0; y < Engine.Window_TileGrid.Y; y++)
                {
                    for (int x = 0; x < Engine.Window_TileGrid.X; x++)
                    {
                        int drawX = (int)(Engine.GetCurrentPlayer().camera.position_Grid.X + x);
                        int drawY = (int)(Engine.GetCurrentPlayer().camera.position_Grid.Y + y);
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

        // Load methods
        protected void InitialiseMap()
        {
            try
            {
                map_Base = new Tile[(int)gridSize_Tiles.X, (int)gridSize_Tiles.Y];
                for (int y = 0; y < gridSize_Tiles.Y; y++)
                {
                    for (int x = 0; x < gridSize_Tiles.X; x++)
                    {
                        map_Base[x, y] = new Tile("NULL", Vector2.Zero, Color.White, Engine.LayerDepth_Background, 00, Tile.TileType.Empty);
                        map_Base[x, y].position_Base = new Vector2(x * Tile.TileDimensions.X, y * Tile.TileDimensions.Y);
                        map_Base[x, y].position_Grid = new Vector2(x, y);

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
                string rawLevel = "";
                XmlReader xmlReader = XmlReader.Create(levelSrc);
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.Name == "level")
                        {
                            index = int.Parse(xmlReader.GetAttribute("index"));
                            tag = xmlReader.GetAttribute("tag");
                            src = xmlReader.GetAttribute("src");
                        }
                        if (xmlReader.Name == "tile_map")
                        {
                            gridSize_Tiles = new Vector2(int.Parse(xmlReader.GetAttribute("width")), int.Parse(xmlReader.GetAttribute("height")));
                            rawLevel = xmlReader.ReadElementString();
                        }
                    }
                }
                InitialiseMap();
                ConvertStringToTiles(rawLevel);
                map_Copy = (Tile[,])map_Base.Clone();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        protected void ConvertStringToTiles(string rawLevel)
        {
            try
            {
                // Remove the formatting
                rawLevel = rawLevel.Replace("\n", "");
                rawLevel = rawLevel.Replace("\t", "");
                rawLevel = rawLevel.Replace("\r", "");
                rawLevel = rawLevel.Replace(" ", "");

                string[] levelY = rawLevel.Split(';');
                for (int y = 0; y < levelY.Length; y++)
                {
                    string[] levelX = rawLevel.Split(',');
                    for (int x = 0; x < levelX.Length; x++)
                    {
                        int pointer_TileRegister = int.Parse(levelX[x]);
                        map_Base[x, y].id = Engine.Register_Tiles[pointer_TileRegister].id;
                        map_Base[x, y].tag = Engine.Register_Tiles[pointer_TileRegister].tag;
                        map_Base[x, y].type = Engine.Register_Tiles[pointer_TileRegister].type;
                        map_Base[x, y].sourceRectangle_Position = Engine.Register_Tiles[pointer_TileRegister].sourceRectangle_Position;
                        map_Base[x, y].colour = Engine.Register_Tiles[pointer_TileRegister].colour;
                    }
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Generation
        protected void Save()
        {
            try
            {
                AddLevelToRegister();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
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
    }
}
