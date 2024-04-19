using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class ObjectSettingzMessager
    {
        private readonly string[] dataN;



        public string GetUsingCommand() => dataN[0];
        public string GetEntityType() => dataN[1];
        public string GetAmountOfObjectz() => dataN[2];
        public string GetPositionX() => dataN[3];
        public string GetPositionY() => dataN[4];
        public string GetPositionZ() => dataN[5];
        public string GetAngleX() => dataN[6];
        public string GetAngleY() => dataN[7];
        public string GetAngleZ() => dataN[8];
        public string GetObjectFileName() => dataN[9];
        public string GetTextureFileName() => dataN[10];



        public ObjectSettingzMessager(string[] dataN)
        {
            this.dataN = dataN;
        }


        public ObjectSettingzMessager(Dictionary<string, string> dataK)
        {
            this.dataN = new string[dataK.Keys.Count];

            dataN[0] = dataK["usingCommand"];
            dataN[1] = dataK["entityType"];
            dataN[2] = dataK["amount"];
            dataN[3] = dataK["X"];
            dataN[4] = dataK["Y"];
            dataN[5] = dataK["Z"];
            dataN[6] = dataK["AX"];
            dataN[7] = dataK["AY"];
            dataN[8] = dataK["AZ"];
            dataN[9] = dataK["objectName"];
            dataN[10] = dataK["textureName"];

        }


    }
}
