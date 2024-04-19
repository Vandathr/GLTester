using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class GameObject: IMoveable
    {
        private readonly MovableObject Move;

        PhysicInternalzContainer ContainerOfPhysicInternalz = new PhysicInternalzContainer(0, 0);

        private readonly int vaoIndex;
        private readonly int textureIndex;

        public int GetVaoIndex() => vaoIndex;
        public int GetTextureIndex() => textureIndex;

        public float GetShineDamper() => ContainerOfPhysicInternalz.GetShineDamper();
        public float GettReflectivity() => ContainerOfPhysicInternalz.GettReflectivity();


        public bool CheckVaoIndex(int vaoIndex) => this.vaoIndex == vaoIndex; 

        public float GetPositionX() => Move.GetPositionX();
        public float GetPositionY() => Move.GetPositionY();
        public float GetPositionZ() => Move.GetPositionZ();

        public float GetAngleX() => Move.GetAngleX();
        public float GetAngleY() => Move.GetAngleY();
        public float GetAngleZ() => Move.GetAngleZ();

        public void IncrementPositionX(float increment) => Move.IncrementPositionX(increment);
        public void IncrementPositionY(float increment) => Move.IncrementPositionY(increment);
        public void IncrementPositionZ(float increment) => Move.IncrementPositionZ(increment);

        public float SetPositionX(float value) => Move.SetPositionX(value);
        public float SetPositionY(float value) => Move.SetPositionY(value);
        public float SetPositionZ(float value) => Move.SetPositionZ(value);

        public void IncrementAngleX(float increment) => Move.IncrementAngleX(increment);
        public void IncrementAngleY(float increment) => Move.IncrementAngleY(increment);
        public void IncrementAngleZ(float increment) => Move.IncrementAngleZ(increment);

        public void MoveForward() => Move.MoveForward();
        public void MoveLeft() => Move.MoveLeft();
        public void MoveRight() => Move.MoveRight();
        public void MoveBackward() => Move.MoveBackward();
        public void MoveUp() => Move.MoveUp();
        public void MoveDown() => Move.MoveDown();

        public int CompareVaoz(GameObject ToCompare) => ToCompare.vaoIndex - vaoIndex;

        public float SetAngleX(float value) => Move.SetAngleX(value);
        public float SetAngleY(float value) => Move.SetAngleY(value);
        public float SetAngleZ(float value) => Move.SetAngleZ(value);


        public void PointObject(float X, float Y) =>  Move.SetAngleY(90 + (float)Math.Atan2(Y + Move.GetPositionY(), X + Move.GetPositionX()) * MovableObject.degreeKoefficient);
        



        public GameObject(int vaoIndex, int textureIndex, MovableObject Move, PhysicInternalzContainer ContainerOfPhysicInternalz)
        {
            this.Move = Move;
            this.vaoIndex = vaoIndex;
            this.textureIndex = textureIndex;
            this.ContainerOfPhysicInternalz = ContainerOfPhysicInternalz;
        }








    }
}
