using Microsoft.Xna.Framework;
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
        public Vector2 positionWorld { get; set; }
        public Vector2 positionGrid { get; set; }

        static Camera()
        {

        }
        public Camera(string tag, Vector2 positionWorld)
        {
            try
            {
                this.tag = tag;
                this.cameraType = CameraType.Follow;
                this.positionWorld = positionWorld;
                int gridX = (int)(this.positionWorld.X / Engine.TileDimensions.X);
                int gridY = (int)(this.positionWorld.Y / Engine.TileDimensions.Y);
                this.positionGrid = new Vector2(gridX, gridY);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
    }
}
