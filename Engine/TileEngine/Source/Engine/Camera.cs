using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class Camera
    {
        public Vector2 position { get; set; }
        public Vector2 position_Grid {  get { return Engine.ConvertPosition_PixelToGrid(position); } }
        public float movementSpeed { get { return Engine.GetCurrentPlayer().movementSpeed_FreeMovement; } }
        public float rotation { get; set; }
        public float scale { get { return Engine.Window_Scaler; } }
        public Matrix transformationMatrix { get; set; }
        private float deltaTime { get; set; }

        public Camera()
        {
            position = Vector2.Zero;
            rotation = 0.0f;
        }

        public void Update(GameTime gameTime, BaseGameObject focus)
        {
            
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;   // Calculate the DeltaTime
            position = new Vector2(focus.position.X - (Engine.Window_PixelGridSize.X / 2), focus.position.Y - (Engine.Window_PixelGridSize.Y / 2));
            CheckBound();

            transformationMatrix = Matrix.Identity * Matrix.CreateTranslation(-position.X, -position.Y, 0) * Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(scale);

        }
        public void CheckBound()
        {
            // Left bounds check
            if (position.X < 0)
            {
                position = new Vector2(0, position.Y);
            }
            // Right bounds check
            if (position.X > (Engine.GetCurrentLevel().gridSize_Pixels.X - Engine.Window_PixelGridSize.X))
            {
                position = new Vector2((Engine.GetCurrentLevel().gridSize_Pixels.X - Engine.Window_PixelGridSize.X), position.Y);
            }
            // Up bounds check
            if (position.Y < 0)
            {
                position = new Vector2(position.X, 0);
            }
            // Down bounds check
            if (position.Y > (Engine.GetCurrentLevel().gridSize_Pixels.Y - (Engine.Window_PixelGridSize.Y - Engine.Window_HUD_Size_Pixels.Y)))
            {
                position = new Vector2(position.X, (Engine.GetCurrentLevel().gridSize_Pixels.Y - (Engine.Window_PixelGridSize.Y - Engine.Window_HUD_Size_Pixels.Y)));
            }
        }
        public bool IsInView(BaseGameObject gameObject)
        {
            if (gameObject.position_Grid.X >= position_Grid.X && gameObject.position_Grid.X < (position_Grid.X + Engine.Window_TileGridSize.X) + 1 && gameObject.position_Grid.Y >= position_Grid.Y && gameObject.position_Grid.Y < (position_Grid.Y + Engine.Window_TileGridSize.Y) + 1)
            {
                return true;
            }
            return false;
        }
    }
}