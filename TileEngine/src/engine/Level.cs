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
        public Vector2 gridSize_Pixels { get; set; }
        public Vector2 positionPlayerStart_Pixel { get; set; }
        public Vector2 positionPlayerStart_Grid { get; set; }
        protected Tile[,] mapBase { get; set; }
        public Tile[,] mapCopy { get; private set; }
        private List<Entity> registerNPC { get; set; }

        // Constructors
        static Level()
        {

        }
        public Level()
        {

        }

        // Methods
        public virtual void Update(GameTime gameTime)
        {
            // Update all the Entities.
            for (int i = 0; i < registerNPC.Count; i++)
            {
                registerNPC[i].Update(gameTime);
            }
        }
        public virtual void Draw()
        {
            for (int y = 0; y < Engine.WindowTileGridSize.Y; y++)
            {
                for (int x = 0; x < Engine.WindowTileGridSize.X; x++)
                {
                    int drawX = (int)(Engine.GetCurrentPlayer().camera.position_Grid.X + x);
                    int drawY = (int)(Engine.GetCurrentPlayer().camera.position_Grid.Y + y);
                    mapCopy[drawX, drawY].Draw();
                    if (drawX + 1 < gridSize_Tiles.X)
                    {
                        mapCopy[drawX + 1, drawY].Draw();
                    }
                    if (drawY + 1 < gridSize_Tiles.Y)
                    {
                        mapCopy[drawX, drawY + 1].Draw();
                    }
                    if (drawX + 1 < gridSize_Tiles.X && drawY + 1 < gridSize_Tiles.Y)
                    {
                        mapCopy[drawX + 1, drawY + 1].Draw();
                    }
                }
            }
            // Update all the Entities.
            for (int i = 0; i < registerNPC.Count; i++)
            {
                registerNPC[i].Draw();
            }
        }
    }
}
