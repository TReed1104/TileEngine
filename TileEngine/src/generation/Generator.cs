using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TileEngine.Level;

namespace TileEngine
{
    public static class Generator
    {
        // Vars
        public static Random RandomNumberGenerator { get; set; }
        public const string AlphanumericSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const string CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        // Constructors
        static Generator()
        {
            RandomNumberGenerator = new Random();
        }

        // Methods
        public static void SetWorldBounds(Level level, WorldType worldType)
        {
            for (int y = 0; y < level.gridSize_Tiles.Y; y++)
            {
                if (y == 0 || y == (level.gridSize_Tiles.Y - 1))
                {
                    for (int x = 0; x < level.gridSize_Tiles.X; x++)
                    {
                        switch (worldType)
                        {
                            case WorldType.Plains:
                                level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Forest:
                                level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Cave:
                                level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Mountain:
                                level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Volcano:
                                level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Snow:
                                level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                                break;
                            case WorldType.Ocean:
                                level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
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
                            level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Forest:
                            level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Cave:
                            level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Mountain:
                            level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Volcano:
                            level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Snow:
                            level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        case WorldType.Ocean:
                            level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                            level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public static Level GenerateWorld(WorldType worldTypeToGenerate)
        {
            try
            {
                Level newLevel = new Level();
                newLevel.SetTileGridSize(new Vector2(100, 100));
                newLevel.SetPlayerStartGridPosition(new Vector2(0, 0));

                // Generation
                SetWorldBounds(newLevel, worldTypeToGenerate);

                // If the register does not exist, generate it.
                string tag = worldTypeToGenerate + "_" + Generator.RandomString(4, 15);
                newLevel.Save(tag, Engine.Register_Levels.Count, worldTypeToGenerate);

                return newLevel;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Generator", methodName, error.Message));
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
