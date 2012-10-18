using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class IntBuffer
    {
        public ByteBuffer Buffer { get; private set; }
        internal IntBuffer(ByteBuffer baseBuffer)
        {
            this.Buffer = baseBuffer;
        }
    }
}
