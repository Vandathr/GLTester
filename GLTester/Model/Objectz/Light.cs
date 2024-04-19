using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class Light: IMoveable
    {
        private readonly MovableObject Move;
        private Color LightColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        private float ambientLight;



        public float GetAmbientLight() => ambientLight;

        public float GetLightColorR() => LightColor.GetR();
        public float GetLightColorG() => LightColor.GetR();
        public float GetLightColorB() => LightColor.GetR();
        public float GetLightColorA() => LightColor.GetR();



        public float GetAngleX() => Move.GetAngleX();
        public float GetAngleY() => Move.GetAngleY();
        public float GetAngleZ() => Move.GetAngleZ();

        public float GetPositionX() => Move.GetPositionX();
        public float GetPositionY() => Move.GetPositionY();
        public float GetPositionZ() => Move.GetPositionZ();

        public void IncrementAngleX(float increment) => Move.IncrementAngleX(increment);
        public void IncrementAngleY(float increment) => Move.IncrementAngleY(increment);
        public void IncrementAngleZ(float increment) => Move.IncrementAngleZ(increment);

        public void IncrementPositionX(float increment) => Move.IncrementPositionX(increment);
        public void IncrementPositionY(float increment) => Move.IncrementPositionY(increment);
        public void IncrementPositionZ(float increment) => IncrementPositionZ(increment);


        public void MoveBackward() => Move.MoveBackward();
        public void MoveDown() => Move.MoveDown();
        public void MoveForward() => Move.MoveForward();
        public void MoveLeft() => Move.MoveLeft();
        public void MoveRight() => Move.MoveRight();
        public void MoveUp() => Move.MoveUp();


        public void SetAmbientLight(float value) => ambientLight = value;

        public void SetLightColorIntensity(float value) => LightColor = new Color(value, value, value, 1.0f);


        public float SetAngleX(float value) => Move.SetAngleX(value);
        public float SetAngleY(float value) => Move.SetAngleY(value);
        public float SetAngleZ(float value) => Move.SetAngleZ(value);

        public float SetPositionX(float value) => Move.SetPositionX(value);
        public float SetPositionY(float value) => Move.SetPositionY(value);
        public float SetPositionZ(float value) => Move.SetPositionZ(value);



        public Light(float ambientLight, Color LightColor, MovableObject Move)
        {
            this.Move = Move;

            this.ambientLight = ambientLight;

            this.LightColor = LightColor;
        }



    }
}
