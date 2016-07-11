using Microsoft.Xna.Framework;
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
        public string tag { get; set; }
        public enum CameraType { Manual, Follow, Snapped, };
        public CameraType cameraType { get; set; }
        public Vector2 position_Base { get; set; }
        public Vector2 position_Grid { get; set; }

        static Camera()
        {

        }
        public Camera(string tag, Vector2 positionBase)
        {
            try
            {
                this.tag = tag;
                this.cameraType = CameraType.Follow;
                this.position_Base = positionBase;
                int gridX = (int)(this.position_Base.X / Engine.TileDimensions.X);
                int gridY = (int)(this.position_Base.Y / Engine.TileDimensions.Y);
                this.position_Grid = new Vector2(gridX, gridY);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public void Update(GameTime gameTime)
        {
            try
            {
                int gridX = (int)(this.position_Base.X / Engine.TileDimensions.X);
                int gridY = (int)(this.position_Base.Y / Engine.TileDimensions.Y);
                this.position_Grid = new Vector2(gridX, gridY);
                Engine.WindowTransformationMatrix = Matrix.Identity;
                Engine.WindowTransformationMatrix = Matrix.CreateTranslation(new Vector3(-position_Base.X, -position_Base.Y, 0.0f));
                Engine.WindowTransformationMatrix *= Matrix.CreateScale(Engine.WindowScaleModifier);

            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }

        public void CheckBounds()
        {
            try
            {
                // Left bounds check
                if (position_Base.X < 0)
                {
                    position_Base = new Vector2(0, position_Base.Y);
                }
                // Right bounds check
                if (position_Base.X > (Engine.GetCurrentLevel().gridSize_Pixels.X - Engine.WindowTileGridSize.X))
                {
                    position_Base = new Vector2((Engine.GetCurrentLevel().gridSize_Pixels.X - Engine.WindowTileGridSize.X), position_Base.Y);
                }
                // Up bounds check
                if (position_Base.Y < 0)
                {
                    position_Base = new Vector2(position_Base.X, 0);
                }
                // Down bounds check
                if (position_Base.Y > (Engine.GetCurrentLevel().gridSize_Pixels.Y - Engine.WindowTileGridSize.Y))
                {
                    position_Base = new Vector2(position_Base.X, (Engine.GetCurrentLevel().gridSize_Pixels.Y - Engine.WindowTileGridSize.Y));
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
