using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.MathClasses
{
    //TODO: Refactor
    public class VectorPool
    {
        private readonly int truncateArrayResetThreshold;
        private readonly int minimumSize;

        /** items at and above nextFreeSpace are assumed to be available */
        private readonly List<Vector3D> vec3Cache = new List<Vector3D>();
        private int nextFreeSpace = 0;
        private int maximumSizeSinceLastTruncation = 0;
        private int resetCount = 0;

        public VectorPool(int par1, int par2)
        {
            this.truncateArrayResetThreshold = par1;
            this.minimumSize = par2;
        }

        /**
         * extends the pool if all vecs are currently "out"
         */
        public Vector3D getVecFromPool(double par1, double par3, double par5)
        {
            Vector3D var7;

            if (this.nextFreeSpace >= this.vec3Cache.Count)
            {
                var7 = new Vector3D(par1, par3, par5);
                this.vec3Cache.Add(var7);
            }
            else
            {
                var7 = (Vector3D)this.vec3Cache[this.nextFreeSpace];
                var7.setComponents(par1, par3, par5);
            }

            ++this.nextFreeSpace;
            return var7;
        }

        /**
         * Will truncate the array everyN clears to the maximum size observed since the last truncation.
         */
        public void clear()
        {
            if (this.nextFreeSpace > this.maximumSizeSinceLastTruncation)
            {
                this.maximumSizeSinceLastTruncation = this.nextFreeSpace;
            }

            if (this.resetCount++ == this.truncateArrayResetThreshold)
            {
                int var1 = Math.Max(this.maximumSizeSinceLastTruncation, this.vec3Cache.Count - this.minimumSize);

                while (this.vec3Cache.Count > var1)
                {
                    this.vec3Cache.RemoveAt(var1);
                }

                this.maximumSizeSinceLastTruncation = 0;
                this.resetCount = 0;
            }

            this.nextFreeSpace = 0;
        }

        public void clearAndFreeCache()
        {
            this.nextFreeSpace = 0;
            this.vec3Cache.Clear();
        }
    }
}
