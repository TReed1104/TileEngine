using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public class PositionComponent : AbstractComponent
    {
        public Vector2 position_Base { get; set; }
        public Vector2 position_Grid { get; set; }
        public Vector2 position_Draw { get; set; }

        public PositionComponent(string entityTag, Vector2 position_Base) : base("Position Component", entityTag)
        {
            this.position_Base = position_Base;
            int gridX = (int)(this.position_Base.X / Tile.Dimensions.X);
            int gridY = (int)(this.position_Base.Y / Tile.Dimensions.Y);
            this.position_Grid = new Vector2(gridX, gridY);
            this.position_Draw = this.position_Base + Engine.MainCamera.position_Base;
        }

        public override void Execute(BaseGameObject gameObject)
        {
            if (entityTag != "Player" && entityTag != "Npc" && entityTag == "Boss")
            {
                position_Draw = position_Base + Engine.MainCamera.position_Base;
                int gridX = (int)(position_Base.X / Tile.Dimensions.X);
                int gridY = (int)(position_Base.Y / Tile.Dimensions.Y);
                position_Grid = new Vector2(gridX, gridY);
            }
        }
    }
}
