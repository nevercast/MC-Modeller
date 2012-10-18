using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class ShortBuffer :IOBuffer<short>
    {
        public ByteBuffer Buffer { get; private set; }
        internal ShortBuffer(ByteBuffer baseBuffer)
        {
            this.Buffer = baseBuffer;
        }

        public void Clear()
        {
            Buffer.Clear();
        }

        public void Put(short[] data, int offset, int count)
        {
            for (var i = offset; i < offset + count; i++)
            {
                Buffer.Writer.Write(data[i]);
            }
        }

        public int Position
        {
            get
            {
                return Buffer.Position;
            }
            set
            {
                Buffer.Position = value;
            }
        }

        public int Limit
        {
            get
            {
                return Buffer.Limit;
            }
            set
            {
                Buffer.Limit = value;
            }
        }
    }
}
