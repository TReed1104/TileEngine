using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TileEngine
{
    [DebuggerDisplay("{tag}")]
    public abstract class AbstractGameObjectMethods
    {
        // Components
        public PositionComponent positionComponent { get; set; }
        public RendererComponent renderComponent { get; set; }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw();
    }
}
