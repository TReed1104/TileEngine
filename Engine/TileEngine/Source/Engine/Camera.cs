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
        public Vector2 position { get; protected set; }
        public Vector2 viewPortSize { get { return Engine.GameWindowSize; } }
        public float rotation { get; protected set; }
        public float zoom { get; set; }
        public Matrix transformationMatrix { get; protected set; }
        public PlayerAgent focus { get; protected set; }
        protected float deltaTime { get; set; }

        public Camera(PlayerAgent focus)
        {
            this.focus = focus;
            this.rotation = 0.0f;
            this.deltaTime = 0.0f;
            this.zoom = 1.0f;
        }

        public void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;   // Calculate the DeltaTime


            bool useLerp = false;
            if (useLerp)
            {
                Vector2 newPosition = focus.position + (focus.boundingBox_Size / 2);
                position = Vector2.Lerp(position, newPosition, 2.0f * deltaTime);
            }
            else
            {
                position = focus.position + (focus.boundingBox_Size / 2);
            }

            Matrix cameraTarget = Matrix.CreateTranslation(new Vector3(-position, 0));
            Matrix cameraRotation = Matrix.CreateRotationZ(rotation);
            Matrix cameraZoom = Matrix.CreateScale(zoom);
            Matrix viewPortOffset = Matrix.CreateTranslation(new Vector3((Engine.ViewPortSize / 2), 0));
            Matrix windowScale = Matrix.CreateScale(Engine.GameWindowScale);

            transformationMatrix = cameraTarget * cameraRotation * cameraZoom * viewPortOffset;

        }
        public bool IsObjectVisible(BaseGameObject gameObject)
        {
            // Returns if the object is visible to decide whether or not an object should be drawn.
            return true;
        }
        public void SetRotation(float newRotation)
        {
            this.rotation = newRotation;
        }
        public void SetFocus(PlayerAgent newFocus)
        {
            this.focus = newFocus;
        }
    }
}