using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileEngine
{
    [DebuggerDisplay("{name}")]
    public class World
    {
        public string name { get; protected set; }
        public List<Zone> Zone_List { get; set; }
        public int PointerCurrent_Zone { get; set; }
        public int Counter_Zones { get { return Zone_List.Count; } }

        static World()
        {

        }
        public World(string name)
        {
            this.name = name;
            Zone_List = new List<Zone>();
            PointerCurrent_Zone = 0;
        }

        // XNA Methods
        public virtual void Update(GameTime gameTime)
        {
            try
            {
                // Update the current World
                Zone_List[PointerCurrent_Zone].Update(gameTime);
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        public virtual void Draw()
        {
            try
            {
                // Draw the current World
                Zone_List[PointerCurrent_Zone].Draw();
            }
            catch (Exception error)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Console.WriteLine(string.Format("An Error has occured in {0}.{1}, the Error message is: {2}", ToString(), methodName, error.Message));
            }
        }
        // Methods
        public Zone GetCurrentZone()
        {
            return Zone_List[PointerCurrent_Zone];
        }
        public void AddZoneToWorld(Zone newZone)
        {
            Zone_List.Add(newZone);
        }
    }
}
