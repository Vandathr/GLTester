using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace GLTester
{
    internal class TextureManager: IDisposable
    {
        private readonly List<int> textureIndexN = new List<int>();

        private int indexOfTexture;


        //public void MakeTexture(int[] pixelN, int textureWidth, int textureHeight)
        //{
        //    GL.CreateTextures(TextureTarget.Texture2D, 1, out indexOfTexture);

        //    GL.TextureStorage2D(indexOfTexture, 1, SizedInternalFormat.Rgba32f, textureWidth, textureHeight);

        //    GL.BindTexture(TextureTarget.Texture2D, indexOfTexture);

        //    GL.TextureSubImage2D(indexOfTexture, 0, 0, 0, textureWidth, textureHeight, PixelFormat.Rgba, PixelType.UnsignedByte, pixelN);

        //    GL.TexImage2D<int>(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textureWidth, textureHeight, 0, PixelFormat.Rgba, PixelType.Float, pixelN);



        //    textureIndexN.Add(indexOfTexture);

        //}


        public int MakeTexture(float[] pixelN, int textureWidth, int textureHeight)
        {
            indexOfTexture = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);

            GL.BindTexture(TextureTarget.Texture2D, indexOfTexture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textureWidth, textureHeight, 0, PixelFormat.Rgba, PixelType.Float, pixelN);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            textureIndexN.Add(indexOfTexture);

            return indexOfTexture;
        }

        public int GetTextureIndex(int index) => textureIndexN[index];


        public void Dispose()
        {
            for (int i = 0; i < textureIndexN.Count; i++)
            {
                GL.DeleteTexture(textureIndexN[i]);
            }
        }


    }
}
