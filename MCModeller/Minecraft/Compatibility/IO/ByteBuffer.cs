using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    public class ByteBuffer
    {
        private int size;
        private MemoryStream stream;

        private IntBuffer _intWrapper = null;
        private FloatBuffer _floatWrapper = null;
        private ShortBuffer _shortWrapper = null;

        public ByteBuffer(int size)
        {
            this.size = size;
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
    }
}
