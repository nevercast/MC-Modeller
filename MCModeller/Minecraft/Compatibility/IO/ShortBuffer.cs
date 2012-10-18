using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class ShortBuffer
    {
        public ByteBuffer Buffer { get; private set; }
        internal ShortBuffer(ByteBuffer baseBuffer)
        {
            this.Buffer = baseBuffer;
        }
    }
}
