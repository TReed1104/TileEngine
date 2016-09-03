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
        private enum Direction { None, Left, Right, Up, Down, UpLeft, UpRight, DownLeft, DownRight}
        public static Random RandomNumberGenerator { get; set; }
        private const string AlphanumericSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string CharacterSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        // Constructors
        static Generator()
        {
            RandomNumberGenerator = new Random();
        }

        // Methods
        private static void FillZoneWithBaseTile(Zone zone, Tile baseZoneTile)
        {
            for (int y = 0; y < zone.gridSize_Tiles.Y; y++)
            {
                for (int x = 0; x < zone.gridSize_Tiles.X; x++)
                {
                    zone.SetCellTile(new Vector2(x, y), baseZoneTile, false);
                }
            }
            zone.CopyBaseTileMap(); // Does the copy at the end to speed up the generation
        }
        private static void AddWorldBorder(Zone zone, Tile baseTile)
        {
            for (int y = 0; y < zone.gridSize_Tiles.Y; y++)
            {
                if (y == 0 || y == (zone.gridSize_Tiles.Y - 1))
                {
                    for (int x = 0; x < zone.gridSize_Tiles.X; x++)
                    {
                        zone.SetCellTile(new Vector2(x, y), baseTile, false);
                    }
                }
                else
                {
                    zone.SetCellTile(new Vector2(0, y), baseTile, false);
                    zone.SetCellTile(new Vector2(zone.gridSize_Tiles.X - 1, y), baseTile, false);
                }
            }
            zone.CopyBaseTileMap(); // Does the copy at the end to speed up the generation
        }
        private static List<Vector2> AddGenericTerrain_StartPoints(Zone zone, Tile genericTerrainTile)
        {
            List<Vector2> terrinGenerationPoints = new List<Vector2>();
            int numberOfGenerationSeeds = RandomInt(10, 100);
            for (int i = 0; i < numberOfGenerationSeeds; i++)
            {
                int randX = RandomInt(0, (int)zone.gridSize_Tiles.X);
                int randY = RandomInt(0, (int)zone.gridSize_Tiles.Y);
                zone.SetCellTile(new Vector2(randX, randY), genericTerrainTile, false);

                terrinGenerationPoints.Add(new Vector2(randX, randY));
            }
            zone.CopyBaseTileMap(); // Does the copy at the end to speed up the generation
            return terrinGenerationPoints;
        }
        private static void AddGenericTerrain_Activate(List<Vector2> terrinGenerationPoints, Zone zone)
        {
            // Go to each seed point
            for (int s = 0; s < terrinGenerationPoints.Count; s++)
            {
                int numberOfTerrainGenerationSteps = RandomInt(5, 25);
                switch (zone.zoneType)
                {
                    case ZoneType.Plains:
                        GenericTerrainGeneration_Plains(zone, terrinGenerationPoints[s], numberOfTerrainGenerationSteps);
                        break;
                    case ZoneType.Forest:
                        break;
                    case ZoneType.Cave:
                        break;
                    case ZoneType.Mountain:
                        break;
                    case ZoneType.Volcano:
                        break;
                    case ZoneType.Snow:
                        break;
                    case ZoneType.Ocean:
                        break;
                    default:
                        break;
                }
            }
        }
        private static void GenericTerrainGeneration_Plains(Zone zone, Vector2 startPoint, int generationSteps)
        {
            Vector2 generationPoint = startPoint;
            Direction previousDirection = Direction.None;
            var numberOfPossibleDirection = Enum.GetNames(typeof(Direction)).Length - 1;    // Get how many directions are possible, minus one to remove "none"

            for (int i = 0; i < generationSteps; i++)
            {
                Direction currentDirection = (Direction)RandomInt(1, numberOfPossibleDirection);
                if (currentDirection != previousDirection)
                {
                    Vector2 directionVector = Vector2.Zero;
                    if (currentDirection == Direction.Left) { directionVector = new Vector2(-1, 0); }
                    if (currentDirection == Direction.Right) { directionVector = new Vector2(1, 0); }
                    if (currentDirection == Direction.Up) { directionVector = new Vector2(0, 1); }
                    if (currentDirection == Direction.Down) { directionVector = new Vector2(0, 1); }
                    if (currentDirection == Direction.UpLeft) { directionVector = new Vector2(-1, -1); }
                    if (currentDirection == Direction.UpRight) { directionVector = new Vector2(1, -1); }
                    if (currentDirection == Direction.DownLeft) { directionVector = new Vector2(-1, 1); }
                    if (currentDirection == Direction.DownRight) { directionVector = new Vector2(1, 1); }
                    generationPoint += directionVector;
                    if (zone.IsTileValid(generationPoint))
                    {
                        zone.SetCellTile(generationPoint, Engine.Register_Tiles[2], false);
                    }
                    previousDirection = currentDirection;
                }
            }
            zone.CopyBaseTileMap();
        }

        public static Zone GenerateZone(ZoneType zoneTypeToGenerate, string worldName)
        {
            try
            {
                Zone newZone = new Zone(zoneTypeToGenerate);
                newZone.SetTileGridSize(new Vector2(100, 100));
                newZone.SetPlayerStartGridPosition(new Vector2(0, 0));

                // Generation
                FillZoneWithBaseTile(newZone, Engine.Register_Tiles[3]);
                List<Vector2> terrinGenerationPoints = AddGenericTerrain_StartPoints(newZone, Engine.Register_Tiles[2]);
                AddGenericTerrain_Activate(terrinGenerationPoints, newZone);

                AddWorldBorder(newZone, Engine.Register_Tiles[2]);

                // If the register does not exist, generate it.
                string tag = zoneTypeToGenerate + "_" + Generator.RandomString(5);
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
