using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.MathClasses
{
    //TODO: Refactor
    public class Vector3D
    {
        /* Not thread safe, should consider looking in to that */
        private static readonly VectorPool myVectorPool = new VectorPool(300, 300);

        /// <summary>
        /// X Component
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y Component
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Z Component
        /// </summary>
        public double Z { get; set; }

        /**
         * Static method for creating a new Vec3D given the three x,y,z values. This is only called from the other static
         * method which creates and places it in the list.
         */
        public static Vector3D CreateVector(double par0, double par2, double par4)
        {
            return new Vector3D(par0, par2, par4);
        }

        public static VectorPool VectorPool
        {
            get
            {
                return myVectorPool;
            }
        }

        internal Vector3D(double par1, double par3, double par5)
        {
            if (par1 == -0.0D)
            {
                par1 = 0.0D;
            }

            if (par3 == -0.0D)
            {
                par3 = 0.0D;
            }

            if (par5 == -0.0D)
            {
                par5 = 0.0D;
            }

            this.X = par1;
            this.Y = par3;
            this.Z = par5;
        }

        /**
         * Sets the x,y,z components of the vector as specified.
         */
        public Vector3D setComponents(double par1, double par3, double par5)
        {
            this.X = par1;
            this.Y = par3;
            this.Z = par5;
            return this;
        }


        //TODO: Convert Vector math methods to operators

        /**
         * Normalizes the vector to a length of 1 (except if it is the zero vector)
         */
        public Vector3D normalize()
        {
            double var1 = (double)MathHelper.SqrtD(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
            return var1 < 1.0E-4D ? VectorPool().getVecFromPool(0.0D, 0.0D, 0.0D) : VectorPool().getVecFromPool(this.X / var1, this.Y / var1, this.Z / var1);
        }

        public double dotProduct(Vector3D par1Vec3)
        {
            return this.X * par1Vec3.X + this.Y * par1Vec3.Y + this.Z * par1Vec3.Z;
        }

        /**
         * Returns a new vector with the result of the specified vector minus this.
         */
        public Vector3D subtract(Vector3D par1Vec3)
        {
            return VectorPool().getVecFromPool(par1Vec3.X - this.X, par1Vec3.Y - this.Y, par1Vec3.Z - this.Z);
        }
        /**
         * Returns a new vector with the result of this vector x the specified vector.
         */
        public Vector3D crossProduct(Vector3D par1Vec3)
        {
            return VectorPool().getVecFromPool(this.Y * par1Vec3.Z - this.Z * par1Vec3.Y, this.Z * par1Vec3.X - this.X * par1Vec3.Z, this.X * par1Vec3.Y - this.Y * par1Vec3.X);
        }

        /**
         * Adds the specified x,y,z vector components to this vector and returns the resulting vector. Does not change this
         * vector.
         */
        public Vector3D addVector(double par1, double par3, double par5)
        {
            return VectorPool().getVecFromPool(this.X + par1, this.Y + par3, this.Z + par5);
        }

        /**
         * Euclidean distance between this and the specified vector, returned as double.
         */
        public double distanceTo(Vector3D par1Vec3)
        {
            double var2 = par1Vec3.X - this.X;
            double var4 = par1Vec3.Y - this.Y;
            double var6 = par1Vec3.Z - this.Z;
            return (double)MathHelper.SqrtD(var2 * var2 + var4 * var4 + var6 * var6);
        }

        /**
         * The square of the Euclidean distance between this and the specified vector.
         */
        public double squareDistanceTo(Vector3D par1Vec3)
        {
            double var2 = par1Vec3.X - this.X;
            double var4 = par1Vec3.Y - this.Y;
            double var6 = par1Vec3.Z - this.Z;
            return var2 * var2 + var4 * var4 + var6 * var6;
        }

        /**
         * The square of the Euclidean distance between this and the vector of x,y,z components passed in.
         */
        public double squareDistanceTo(double par1, double par3, double par5)
        {
            double var7 = par1 - this.X;
            double var9 = par3 - this.Y;
            double var11 = par5 - this.Z;
            return var7 * var7 + var9 * var9 + var11 * var11;
        }

        /**
         * Returns the length of the vector.
         */
        public double lengthVector()
        {
            return (double)MathHelper.SqrtD(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
        }

        /**
         * Returns a new vector with x value equal to the second parameter, along the line between this vector and the
         * passed in vector, or null if not possible.
         */
        public Vector3D getIntermediateWithXValue(Vector3D par1Vec3, double par2)
        {
            double var4 = par1Vec3.X - this.X;
            double var6 = par1Vec3.Y - this.Y;
            double var8 = par1Vec3.Z - this.Z;

            if (var4 * var4 < 1.0000000116860974E-7D)
            {
                return null;
            }
            else
            {
                double var10 = (par2 - this.X) / var4;
                return var10 >= 0.0D && var10 <= 1.0D ? VectorPool().getVecFromPool(this.X + var4 * var10, this.Y + var6 * var10, this.Z + var8 * var10) : null;
            }
        }

        /**
         * Returns a new vector with y value equal to the second parameter, along the line between this vector and the
         * passed in vector, or null if not possible.
         */
        public Vector3D getIntermediateWithYValue(Vector3D par1Vec3, double par2)
        {
            double var4 = par1Vec3.X - this.X;
            double var6 = par1Vec3.Y - this.Y;
            double var8 = par1Vec3.Z - this.Z;

            if (var6 * var6 < 1.0000000116860974E-7D)
            {
                return null;
            }
            else
            {
                double var10 = (par2 - this.Y) / var6;
                return var10 >= 0.0D && var10 <= 1.0D ? VectorPool().getVecFromPool(this.X + var4 * var10, this.Y + var6 * var10, this.Z + var8 * var10) : null;
            }
        }

        /**
         * Returns a new vector with z value equal to the second parameter, along the line between this vector and the
         * passed in vector, or null if not possible.
         */
        public Vector3D getIntermediateWithZValue(Vector3D par1Vec3, double par2)
        {
            double var4 = par1Vec3.X - this.X;
            double var6 = par1Vec3.Y - this.Y;
            double var8 = par1Vec3.Z - this.Z;

            if (var8 * var8 < 1.0000000116860974E-7D)
            {
                return null;
            }
            else
            {
                double var10 = (par2 - this.Z) / var8;
                return var10 >= 0.0D && var10 <= 1.0D ? VectorPool().getVecFromPool(this.X + var4 * var10, this.Y + var6 * var10, this.Z + var8 * var10) : null;
            }
        }

        public String toString()
        {
            return "(" + this.X + ", " + this.Y + ", " + this.Z + ")";
        }

        /**
         * Rotates the vector around the x axis by the specified angle.
         */
        public void rotateAroundX(float par1)
        {
            float var2 = MathHelper.Cos(par1);
            float var3 = MathHelper.Sin(par1);
            double var4 = this.X;
            double var6 = this.Y * (double)var2 + this.Z * (double)var3;
            double var8 = this.Z * (double)var2 - this.Y * (double)var3;
            this.X = var4;
            this.Y = var6;
            this.Z = var8;
        }

        /**
         * Rotates the vector around the y axis by the specified angle.
         */
        public void rotateAroundY(float par1)
        {
            float var2 = MathHelper.Cos(par1);
            float var3 = MathHelper.Sin(par1);
            double var4 = this.X * (double)var2 + this.Z * (double)var3;
            double var6 = this.Y;
            double var8 = this.Z * (double)var2 - this.X * (double)var3;
            this.X = var4;
            this.Y = var6;
            this.Z = var8;
        }


        /**
         * Rotates the vector around the z axis by the specified angle.
         */
        public void rotateAroundZ(float par1)
        {
            float var2 = MathHelper.Cos(par1);
            float var3 = MathHelper.Sin(par1);
            double var4 = this.X * (double)var2 + this.Y * (double)var3;
            double var6 = this.Y * (double)var2 - this.X * (double)var3;
            double var8 = this.Z;
            this.X = var4;
            this.Y = var6;
            this.Z = var8;
        }
    }
}
