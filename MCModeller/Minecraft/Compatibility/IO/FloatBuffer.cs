using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class FloatBuffer : IOBuffer<float>
    {
        public ByteBuffer Buffer { get; private set; }
        internal FloatBuffer(ByteBuffer baseBuffer)
        {
            this.Buffer = baseBuffer;
        }

        public void Clear()
        {
            Buffer.Clear();
        }

        public void Put(float[] data, int offset, int count)
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

        public static implicit operator float[](FloatBuffer buffer)
        {
            float[] fBuffer = new float[buffer.Limit / 4];
            byte[] data = buffer.Buffer.ToArray();
            for (int i = 0; i < fBuffer.Length; i++)
            {
                fBuffer[i] = BitConverter.ToSingle(data, i * 4);
            }
            return fBuffer;
        }
    }
}
