using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    public abstract class AbstractComponent
    {
        public string componentTag { get; private set; }
        public string entityTag { get; private set; }

        public AbstractComponent(string componentTag, string entityTag)
        {
            this.componentTag = componentTag;
            this.entityTag = entityTag;
        }
        public abstract void Execute(BaseGameObject gameObject);
    }
}
