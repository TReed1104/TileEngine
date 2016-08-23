using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TileEngine.Zone;

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
        public static void SetWorldBounds(Zone level, ZoneType zoneType)
        {
            for (int y = 0; y < level.gridSize_Tiles.Y; y++)
            {
                if (y == 0 || y == (level.gridSize_Tiles.Y - 1))
                {
                    for (int x = 0; x < level.gridSize_Tiles.X; x++)
                    {
                        level.SetTileType(new Vector2(x, y), Engine.Register_Tiles[1]);
                    }
                }
                else
                {
                    level.SetTileType(new Vector2(0, y), Engine.Register_Tiles[1]);
                    level.SetTileType(new Vector2(level.gridSize_Tiles.X - 1, y), Engine.Register_Tiles[1]);

                }
            }
        }
        public static Zone GenerateZone(ZoneType zoneTypeToGenerate, string worldName)
        {
            try
            {
                Zone newLevel = new Zone();
                newLevel.SetTileGridSize(new Vector2(100, 100));
                newLevel.SetPlayerStartGridPosition(new Vector2(0, 0));

                // Generation
                SetWorldBounds(newLevel, zoneTypeToGenerate);

                // If the register does not exist, generate it.
                string tag = zoneTypeToGenerate + "_" + Generator.RandomString(5);
                newLevel.Save(worldName, tag, Engine.Register_Worlds.Count, zoneTypeToGenerate);

                return newLevel;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", "Generator", methodName, error.Message));
                return null;
            }
        }
        public static World GenerateWorld(string worldName, int numberOfZones)
        {
            try
            {
                Directory.CreateDirectory(Engine.ConfigDirectory_Worlds + worldName);

                World newWorld = new World(worldName);
                for (int i = 0; i < numberOfZones; i++)
                {
                    newWorld.AddZoneToWorld(GenerateZone(ZoneType.Plains, worldName));
                }

                return newWorld;
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
