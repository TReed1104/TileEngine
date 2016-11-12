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
    public class NPC : AbstractEntity
    {
        public enum NPCType { Rookie, Champion, Boss, Critter, }
        public NPCType entityType { get; set; }

        static NPC()
        {

        }
        public NPC(string tag, Texture2D texture, Vector2 position_World, Vector2 sourceRectangle_Position, Vector2 sourceRectangle_Size, Color colour, float layerDepth, float healthPoints)
            : base(tag, texture, position_World, sourceRectangle_Position, sourceRectangle_Size, colour, layerDepth, healthPoints)
        {
            layerDepth = Engine.LayerDepth_Player;
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
        public override void Draw()
        {
            base.Draw();
        }

        protected override void MovementControl()
        {
            try
            {

            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
    }
}
