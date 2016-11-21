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
    public class Level
    {
        // Vars
        public string tag { get; protected set; }
        public int index { get; protected set; }
        public Vector2 gridSize_Tiles { get; protected set; }
        public Vector2 gridSize_Pixels { get { return gridSize_Tiles * Tile.Dimensions; } }
        public Vector2 positionPlayerStart_Grid { get; protected set; }
        public Vector2 positionPlayerStart_Pixel { get { return positionPlayerStart_Grid * Tile.Dimensions; } }
        protected Tile[,] tilemap { get; set; }
        protected List<NpcAgent> NPCs { get; set; }

        // Constructors
        static Level()
        {

        }
        public Level(string src)
        {
            try
            {
                this.tag = "";
                this.index = -1;
                this.gridSize_Tiles = Vector2.Zero;
                this.positionPlayerStart_Grid = Vector2.Zero;
                this.NPCs = new List<NpcAgent>();
                if (src != "")
                {
                    Load(src);
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }

        // Engine Methods
        public virtual void Update(GameTime gameTime)
        {
            try
            {
                // Update all the Entities.
                for (int i = 0; i < NPCs.Count; i++)
                {
                    NPCs[i].Update(gameTime);
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
                        tilemap[drawX, drawY].Draw();
                        if (drawX + 1 < gridSize_Tiles.X)
                        {
                            tilemap[drawX + 1, drawY].Draw();
                        }
                        if (drawY + 1 < gridSize_Tiles.Y)
                        {
                            tilemap[drawX, drawY + 1].Draw();
                        }
                        if (drawX + 1 < gridSize_Tiles.X && drawY + 1 < gridSize_Tiles.Y)
                        {
                            tilemap[drawX + 1, drawY + 1].Draw();
                        }
                    }
                }
                // Update all the Entities.
                for (int i = 0; i < NPCs.Count; i++)
                {
                    NPCs[i].Draw();
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
                tilemap = new Tile[(int)gridSize_Tiles.X, (int)gridSize_Tiles.Y];
                for (int y = 0; y < gridSize_Tiles.Y; y++)
                {
                    for (int x = 0; x < gridSize_Tiles.X; x++)
                    {
                        tilemap[x, y] = new Tile("EMPTY", Vector2.Zero, Color.White, Engine.LayerDepth_Terrain, 00, Tile.TileType.Empty);
                        tilemap[x, y].positionComponent.position_Base = new Vector2(x * Tile.Dimensions.X, y * Tile.Dimensions.Y);
                        tilemap[x, y].positionComponent.position_Grid = new Vector2(x, y);
                        tilemap[x, y].positionComponent.position_Draw = new Vector2(x * Tile.Dimensions.X, y * Tile.Dimensions.Y) + Engine.Window_HUD_Size_Pixels;
                    }
                }
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Checks
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
                return (tilemap[(int)(gridPositionToCheck.X), (int)(gridPositionToCheck.Y)].type == Tile.TileType.Empty);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return false;
            }
        }
        // Sets
        public void SetCellTile(Vector2 gridPositionToSet, Tile newTile)
        {
            try
            {
                tilemap[(int)(gridPositionToSet.X), (int)(gridPositionToSet.Y)].Copy(newTile);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public void AddNPC(NpcAgent newNPC)
        {
            try
            {
                NPCs.Add(newNPC);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Save/loading
        protected void Load(string levelPath)
        {
            try
            {
                XmlReader xmlReader = XmlReader.Create(levelPath);
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
                                    tilemap[x, y].Copy(Engine.Register_Tiles[pointer_TileRegister]);
                                }
                            }
                        }
                    }
                }
                xmlReader.Close();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public bool Save()
        {
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(Engine.FullPath_Levels + tag + ".lvl"))
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
                            tileMapString += "0" + tilemap[x, y].id + ",";
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
        // Generation
        public void Generate(string tag, int index)
        {
            this.tag = tag;
            this.index = index;
            gridSize_Tiles = new Vector2(120, 80);
            InitialiseMap();
            positionPlayerStart_Grid = new Vector2(0, 0);

            // Fill the grid.
            for (int y = 0; y < gridSize_Tiles.Y; y++)
            {
                for (int x = 0; x < gridSize_Tiles.X; x++)
                {
                    SetCellTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                }
            }

            if (Engine.isEngineInTestMode)
            {
                // Cap the grid
                for (int x = 0; x < gridSize_Tiles.X; x++)
                {
                    SetCellTile(new Vector2(x, 0), Engine.Register_Tiles[2]);
                    SetCellTile(new Vector2(x, gridSize_Tiles.Y - 1), Engine.Register_Tiles[2]);
                }
                for (int y = 0; y < gridSize_Tiles.Y; y++)
                {
                    SetCellTile(new Vector2(0, y), Engine.Register_Tiles[2]);
                    SetCellTile(new Vector2(gridSize_Tiles.X - 1, y), Engine.Register_Tiles[2]);
                }
            }

            Save();
        }
    }
}
