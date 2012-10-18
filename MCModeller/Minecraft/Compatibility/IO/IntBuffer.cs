using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class IntBuffer : IOBuffer<int>
    {
        public ByteBuffer Buffer { get; private set; }

        internal IntBuffer(ByteBuffer baseBuffer)
        {
            this.Buffer = baseBuffer;
        }

        public void Clear()
        {
            Buffer.Clear();
        }

        public void Put(int[] data, int offset, int count)
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

        public static implicit operator int[](IntBuffer buffer)
        {
            int[] iBuffer = new int[buffer.Limit / 4];
            byte[] data = buffer.Buffer.ToArray();
            for (int i = 0; i < iBuffer.Length; i++)
            {
                iBuffer[i] = BitConverter.ToInt32(data, i * 4);
            }
            return iBuffer;
        }

    }
}
