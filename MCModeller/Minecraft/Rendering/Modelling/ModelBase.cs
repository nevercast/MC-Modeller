﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCModeller.Minecraft.Rendering.Modelling
{
    //TODO: Refactor
    //TODO: Entities
    public abstract class ModelBase
    {
        public float onGround;
        public bool isRiding = false;

        /**
         * This is a list of all the boxes (ModelRenderer.class) in the current model.
         */
        public ArrayList boxList = new ArrayList();
        public bool isChild = true;

        /** A mapping for all texture offsets */
        private Dictionary<String, TextureOffset> modelTextureMap = new Dictionary<String, TextureOffset>();
        public int textureWidth = 64;
        public int textureHeight = 32;

        /**
         * Sets the models various rotation angles then renders the model.
         */
        public void render(object par1Entity, float par2, float par3, float par4, float par5, float par6, float par7) { }

        /**
         * Sets the model's various rotation angles. For bipeds, par1 and par2 are used for animating the movement of arms
         * and legs, where par1 represents the time(so that arms and legs swing back and forth) and par2 represents how
         * "far" arms and legs can swing at most.
         */
        public void setRotationAngles(float par1, float par2, float par3, float par4, float par5, float par6) { }

        /**
         * Used for easily adding entity-dependent animations. The second and third float params here are the same second
         * and third as in the setRotationAngles method.
         */
        public void setLivingAnimations(object par1EntityLiving, float par2, float par3, float par4) { }

        protected void setTextureOffset(String par1Str, int par2, int par3)
        {
            this.modelTextureMap.Add(par1Str, new TextureOffset(par2, par3));
        }

        public TextureOffset getTextureOffset(String par1Str)
        {
            return (TextureOffset)this.modelTextureMap[par1Str];
        }
    }

}
