using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering
{
    public class TextureOffset
    {
        /** The x coordinate offset of the texture */
        public readonly int textureOffsetX;

        /** The y coordinate offset of the texture */
        public readonly int textureOffsetY;

        public TextureOffset(int par1, int par2)
        {
            this.textureOffsetX = par1;
            this.textureOffsetY = par2;
        }
    }
}
