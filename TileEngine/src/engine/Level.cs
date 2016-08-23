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
        public enum WorldType { Plains, Forest, Cave, Mountain, Volcano, Snow, Ocean, }
        public string tag { get; protected set; }
        public int index { get; protected set; }
        public WorldType worldType { get; protected set; }
        public Vector2 gridSize_Tiles { get; protected set; }
        public Vector2 gridSize_Pixels { get { return gridSize_Tiles * Tile.TileDimensions; } }
        public Vector2 positionPlayerStart_Grid { get; protected set; }
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
        public Level(string src)
        {
            try
            {
                this.tag = "";
                this.index = -1;
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
        public bool IsTileSolid(Vector2 gridPositionToCheck)
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
        public bool SetTileType(Vector2 gridPositionToSet, Tile newTile)
        {
            try
            {
                map_Base[(int)(gridPositionToSet.X), (int)(gridPositionToSet.Y)].Copy(newTile);
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
        public void SetPlayerStartGridPosition(Vector2 newPosition)
        {
            try
            {
                positionPlayerStart_Grid = newPosition;
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
                        if (xmlReader.Name == "base_tile_map")
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
        public bool Save(string tag, int index, WorldType worldType)
        {
            try
            {
                this.tag = tag;
                this.index = index;
                this.worldType = worldType;


                using (XmlWriter xmlWriter = XmlWriter.Create(Engine.ConfigDirectory_Levels + tag + ".lvl"))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteWhitespace("\r\n");
                    xmlWriter.WriteStartElement("level");

                    xmlWriter.WriteAttributeString("index", index.ToString());
                    xmlWriter.WriteAttributeString("tag", tag);
                    xmlWriter.WriteWhitespace("\r\n\t");

                    xmlWriter.WriteStartElement("base_tile_map");

                    string tileMapString = "";
                    for (int y = 0; y < gridSize_Tiles.Y; y++)
                    {
                        tileMapString += "\r\n\t\t";
                        for (int x = 0; x < gridSize_Tiles.X; x++)
                        {
                            tileMapString += "0" + map_Base[x, y].id + ",";
                        }
                        tileMapString += ";";
                    }

                    xmlWriter.WriteValue(tileMapString);
                    xmlWriter.WriteWhitespace("\r\n\t");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteWhitespace("\r\n");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
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
    }
}
