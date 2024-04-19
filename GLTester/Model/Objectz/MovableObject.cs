using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class MovableObject:IMoveable
    {
        private float positionX;
        private float positionY;
        private float positionZ;

        private float angleX;
        private float angleY;
        private float angleZ;

        private readonly float moveSpeed = 0.1f;
        private readonly float rotateSpeed = 0.1f;

        public static readonly float radianKoefficient = (float)(Math.PI / 180.0f);
        public static readonly float degreeKoefficient = (float)(180.0f / Math.PI);

        public float GetPositionX() => positionX;
        public float GetPositionY() => positionY;
        public float GetPositionZ() => positionZ;

        public float GetAngleX() => angleX;
        public float GetAngleY() => angleY;
        public float GetAngleZ() => angleZ;


        public void IncrementPositionX(float increment) => positionX += increment;
        public void IncrementPositionY(float increment) => positionY += increment;
        public void IncrementPositionZ(float increment) => positionZ += increment;

        public float SetPositionX(float value) => positionX = -value;
        public float SetPositionY(float value) => positionY = -value;
        public float SetPositionZ(float value) => positionZ = -value;


        public void IncrementAngleX(float increment) => angleX = NormalizeAngleX(angleX + increment * rotateSpeed);       
        public void IncrementAngleY(float increment) => angleY = NormalizeAngle(angleY + increment * rotateSpeed);
        public void IncrementAngleZ(float increment) => angleZ = NormalizeAngle(angleZ + increment * -rotateSpeed);


        public float SetAngleX(float value) => angleX = NormalizeAngle(value);
        public float SetAngleY(float value) => angleY = NormalizeAngle(value);
        public float SetAngleZ(float value) => angleZ = NormalizeAngle(value);



        public void MoveForward()
        {
            positionX += (float)Math.Sin(angleZ * radianKoefficient) * moveSpeed;
            positionY += (float)Math.Cos(angleZ * radianKoefficient) * moveSpeed;
        }

        public void MoveLeft()
        {
            positionX += (float)Math.Sin(NormalizeAngle(angleZ + 90) * radianKoefficient) * moveSpeed;
            positionY += (float)Math.Cos(NormalizeAngle(angleZ + 90) * radianKoefficient) * moveSpeed;
        }

        public void MoveRight()
        {
            positionX += (float)Math.Sin(NormalizeAngle(angleZ - 90) * radianKoefficient) * moveSpeed;
            positionY += (float)Math.Cos(NormalizeAngle(angleZ - 90) * radianKoefficient) * moveSpeed;
        }


        public void MoveBackward()
        {
            positionX += (float)Math.Sin(angleZ * radianKoefficient) * -moveSpeed;
            positionY += (float)Math.Cos(angleZ * radianKoefficient) * -moveSpeed;
        }


        public void MoveUp() => positionZ += moveSpeed;
        public void MoveDown() => positionZ -= moveSpeed;





        public MovableObject(float positionX, float positionY, float positionZ, float angleX, float angleY, float angleZ, float moveSpeed, float rotateSpeed)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.positionZ = positionZ;
            this.angleX = angleX;
            this.angleY = angleY;
            this.angleZ = angleZ;
            this.moveSpeed = moveSpeed;
            this.rotateSpeed = rotateSpeed;
        }




        private static float NormalizeAngle(float angle)
        {
            if (angle < 0) return angle += 360;
            if (angle > 360) return angle -= 360;

            return angle;
        }

        private static float NormalizeAngleX(float angle)
        {
            if (angle > 0) return 0;
            if (angle < -160) return -160;

            return angle;
        }


    }
}
