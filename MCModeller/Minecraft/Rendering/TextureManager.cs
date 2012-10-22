using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL.SceneGraph.Assets;

namespace MCModeller.Minecraft.Rendering
{
    public class TextureManager
    {
        private static Dictionary<String, Texture> textureMap = new Dictionary<string, Texture>();
        public static void InitTexture(string path, String name)
        {
            if(textureMap.ContainsKey(name)){
                throw new InvalidOperationException("Texture by name " + name + " already exists!");
            }
            var texture = new Texture();
            texture.Create(MainForm.GL, path);
            texture.Name = name;
            textureMap.Add(name, texture);
        }

        public static void BindTexture(String name)
        {
            if (!textureMap.ContainsKey(name))
            {
                throw new InvalidOperationException("Texture by name " + name + " does not exist, cannot bind!");
            }
            textureMap[name].Bind(MainForm.GL);
        }
    }
}
