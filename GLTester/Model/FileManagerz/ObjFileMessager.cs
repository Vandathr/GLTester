using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class ObjFileMessager
    {
        private readonly float[] vertexezAndColorzN;
        private readonly uint[] indexN;

        private readonly int blockSize;

        private readonly string objectName;


        public float[] GetVertexezAndColorz() => vertexezAndColorzN;
        public uint[] GetIndexez() => indexN;
        public int GetBlockSize() => blockSize;

        public string GetObjectName() => objectName;


        public ObjFileMessager(float[] vertexezAndColorzN, uint[] indexN, int blockSize, string objectName)
        {
            this.vertexezAndColorzN = vertexezAndColorzN;
            this.indexN = indexN;
            this.blockSize = blockSize;

            this.objectName = objectName;
        }



    }
}
