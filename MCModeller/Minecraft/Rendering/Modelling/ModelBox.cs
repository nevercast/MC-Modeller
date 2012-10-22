﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering.Modelling
{
    public class ModelBox
    {
        /**
  * The (x,y,z) vertex positions and (u,v) texture coordinates for each of the 8 points on a cube
  */
        private PositionTextureVertex[] vertexPositions;

        /** An array of 6 TexturedQuads, one for each face of a cube */
        private TexturedQuad[] quadList;

        /** X vertex coordinate of lower box corner */
        public readonly float posX1;

        /** Y vertex coordinate of lower box corner */
        public readonly float posY1;

        /** Z vertex coordinate of lower box corner */
        public readonly float posZ1;

        /** X vertex coordinate of upper box corner */
        public readonly float posX2;

        /** Y vertex coordinate of upper box corner */
        public readonly float posY2;

        /** Z vertex coordinate of upper box corner */
        public readonly float posZ2;
        public String name;

        public ModelBox(ModelRenderer par1ModelRenderer, int par2, int par3, float par4, float par5, float par6, int par7, int par8, int par9, float par10)
        {
            this.posX1 = par4;
            this.posY1 = par5;
            this.posZ1 = par6;
            this.posX2 = par4 + (float)par7;
            this.posY2 = par5 + (float)par8;
            this.posZ2 = par6 + (float)par9;
            this.vertexPositions = new PositionTextureVertex[8];
            this.quadList = new TexturedQuad[6];
            float var11 = par4 + (float)par7;
            float var12 = par5 + (float)par8;
            float var13 = par6 + (float)par9;
            par4 -= par10;
            par5 -= par10;
            par6 -= par10;
            var11 += par10;
            var12 += par10;
            var13 += par10;

            if (par1ModelRenderer.mirror)
            {
                float var14 = var11;
                var11 = par4;
                par4 = var14;
            }

            PositionTextureVertex var26 = new PositionTextureVertex(par4, par5, par6, 0.0F, 0.0F);
            PositionTextureVertex var15 = new PositionTextureVertex(var11, par5, par6, 0.0F, 8.0F);
            PositionTextureVertex var16 = new PositionTextureVertex(var11, var12, par6, 8.0F, 8.0F);
            PositionTextureVertex var17 = new PositionTextureVertex(par4, var12, par6, 8.0F, 0.0F);
            PositionTextureVertex var18 = new PositionTextureVertex(par4, par5, var13, 0.0F, 0.0F);
            PositionTextureVertex var19 = new PositionTextureVertex(var11, par5, var13, 0.0F, 8.0F);
            PositionTextureVertex var20 = new PositionTextureVertex(var11, var12, var13, 8.0F, 8.0F);
            PositionTextureVertex var21 = new PositionTextureVertex(par4, var12, var13, 8.0F, 0.0F);
            this.vertexPositions[0] = var26;
            this.vertexPositions[1] = var15;
            this.vertexPositions[2] = var16;
            this.vertexPositions[3] = var17;
            this.vertexPositions[4] = var18;
            this.vertexPositions[5] = var19;
            this.vertexPositions[6] = var20;
            this.vertexPositions[7] = var21;
            this.quadList[0] = new TexturedQuad(new PositionTextureVertex[] { var19, var15, var16, var20 }, par2 + par9 + par7, par3 + par9, par2 + par9 + par7 + par9, par3 + par9 + par8, par1ModelRenderer.textureWidth, par1ModelRenderer.textureHeight);
            this.quadList[1] = new TexturedQuad(new PositionTextureVertex[] { var26, var18, var21, var17 }, par2, par3 + par9, par2 + par9, par3 + par9 + par8, par1ModelRenderer.textureWidth, par1ModelRenderer.textureHeight);
            this.quadList[2] = new TexturedQuad(new PositionTextureVertex[] { var19, var18, var26, var15 }, par2 + par9, par3, par2 + par9 + par7, par3 + par9, par1ModelRenderer.textureWidth, par1ModelRenderer.textureHeight);
            this.quadList[3] = new TexturedQuad(new PositionTextureVertex[] { var16, var17, var21, var20 }, par2 + par9 + par7, par3 + par9, par2 + par9 + par7 + par7, par3, par1ModelRenderer.textureWidth, par1ModelRenderer.textureHeight);
            this.quadList[4] = new TexturedQuad(new PositionTextureVertex[] { var15, var26, var17, var16 }, par2 + par9, par3 + par9, par2 + par9 + par7, par3 + par9 + par8, par1ModelRenderer.textureWidth, par1ModelRenderer.textureHeight);
            this.quadList[5] = new TexturedQuad(new PositionTextureVertex[] { var18, var19, var20, var21 }, par2 + par9 + par7 + par9, par3 + par9, par2 + par9 + par7 + par9 + par7, par3 + par9 + par8, par1ModelRenderer.textureWidth, par1ModelRenderer.textureHeight);

            if (par1ModelRenderer.mirror)
            {
                TexturedQuad[] var22 = this.quadList;
                int var23 = var22.Length;

                for (int var24 = 0; var24 < var23; ++var24)
                {
                    TexturedQuad var25 = var22[var24];
                    var25.flipFace();
                }
            }
        }

        /**
         * Draw the six sided box defined by this ModelBox
         */
        public void render(Tessellator par1Tessellator, float par2)
        {
            TexturedQuad[] var3 = this.quadList;
            int var4 = var3.Length;

            for (int var5 = 0; var5 < var4; ++var5)
            {
                TexturedQuad var6 = var3[var5];
                var6.draw(par1Tessellator, par2);
            }
        }

        public ModelBox SetName(String name)
        {
            this.name = name;
            return this;
        }
    }
}
