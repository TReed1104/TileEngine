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
        public Vector2 viewPortSize { get { return Engine.Window_DimensionsPixels_Scaled; } }
        public float rotation { get; protected set; }
        public float zoom { get; set; }
        protected float renderScale { get { return zoom * Engine.Window_Scale; } }
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


            Vector2 newPosition = focus.position + (focus.boundingBox_Size / 2);
            
            // Lerp amount changes dependant on movement speed.
            if (Engine.IsMovementGridSnapped)
            {
                position = Vector2.Lerp(position, newPosition, 1.0f);
            }
            else
            {
                position = Vector2.Lerp(position, newPosition, focus.movementSpeed * deltaTime);
            }

            // Transformation matrices
            Matrix translationMatrix_Focus = Matrix.CreateTranslation(new Vector3(-position, 0));
            Matrix translatioMatrixn_ViewportOffset = Matrix.CreateTranslation(new Vector3((Engine.Window_DimensionsPixels_Scaled / 2), 0));
            Matrix rotationMatrix = Matrix.CreateRotationZ(rotation);
            Matrix scaleMatrix = Matrix.CreateScale(renderScale);
            transformationMatrix = translationMatrix_Focus * rotationMatrix * scaleMatrix * translatioMatrixn_ViewportOffset;

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