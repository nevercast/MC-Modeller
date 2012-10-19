using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering
{
    public class RendererUtil
    {
        public static byte[] UnpackStructure(object structure)
        {
            byte[] Result;

            int Size = Marshal.SizeOf(structure);
            IntPtr Ptr = Marshal.AllocHGlobal(Size);
            try
            {
                Marshal.StructureToPtr(structure, Ptr, false);
                Result = new byte[Size];
                Marshal.Copy(Ptr, Result, 0, Size);
            }
            finally { Marshal.FreeHGlobal(Ptr); }
            return Result;
        }
    }
}
