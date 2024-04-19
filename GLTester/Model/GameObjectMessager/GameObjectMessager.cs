using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class GameObjectMessager
    {
        private readonly string name;
        private readonly int Id;
        private readonly int typeId;

        private readonly float[] entityElementN;
        private readonly uint[] indexN;


        public float[] GetEntityElementzArray() => entityElementN;
        public uint[] GetIndexez() => indexN;

        public string GetName() => name;
        public int GetId() => Id;
        public int GetTypeId() => typeId;


        public GameObjectMessager(float[] entityElementN, uint[] indexN)
        {
            this.entityElementN = entityElementN;
            this.indexN = indexN;
        }

    }
}
