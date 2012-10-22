using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace MCModeller.Minecraft.Rendering
{
    public class GLAllocation
    {
        private static readonly Dictionary<uint, int> displayListMap = new Dictionary<uint, int>();
        private static readonly List<int> field_74530_b = new List<int>();
        private static readonly OpenGL GL11 = MainForm.GL;

        private static object padlock = new object();
        /**
         * Generates the specified number of display lists and returns the first index.
         */
        public static uint generateDisplayLists(int par0)
        {
            lock (padlock) // Thread safe lock
            {
                uint var1 = GL11.GenLists(par0);
                displayListMap.Add(var1, par0);
                return var1;
            }
        }
    }
}
