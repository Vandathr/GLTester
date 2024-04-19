using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class GameObjectzFabric
    {
        private int vaoIndex;
        private int textureIndex;
        private float positionX;
        private float positionY;
        private float positionZ;
        private float angleX;
        private float angleY;
        private float angleZ;
        private float moveSpeed;
        private float rotateSpeed;
        private float shineDamper;
        private float reflectivity;


        public GameObject Make()
        {
            return new GameObject(vaoIndex, textureIndex, new MovableObject(positionX, positionY, positionZ, angleX, angleY, angleZ, moveSpeed, rotateSpeed), 
                new PhysicInternalzContainer(0, 0));
        }

        public void SetVaoIndexez(int vaoIndex, int textureIndex)
        {
            this.vaoIndex = vaoIndex;
            this.textureIndex = textureIndex;
        }


    }
}
