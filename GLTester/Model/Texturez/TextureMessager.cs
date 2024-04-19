using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class TextureMessager
    {
        private readonly float[] pixelN;
        private readonly string name;

        private readonly int width;
        private readonly int height;

        

        public int GetWidth() => width;
        public int GetHeight() => height;
        public int GetLength() => pixelN.Length;
        public float[] GetPixelz() => pixelN;
        public string GetName() => name; 

        public TextureMessager(Image ToConvert, string path)
        {
            var TextureBitmap = new Bitmap(ToConvert);

            name = Path.GetFileNameWithoutExtension(path);

            width = TextureBitmap.Width;
            height = TextureBitmap.Height;

            pixelN = new float[TextureBitmap.Height * TextureBitmap.Width * 4];


            for (int i = 0; i < TextureBitmap.Height; i++)
                for (int j = 0; j < TextureBitmap.Width; j++)
                {
                    System.Drawing.Color Pixel = TextureBitmap.GetPixel(j, i);

                    pixelN[TextureBitmap.Width * 4 * i + j * 4] = Pixel.R / 255.0f;
                    pixelN[TextureBitmap.Width * 4 * i + j * 4 + 1] = Pixel.G / 255.0f;
                    pixelN[TextureBitmap.Width * 4 * i + j * 4 + 2] = Pixel.B / 255.0f;
                    pixelN[TextureBitmap.Width * 4 * i + j * 4 + 3] = Pixel.A / 255.0f;
                }

        }


        public float this[int index] => pixelN[index];


    }
}
