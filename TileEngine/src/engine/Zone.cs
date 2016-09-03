using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TileEngine
{
    [DebuggerDisplay("{tag}")]
    public class Zone
    {
        // Vars
        public enum ZoneType { Plains, Forest, Cave, Mountain, Volcano, Snow, Ocean, }
        public string tag { get; protected set; }
        public int index { get; protected set; }
        public ZoneType zoneType { get; protected set; }
        public Vector2 gridSize_Tiles { get; protected set; }
        public Vector2 gridSize_Pixels { get { return gridSize_Tiles * Tile.Dimensions; } }
        public Vector2 positionPlayerStart_Grid { get; protected set; }
        public Vector2 positionPlayerStart_Pixel { get { return positionPlayerStart_Grid * Tile.Dimensions; } }
        protected Tile[,] map_Base { get; set; }
        public Tile[,] map_Copy { get; private set; }
        protected List<Entity> registerNPC { get; set; }

        // Constructors
        static Zone()
        {

        }
        public Zone(ZoneType zoneType)
        {
            try
            {
                this.tag = "";
                this.index = -1;
                this.zoneType = zoneType;
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
        public Zone(string src)
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
                for (int y = 0; y < Engine.Camera_RenderGridSize_Tiles.Y; y++)
                {
                    for (int x = 0; x < Engine.Camera_RenderGridSize_Tiles.X; x++)
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
        // Methods
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
                        map_Base[x, y] = new Tile("EMPTY", Vector2.Zero, Color.White, Engine.LayerDepth_Terrain, 00, Tile.TileType.Empty);
                        map_Base[x, y].position_Base = new Vector2(x * Tile.Dimensions.X, y * Tile.Dimensions.Y);
                        map_Base[x, y].position_Grid = new Vector2(x, y);
                        map_Base[x, y].position_Draw = new Vector2(x * Tile.Dimensions.X, y * Tile.Dimensions.Y) + Engine.Window_HUD_Size_Pixels;
                    }
                }
                CopyBaseTileMap();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public bool IsTileValid(Vector2 gridPositionToCheck)
        {
            try
            {
                if (gridPositionToCheck.X < 0) return false;
                if (gridPositionToCheck.X >= gridSize_Tiles.X) return false;
                if (gridPositionToCheck.Y < 0) return false;
                if (gridPositionToCheck.Y >= gridSize_Tiles.Y) return false;
                return true;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return false;
            }
        }
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
        public bool SetCellTile(Vector2 gridPositionToSet, Tile newTile, bool isPermenant)
        {
            try
            {
                if (isPermenant)
                {
                    map_Base[(int)(gridPositionToSet.X), (int)(gridPositionToSet.Y)].Copy(newTile);
                    map_Copy = map_Base.Clone() as Tile[,];
                }
                else
                {
                    map_Copy[(int)(gridPositionToSet.X), (int)(gridPositionToSet.Y)].Copy(newTile);
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
        public void CopyBaseTileMap()
        {
            map_Copy = map_Base.Clone() as Tile[,];
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
                            zoneType = Convert_StringToZoneType(xmlReader.GetAttribute("zone_type"));
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
                            CopyBaseTileMap();
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
        public bool Save(string worldName, string tag, int index, ZoneType zoneType)
        {
            try
            {
                this.tag = tag;
                this.index = index;
                this.zoneType = zoneType;

                using (XmlWriter xmlWriter = XmlWriter.Create(Engine.ConfigDirectory_Worlds + worldName + "/" + tag + ".lvl"))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteWhitespace("\r\n");
                    xmlWriter.WriteStartElement("level");

                    xmlWriter.WriteAttributeString("index", index.ToString());
                    xmlWriter.WriteAttributeString("tag", tag);
                    xmlWriter.WriteAttributeString("zone_type", zoneType.ToString());
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
        public static ZoneType Convert_StringToZoneType(string zoneTypeString)
        {
            switch (zoneTypeString)
            {
                case "Plains":
                    return ZoneType.Plains;
                case "Forest":
                    return ZoneType.Forest;
                case "Cave":
                    return ZoneType.Cave;
                case "Mountain":
                    return ZoneType.Mountain;
                case "Volcano":
                    return ZoneType.Volcano;
                case "Snow":
                    return ZoneType.Snow;
                case "Ocean":
                    return ZoneType.Ocean;
                default:
                    return ZoneType.Plains;
            }
        }
    }
}
