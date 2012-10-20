using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering
{
    public class NativeBuffer : IDisposable
    {
        /// <summary>
        /// Totally 100% NOT Thread Safe, don't use this in different threads, it'll hurt
        /// </summary>
        private unsafe static FastDataConverter* FDC;

        public IntPtr Pointer;
        public int Size { get; private set; }
        public int Index { get; set; }

        public NativeBuffer(int count)
        {
            Size = count;
            Index = 0;
            Pointer = Marshal.AllocHGlobal(Size);
        }

        public void Dispose()
        {
            if (Pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Pointer);
                Pointer = IntPtr.Zero;
            }
        }

        public void Expand(int newSize)
        {
            if (newSize <= Size) return;
            /* Create managed buffer */
            byte[] buffer = new byte[Size];
            /* Marshal data over */
            Marshal.Copy(Pointer, buffer, 0, Size);
            /* Release the native handle */
            Marshal.FreeHGlobal(Pointer);
            /* Set new size */
            Size = newSize;
            /* Reallocate native handle */
            Pointer = Marshal.AllocHGlobal(Size);
            /* Copy data in to new allocation */
            Marshal.Copy(buffer, 0, Pointer, buffer.Length);
        }

        public void AppendInteger(int value)
        {
            Integer = value;
            Index++;
        }

        public void AppendFloat(float value)
        {
            Float = value;
            Index++;
        }

        public int Integer
        {
            get {
                ConstrainIndex();
                unsafe /* Evil wizardry here! */
                {
                    /* Direct the structure to the memory location */
                    FDC = (FastDataConverter*)(Pointer + Index);
                    return (*FDC).IntValue;
                }
            }

            set {
                ConstrainIndex();
                unsafe
                {
                    FDC = (FastDataConverter*)(Pointer + Index);
                    (*FDC).IntValue = value;
                }
            }
        }

        public float Float
        {
            get
            {
                ConstrainIndex();
                unsafe /* Evil wizardry here! */
                {
                    /* Direct the structure to the memory location */
                    FDC = (FastDataConverter*)(Pointer + Index);
                    return (*FDC).FloatValue;
                }
            }

            set
            {
                ConstrainIndex();
                unsafe
                {
                    FDC = (FastDataConverter*)(Pointer + Index);
                    (*FDC).FloatValue = value;
                }
            }
        }

        private void ConstrainIndex()
        {
            if (Index >= Size)
                Index = Size - 1;
            if (Index < 0)
                Index = 0;
        }

    }
}
