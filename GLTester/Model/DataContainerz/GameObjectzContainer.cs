using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class GameObjectzContainer
    {
        private readonly int vaoIndex;

        private readonly List<GameObject> objectN = new List<GameObject>();

        private int currentEntity = -1;



        public void Add(GameObject ToAdd) => objectN.Add(ToAdd);


        public bool CheckVaoIndex(int vaoIndex) => this.vaoIndex == vaoIndex;


        public bool HasNext()
        {
            if (currentEntity < objectN.Count - 1)
            {
                currentEntity++;
                return true;
            }

            currentEntity = -1;
            return false;
        }


        public void MoveStart() => currentEntity = -1;


        public float GetCurrentObjectPositionX() => objectN[currentEntity].GetPositionX();
        public float GetCurrentObjectPositionY() => objectN[currentEntity].GetPositionY();
        public float GetCurrentObjectPositionZ() => objectN[currentEntity].GetPositionZ();

        public float GetCurrentObjectAngleX() => objectN[currentEntity].GetAngleX();
        public float GetCurrentObjectAngleY() => objectN[currentEntity].GetAngleY();
        public float GetCurrentObjectAngleZ() => objectN[currentEntity].GetAngleZ();

        public int GetCurrentObjectTextureIndex() => objectN[currentEntity].GetTextureIndex();

        public float GetCurrentObjectShineDamper() => objectN[currentEntity].GetShineDamper();
        public float GetCurrentObjectReflectivity() => objectN[currentEntity].GettReflectivity();

        public void PointObject(float X, float Y) => objectN[currentEntity].PointObject(X, Y);


        public float GetDebugInfo() => objectN[0].GetAngleY();


        public GameObjectzContainer(int vaoIndex)
        {
            this.vaoIndex = vaoIndex;
        }



    }
}
