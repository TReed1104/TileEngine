using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public abstract class BaseAgent : BaseGameObject
    {
        // Enums
        public float healthPoints { get; protected set; }
        public float damagePower { get; protected set; }
        public InputComponent inputComponent { get; set; }

        // Constructors
        static BaseAgent()
        {

        }
        public BaseAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base (tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth)
        {
            inputComponent = new InputComponent(tag, this);
            this.healthPoints = healthPoints;
            damagePower = 1.0f;
        }
    }
}
