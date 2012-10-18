using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class FloatBuffer
    {
        public ByteBuffer Buffer { get; private set; }
        internal FloatBuffer(ByteBuffer baseBuffer)
        {
            this.Buffer = baseBuffer;
        }
    }
}
