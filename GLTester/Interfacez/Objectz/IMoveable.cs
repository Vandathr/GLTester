using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal interface IMoveable
    {
        float GetPositionX();
        float GetPositionY();
        float GetPositionZ();

        float GetAngleX();
        float GetAngleY();
        float GetAngleZ();


        void IncrementPositionX(float increment);
        void IncrementPositionY(float increment);
        void IncrementPositionZ(float increment);

        float SetPositionX(float value);
        float SetPositionY(float value);
        float SetPositionZ(float value);


        void IncrementAngleX(float increment);
        void IncrementAngleY(float increment);
        void IncrementAngleZ(float increment);

        float SetAngleX(float value);
        float SetAngleY(float value);
        float SetAngleZ(float value);


        void MoveForward();
        void MoveLeft();
        void MoveRight();
        void MoveBackward();
        void MoveUp();
        void MoveDown();
    }
}
