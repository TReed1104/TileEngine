using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class WorldGeneration
    {
        // Vars
        public enum WorldType { Plains, Forest, Cave, Mountain, Volcano, Snow, Ocean, }
        protected WorldType worldType { get; set; }
        protected Level generatedLevel { get; set; }

        // Constructors
        static WorldGeneration()
        {

        }
        public WorldGeneration()
        {

        }

        // Methods
        public void SetWorldBounds()
        {
            for (int y = 0; y < generatedLevel.gridSize_Tiles.Y; y++)
            {
                if (y == 0 || y == (generatedLevel.gridSize_Tiles.Y - 1))
                {
                    for (int x = 0; x < generatedLevel.gridSize_Tiles.X; x++)
                    {
                        switch (worldType)
                        {
                            case WorldType.Plains:
                                generatedLevel.SetTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Forest:
                                generatedLevel.SetTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Cave:
                                generatedLevel.SetTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Mountain:
                                generatedLevel.SetTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Volcano:
                                generatedLevel.SetTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Snow:
                                generatedLevel.SetTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Ocean:
                                generatedLevel.SetTile(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    switch (worldType)
                    {
                        case WorldType.Plains:
                            generatedLevel.SetTile(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTile(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Forest:
                            generatedLevel.SetTile(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTile(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Cave:
                            generatedLevel.SetTile(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTile(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Mountain:
                            generatedLevel.SetTile(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTile(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Volcano:
                            generatedLevel.SetTile(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTile(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Snow:
                            generatedLevel.SetTile(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTile(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Ocean:
                            generatedLevel.SetTile(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTile(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public Level Generate(WorldType worldType)
        {
            try
            {
                this.worldType = worldType;
                generatedLevel = new Level();
                generatedLevel.SetTileGridSize(new Vector2(100, 100));
                SetWorldBounds();
                generatedLevel.tag = worldType.ToString();
                generatedLevel.index = Engine.Register_Levels.Count;
                generatedLevel.Save();
                return generatedLevel;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return null;
            }
        }
    }
}
