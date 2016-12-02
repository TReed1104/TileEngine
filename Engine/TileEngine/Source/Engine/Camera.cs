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
        // Enums
        public enum CameraType { Follow, Snapped, Manual, };
        // Vars
        public string tag { get; set; }
        public CameraType cameraType { get; set; }
        public Vector2 position_Base { get; set; }
        public Vector2 position_Grid { get; set; }

        // Constructors
        static Camera()
        {

        }
        public Camera(string tag, Vector2 positon)
        {
            try
            {
                this.tag = tag;
                this.cameraType = CameraType.Follow;
                this.position_Base = positon;
                this.position_Grid = Engine.ConvertPosition_PixelToGrid(position_Base);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // XNA methods
        public void Update(GameTime gameTime, BaseGameObject cameraTarget)
        {
            try
            {
                if (cameraType == CameraType.Follow)
                {
                    float newCameraX = cameraTarget.position_Base.X - (Engine.Window_PixelGridSize.X / 2);
                    float newCameraY = cameraTarget.position_Base.Y - ((Engine.Window_PixelGridSize.Y - Engine.Window_HUD_Size_Pixels.Y) / 2);
                    position_Base = new Vector2(newCameraX, newCameraY);
                }
                CheckBounds();
                position_Grid = Engine.ConvertPosition_PixelToGrid(position_Base);
                Engine.Window_TransformationMatrix = Matrix.Identity;
                Engine.Window_TransformationMatrix = Matrix.CreateTranslation(new Vector3(-position_Base.X, -position_Base.Y, 0.0f));
                Engine.Window_TransformationMatrix *= Matrix.CreateScale(Engine.Window_Scaler);

            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Camera control methods
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
                if (position_Base.X > (Engine.GetCurrentLevel().gridSize_Pixels.X - Engine.Window_PixelGridSize.X))
                {
                    position_Base = new Vector2((Engine.GetCurrentLevel().gridSize_Pixels.X - Engine.Window_PixelGridSize.X), position_Base.Y);
                }
                // Up bounds check
                if (position_Base.Y < 0)
                {
                    position_Base = new Vector2(position_Base.X, 0);
                }
                // Down bounds check
                if (position_Base.Y > (Engine.GetCurrentLevel().gridSize_Pixels.Y - (Engine.Window_PixelGridSize.Y - Engine.Window_HUD_Size_Pixels.Y)))
                {
                    position_Base = new Vector2(position_Base.X, (Engine.GetCurrentLevel().gridSize_Pixels.Y - (Engine.Window_PixelGridSize.Y - Engine.Window_HUD_Size_Pixels.Y)));
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