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
    public class NpcAgent : BaseAgent
    {
        public NpcAgent(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth, healthPoints)
        {
            MovementController += NpcMovement;
            BehaviourController += NpcBehaviour;
        }

        protected void NpcMovement()
        {

        }
        protected void NpcBehaviour(GameTime gameTime)
        {

        }
    }
}
