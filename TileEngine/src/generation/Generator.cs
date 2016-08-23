using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TileEngine.Level;

namespace TileEngine
{
    public class Generator
    {
        // Vars
        public static Random RandomNumberGenerator { get; set; }
        public const string AlphanumericSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const string CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        protected WorldType worldType { get; set; }
        protected Level generatedLevel { get; set; }

        // Constructors
        static Generator()
        {
            RandomNumberGenerator = new Random();
        }
        public Generator()
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
                                generatedLevel.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Forest:
                                generatedLevel.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Cave:
                                generatedLevel.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Mountain:
                                generatedLevel.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Volcano:
                                generatedLevel.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Snow:
                                generatedLevel.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Ocean:
                                generatedLevel.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
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
                            generatedLevel.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTileType(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Forest:
                            generatedLevel.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTileType(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Cave:
                            generatedLevel.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTileType(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Mountain:
                            generatedLevel.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTileType(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Volcano:
                            generatedLevel.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTileType(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Snow:
                            generatedLevel.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTileType(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Ocean:
                            generatedLevel.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            generatedLevel.SetTileType(new Vector2(generatedLevel.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public Level GenerateWorld(WorldType worldType)
        {
            try
            {
                this.worldType = worldType;
                generatedLevel = new Level();
                generatedLevel.SetTileGridSize(new Vector2(100, 100));
                generatedLevel.SetPlayerStartGridPosition(new Vector2(0, 0));
                SetWorldBounds();


                // If the register does not exist, generate it.
                string tag = worldType + "_" + Generator.RandomString(4, 15);
                generatedLevel.Save(tag, Engine.Register_Levels.Count, worldType);
                return generatedLevel;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return null;
            }
        }

        // Randomiser Methods
        public static int RandomInt()
        {
            return RandomNumberGenerator.Next();
        }
        public static int RandomInt(int max)
        {
            return RandomNumberGenerator.Next(max);
        }
        public static int RandomInt(int min, int max)
        {
            return RandomNumberGenerator.Next(min, max);
        }
        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(CharacterSet, length).Select(s => s[RandomNumberGenerator.Next(s.Length)]).ToArray());
        }
        public static string RandomString(int minLength, int maxLength)
        {
            int length = RandomNumberGenerator.Next(minLength, maxLength);
            return new string(Enumerable.Repeat(CharacterSet, length).Select(s => s[RandomNumberGenerator.Next(s.Length)]).ToArray());
        }
        public static string RandomSeed(int length)
        {
            return new string(Enumerable.Repeat(AlphanumericSet, length).Select(s => s[RandomNumberGenerator.Next(s.Length)]).ToArray());
        }
    }
}
