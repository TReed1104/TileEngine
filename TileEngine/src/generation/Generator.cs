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
        private static void FillZoneWithBaseTile(Zone zone, Tile baseTile)
        {
            for (int y = 0; y < zone.gridSize_Tiles.Y; y++)
            {
                for (int x = 0; x < zone.gridSize_Tiles.X; x++)
                {
                    zone.SetTileType(new Vector2(x, y), baseTile);
                }
            }
        }
        private static void AddWorldBorder(Zone zone, Tile baseTile)
        {
            for (int y = 0; y < zone.gridSize_Tiles.Y; y++)
            {
                if (y == 0 || y == (zone.gridSize_Tiles.Y - 1))
                {
                    for (int x = 0; x < zone.gridSize_Tiles.X; x++)
                    {
                        zone.SetTileType(new Vector2(x, y), baseTile);
                    }
                }
                else
                {
                    zone.SetTileType(new Vector2(0, y), baseTile);
                    zone.SetTileType(new Vector2(zone.gridSize_Tiles.X - 1, y), baseTile);

                }
            }
        }
        public static Zone GenerateZone(ZoneType zoneTypeToGenerate, string worldName)
        {
            try
            {
                Zone newZone = new Zone();
                newZone.SetTileGridSize(new Vector2(100, 100));
                newZone.SetPlayerStartGridPosition(new Vector2(0, 0));

                // Generation
                FillZoneWithBaseTile(newZone, Engine.Register_Tiles[3]);
                AddWorldBorder(newZone, Engine.Register_Tiles[2]);

                // If the register does not exist, generate it.
                string tag = zoneTypeToGenerate + "_" + Generator.RandomString(5);
                newZone.CopyBaseTileMap();
                newZone.Save(worldName, tag, Engine.Register_Worlds.Count, zoneTypeToGenerate);

                return newZone;
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
                    newWorld.AddZoneToWorld(GenerateZone((ZoneType)Generator.RandomInt(0, 1), worldName));
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
