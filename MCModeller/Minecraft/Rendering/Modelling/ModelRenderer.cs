using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace MCModeller.Minecraft.Rendering.Modelling
{
    public class ModelRenderer
    {

        public static OpenGL GL11 = MainForm.GL;
           /** The size of the texture file's width in pixels. */
    public float textureWidth;

    /** The size of the texture file's height in pixels. */
    public float textureHeight;

    /** The X offset into the texture used for displaying this model */
    private int textureOffsetX;

    /** The Y offset into the texture used for displaying this model */
    private int textureOffsetY;
    public float rotationPointX;
    public float rotationPointY;
    public float rotationPointZ;
    public float rotateAngleX;
    public float rotateAngleY;
    public float rotateAngleZ;
    private bool compiled;

    /** The GL display list rendered by the Tessellator for this model */
    private uint displayList;
    public bool mirror;
    public bool showModel;

    /** Hides the model. */
    public bool isHidden;
    public List<ModelBox> cubeList;
    public List<ModelRenderer> childModels;
    public readonly String boxName;
    private ModelBase baseModel;

    public ModelRenderer(ModelBase par1ModelBase, String par2Str)
    {
        this.textureWidth = 64.0F;
        this.textureHeight = 32.0F;
        this.compiled = false;
        this.displayList = 0;
        this.mirror = false;
        this.showModel = true;
        this.isHidden = false;
        this.cubeList = new List<ModelBox>();
        this.baseModel = par1ModelBase;
        par1ModelBase.boxList.Add(this);
        this.boxName = par2Str;
        this.setTextureSize(par1ModelBase.textureWidth, par1ModelBase.textureHeight);
    }

    public ModelRenderer(ModelBase par1ModelBase)
    
        :this(par1ModelBase, (String)null){
    }

    public ModelRenderer(ModelBase par1ModelBase, int par2, int par3)
    
        :this(par1ModelBase){
        this.setTextureOffset(par2, par3);
    }

    /**
     * Sets the current box's rotation points and rotation angles to another box.
     */
    public void addChild(ModelRenderer par1ModelRenderer)
    {
        if (this.childModels == null)
        {
            this.childModels = new List<ModelRenderer>();
        }

        this.childModels.Add(par1ModelRenderer);
    }

    public ModelRenderer setTextureOffset(int par1, int par2)
    {
        this.textureOffsetX = par1;
        this.textureOffsetY = par2;
        return this;
    }

    public ModelRenderer addBox(String par1Str, float par2, float par3, float par4, int par5, int par6, int par7)
    {
        par1Str = this.boxName + "." + par1Str;
        TextureOffset var8 = this.baseModel.getTextureOffset(par1Str);
        this.setTextureOffset(var8.textureOffsetX, var8.textureOffsetY);
        this.cubeList.Add((new ModelBox(this, this.textureOffsetX, this.textureOffsetY, par2, par3, par4, par5, par6, par7, 0.0F)).SetName(par1Str));
        return this;
    }

    public ModelRenderer addBox(float par1, float par2, float par3, int par4, int par5, int par6)
    {
        this.cubeList.Add(new ModelBox(this, this.textureOffsetX, this.textureOffsetY, par1, par2, par3, par4, par5, par6, 0.0F));
        return this;
    }

    /**
     * Creates a textured box. Args: originX, originY, originZ, width, height, depth, scaleFactor.
     */
    public void addBox(float par1, float par2, float par3, int par4, int par5, int par6, float par7)
    {
        this.cubeList.Add(new ModelBox(this, this.textureOffsetX, this.textureOffsetY, par1, par2, par3, par4, par5, par6, par7));
    }

    public void setRotationPoint(float par1, float par2, float par3)
    {
        this.rotationPointX = par1;
        this.rotationPointY = par2;
        this.rotationPointZ = par3;
    }
    
    public void render(float par1)
    {
        if (!this.isHidden)
        {
            if (this.showModel)
            {
                if (!this.compiled)
                {
                    this.compileDisplayList(par1);
                }


                if (this.rotateAngleX == 0.0F && this.rotateAngleY == 0.0F && this.rotateAngleZ == 0.0F)
                {
                    if (this.rotationPointX == 0.0F && this.rotationPointY == 0.0F && this.rotationPointZ == 0.0F)
                    {
                        GL11.CallList(this.displayList);

                        if (this.childModels != null)
                        {
                            foreach(ModelRenderer var3 in this.childModels){
                                var3.render(par1);
                            }
                        }
                    }
                    else
                    {
                        GL11.Translate(this.rotationPointX * par1, this.rotationPointY * par1, this.rotationPointZ * par1);
                        GL11.CallList(this.displayList);

                        if (this.childModels != null)
                        {
                            foreach (ModelRenderer var3 in this.childModels)
                            {
                                var3.render(par1);
                            }
                        }

                        GL11.Translate(-this.rotationPointX * par1, -this.rotationPointY * par1, -this.rotationPointZ * par1);
                    }
                }
                else
                {
                    GL11.PushMatrix();
                    GL11.Translate(this.rotationPointX * par1, this.rotationPointY * par1, this.rotationPointZ * par1);

                    if (this.rotateAngleZ != 0.0F)
                    {
                        GL11.Rotate(this.rotateAngleZ * (180F / (float)Math.PI), 0.0F, 0.0F, 1.0F);
                    }

                    if (this.rotateAngleY != 0.0F)
                    {
                        GL11.Rotate(this.rotateAngleY * (180F / (float)Math.PI), 0.0F, 1.0F, 0.0F);
                    }

                    if (this.rotateAngleX != 0.0F)
                    {
                        GL11.Rotate(this.rotateAngleX * (180F / (float)Math.PI), 1.0F, 0.0F, 0.0F);
                    }

                    GL11.CallList(this.displayList);

                    if (this.childModels != null)
                    {
                        foreach(ModelRenderer var3 in this.childModels){
                            var3.render(par1);
                        }
                    }

                    GL11.PopMatrix();
                }
            }
        }
    }
    public void renderWithRotation(float par1)
    {
        if (!this.isHidden)
        {
            if (this.showModel)
            {
                if (!this.compiled)
                {
                    this.compileDisplayList(par1);
                }

                GL11.PushMatrix();
                GL11.Translate(this.rotationPointX * par1, this.rotationPointY * par1, this.rotationPointZ * par1);

                if (this.rotateAngleY != 0.0F)
                {
                    GL11.Rotate(this.rotateAngleY * (180F / (float)Math.PI), 0.0F, 1.0F, 0.0F);
                }

                if (this.rotateAngleX != 0.0F)
                {
                    GL11.Rotate(this.rotateAngleX * (180F / (float)Math.PI), 1.0F, 0.0F, 0.0F);
                }

                if (this.rotateAngleZ != 0.0F)
                {
                    GL11.Rotate(this.rotateAngleZ * (180F / (float)Math.PI), 0.0F, 0.0F, 1.0F);
                }

                GL11.CallList(this.displayList);
                GL11.PopMatrix();
            }
        }
    }

    /**
     * Allows the changing of Angles after a box has been rendered
     */
    public void postRender(float par1)
    {
        if (!this.isHidden)
        {
            if (this.showModel)
            {
                if (!this.compiled)
                {
                    this.compileDisplayList(par1);
                }

                if (this.rotateAngleX == 0.0F && this.rotateAngleY == 0.0F && this.rotateAngleZ == 0.0F)
                {
                    if (this.rotationPointX != 0.0F || this.rotationPointY != 0.0F || this.rotationPointZ != 0.0F)
                    {
                        GL11.Translate(this.rotationPointX * par1, this.rotationPointY * par1, this.rotationPointZ * par1);
                    }
                }
                else
                {
                    GL11.Translate(this.rotationPointX * par1, this.rotationPointY * par1, this.rotationPointZ * par1);

                    if (this.rotateAngleZ != 0.0F)
                    {
                        GL11.Rotate(this.rotateAngleZ * (180F / (float)Math.PI), 0.0F, 0.0F, 1.0F);
                    }

                    if (this.rotateAngleY != 0.0F)
                    {
                        GL11.Rotate(this.rotateAngleY * (180F / (float)Math.PI), 0.0F, 1.0F, 0.0F);
                    }

                    if (this.rotateAngleX != 0.0F)
                    {
                        GL11.Rotate(this.rotateAngleX * (180F / (float)Math.PI), 1.0F, 0.0F, 0.0F);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Compiles a GL display list for this model
    /// </summary>
    /// <param name="scale">Multiplier for the model size</param>
    private void compileDisplayList(float scale)
    {
        this.displayList = GLAllocation.generateDisplayLists(1);
        GL11.NewList(this.displayList, OpenGL.GL_COMPILE);
        Tessellator tessellator = Tessellator.Instance;

        foreach(ModelBox box in this.cubeList){
            box.render(tessellator, scale);
        }

        GL11.EndList();
        this.compiled = true;
    }

    /**
     * Returns the model renderer with the new texture parameters.
     */
    public ModelRenderer setTextureSize(int par1, int par2)
    {
        this.textureWidth = (float)par1;
        this.textureHeight = (float)par2;
        return this;
    }
    }
}
