using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class PrimitiveMessager
    {
        private readonly string objectName;
        private readonly int objectIndex;
        private readonly int[] argumentN;
        
        public string GetObjectName() => objectName;
        public int GetObjectIndex() => objectIndex;
  


        public PrimitiveMessager(string objectName, int objectIndex, string argumentz, char[] divider)
        {
            this.objectName = Path.GetFileNameWithoutExtension(objectName);
            this.objectIndex = objectIndex;

            var argumentStringN = argumentz.Split(divider, StringSplitOptions.RemoveEmptyEntries);

            this.argumentN = new int[argumentStringN.Length];

            for (int i = 0; i < argumentN.Length; i++)
            {
                argumentN[i] = int.Parse(argumentStringN[i]);
            }
        }


        public int this[int index] => argumentN[index];



    }
}
