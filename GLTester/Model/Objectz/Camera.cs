using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class Camera
    {
        private readonly IMoveable Move;

        private int inversionX = -1;


        public void SwitchInversionX() => inversionX = -inversionX;

        public float GetPositionX() => Move.GetPositionX();
        public float GetPositionY() => Move.GetPositionY();
        public float GetPositionZ() => Move.GetPositionZ();

        public float GetAngleX() => Move.GetAngleX();
        public float GetAngleY() => Move.GetAngleY();
        public float GetAngleZ() => Move.GetAngleZ();


        public void IncrementPositionX(float increment) => Move.IncrementPositionX(increment);
        public void IncrementPositionY(float increment) => Move.IncrementPositionY(increment);
        public void IncrementPositionZ(float increment) => Move.IncrementPositionZ(increment);

        public float SetCameraPositionX(float value) => Move.SetPositionX(value);
        public float SetCameraPositionY(float value) => Move.SetPositionY(value);
        public float SetCameraPositionZ(float value) => Move.SetPositionZ(value);


        public void IncrementAngleX(float increment) => Move.IncrementAngleX(increment * inversionX);
        public void IncrementAngleY(float increment) => Move.IncrementAngleY(increment);
        public void IncrementAngleZ(float increment) => Move.IncrementAngleZ(increment);



        public void MoveForward() => Move.MoveForward();
        public void MoveLeft() => Move.MoveLeft();
        public void MoveRight() => Move.MoveRight();
        public void MoveBackward() => Move.MoveBackward();
        public void MoveUp() => Move.MoveUp();
        public void MoveDown() => Move.MoveDown();


        public Camera()
        {
            Move = new MovableObject(0, 2, 2, -90, 0, 0, 0.1f, 0.1f);



        }



    }
}
