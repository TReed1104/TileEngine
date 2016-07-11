using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class Level
    {
        // Vars
        public string tag { get; set; }
        public Vector2 gridSize_Tiles { get; set; }
        public Vector2 gridSize_Pixels
        {
            get
            {
                return gridSize_Tiles * Engine.TileDimensions;
            }
        }
        public Vector2 positionPlayerStart_Grid { get; set; }
        public Vector2 positionPlayerStart_Pixel
        {
            get
            {
                return positionPlayerStart_Grid * Engine.TileDimensions;
            }
        }
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
                this.tag = "NULL";
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
        public bool Load(string path)
        {
            try
            {
                return true;
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
                return false;
            }
        }
        protected void InitialiseMap()
        {
            try
            {
                map_Base = new Tile[(int)gridSize_Tiles.X, (int)gridSize_Tiles.Y];
                for (int y = 0; y < gridSize_Tiles.Y; y++)
                {
                    for (int x = 0; x < gridSize_Tiles.X; x++)
                    {
                        map_Base[x, y] = new Tile("BLANK", Vector2.Zero, Color.White, Engine.LayerDepth_Background, 00, Tile.TileType.Empty);
                        map_Base[x, y].position_Base = new Vector2(x * Engine.TileDimensions.X, y * Engine.TileDimensions.Y);
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
    }
}
