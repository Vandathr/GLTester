using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class TextureLoader
    {

        public TextureMessager[] LoadTexturez(string[] path)
        {
            var toReturn = new TextureMessager[path.Length];

            for (int i = 0; i < path.Length; i++)
            {
                toReturn[i] = new TextureMessager(Image.FromFile(path[i]), path[i]);
            }

            return toReturn;
        }


    }
}
