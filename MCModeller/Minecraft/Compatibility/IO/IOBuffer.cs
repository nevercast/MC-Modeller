using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Compatibility.IO
{
    interface IOBuffer<T>
    {
        /// <summary>
        /// Clears the buffer
        /// </summary>
        void Clear();

        /// <summary>
        /// Write an array in to the buffer
        /// </summary>
        /// <param name="data">Data to write</param>
        /// <param name="offset">Offset in buffer</param>
        /// <param name="count">Amount of data to write</param>
        void Put(T[] data, int offset, int count);

        /// <summary>
        /// Position in buffer
        /// </summary>
        /// <param name="position">position in buffer</param>
        int Position { get; set; }

        /// <summary>
        /// Buffer position limit.
        /// If position is higher than limit it's set to limit
        /// </summary>
        int Limit { get; set; }


    }
}
