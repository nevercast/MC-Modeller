using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class ByteBuffer : Stream, IOBuffer<byte>
    {
        private int size;
        private MemoryStream stream;

        private IntBuffer _intWrapper = null;
        private FloatBuffer _floatWrapper = null;
        private ShortBuffer _shortWrapper = null;


        internal BinaryWriter Writer { get; private set; }
        internal BinaryReader Reader { get; private set; }

        public ByteBuffer(int size)
        {
            Reset(size);
            Writer = new BinaryWriter(this);
            Reader = new BinaryReader(this);
        }

        protected void Reset(int size)
        {
            this.size = size;
            if (this.stream != null)
                this.stream.Close();
            this.stream = new MemoryStream(size);
        }

        public static ByteBuffer Allocate(int size)
        {
            return new ByteBuffer(size);
        }

        public IntBuffer IntBuffer
        {
            get
            {
                if (_intWrapper == null)
                    _intWrapper = new IntBuffer(this);
                return _intWrapper;
            }
        }

        public ShortBuffer ShortBuffer
        {
            get
            {
                if (_shortWrapper == null)
                    _shortWrapper = new ShortBuffer(this);
                return _shortWrapper;
            }
        }

        public FloatBuffer FloatBuffer
        {
            get
            {
                if (_floatWrapper == null)
                    _floatWrapper = new FloatBuffer(this);
                return _floatWrapper;
            }
        }

        public void Clear()
        {
            var position = Position;
            Reset(size);
            Position = position;

        }

        public void Put(byte[] data, int offset, int count)
        {
            stream.Write(data, offset, count);
        }

        public int Position
        {
            get
            {
                return (int)stream.Position;
            }
            set
            {
                if (value > Limit)
                    stream.Position = Limit;
                else
                    stream.Position = value;
            }
        }

        public int Limit
        {
            get;
            set;
        }

        public override bool CanRead
        {
            get { return stream.CanRead;  }
        }

        public override bool CanSeek
        {
            get { return stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return stream.CanWrite}
        }

        public override void Flush()
        {
            stream.Flush();
        }

        public override long Length
        {
            get { return stream.Length; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (Position + count > Limit)
            {
                count = Limit - Position;
            }
            Put(buffer, offset, count);
        }
    }
}
