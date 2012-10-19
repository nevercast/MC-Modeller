using System;
namespace MCModeller.Minecraft.Rendering
{
    interface ITessellator
    {
        int Draw();
        void AddTranslation(float x, float y, float z);
        void AddVertex(double x, double y, double z);
        void AddVertexWithUV(double x, double y, double z, double textureU, double textureV);
        void DisableColor();
        void SetBrightness(int brightness);
        void SetColorOpaque(int r, int g, int b);
        void SetColorOpaque_F(float r, float g, float b);
        void SetColorOpaque_I(int color);
        void SetColorRGBA(int r, int g, int b, int a);
        void SetColorRGBA_F(float r, float g, float b, float a);
        void SetColorRGBA_I(int color, int alpha);
        void SetNormal(float x, float y, float z);
        void SetTextureUV(double textureU, double textureV);
        void SetTranslation(double x, double y, double z);
        void StartDrawing(int mode);
        void StartDrawingQuads();
    }
}
