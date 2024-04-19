using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class GameSettingzKeeper
    {
        private char elementSeparator = ',';
        private readonly string fileName = "Settings";

        private readonly int screenWidth = 1200;
        private readonly int screenHeight = 900;
        private readonly int graphicsMode = 0;
        private readonly string windowTitle = "window title";
        private readonly int gameWindowFlagsIndex = 0;
        private readonly int displayDeviceIndex = 0;
        private readonly int manorOpenGLVersion = 4;
        private readonly int minorOpenGLVersion = 5;
        private readonly int contextFlagIndex = 0;
        private readonly int updateRate = 40;
        private readonly int frontPolygonModeIndex = 1;
        private readonly int backPolygonModeIndex = 0;


        private readonly int[] orthoSettingN = new int[] { 2, 2, -2, 2, -1, 1 };
        private readonly int[] frustumSettingN = new int[] { -1, 1, -1, 1, 2, 16 };
        private readonly float[] perspectiveFieldOfViewSettingN = new float[] { 80.0f, 0.5f, 10.0f };

        public int GetScreenWidth() => screenWidth;
        public int GetScreenHeight() => screenHeight;
        public int GetGraphicsModeIndex() => graphicsMode;
        public string GetWindowTitle() => windowTitle;
        public int GetGameWindowFlagsIndex() => gameWindowFlagsIndex;
        public int GetDisplayDeviceIndex() => displayDeviceIndex;
        public int GetManorOpenGLVersion() => manorOpenGLVersion;
        public int GetMinorOpenGLVersion() => minorOpenGLVersion;
        public int GetContextFlagIndex() => contextFlagIndex;
        public int GetUpdateRate() => updateRate;
        public int GetFrontPolygonModeIndex() => frontPolygonModeIndex;
        public int GetBackPolygonModeIndex() => backPolygonModeIndex;


        public int[] GetOrthoSettingz() => orthoSettingN;
        public int[] GetFrustumSettingN() => frustumSettingN;
        public float[] GetPerspectiveFieldOfViewSettingN() => perspectiveFieldOfViewSettingN;


        public GameSettingzKeeper(Dictionary<string, string> sourceK)
        {
            int rowIndex = 0;

            this.screenWidth = ParseToInt(sourceK["screenWidth"], 400, 2000, fileName, ++rowIndex);
            this.screenHeight = ParseToInt(sourceK["screenHeight"], 400, 2000, fileName, ++rowIndex);
            this.graphicsMode = ParseToInt(sourceK["graphicsMode"], 0, 10, fileName, ++rowIndex);
            this.windowTitle = sourceK["windowTitle"];
            this.gameWindowFlagsIndex = ParseToInt(sourceK["gameWindowFlagsIndex"], 0, 10, fileName, ++rowIndex);
            this.displayDeviceIndex = ParseToInt(sourceK["displayDeviceIndex"], 0, 100, fileName, ++rowIndex);
            this.manorOpenGLVersion = ParseToInt(sourceK["manorOpenGLVersion"], 0, 100, fileName, ++rowIndex);
            this.minorOpenGLVersion = ParseToInt(sourceK["minorOpenGLVersion"], 0, 100, fileName, ++rowIndex);
            this.contextFlagIndex = ParseToInt(sourceK["contextFlagIndex"], 0, 10, fileName, ++rowIndex);
            this.updateRate = ParseToInt(sourceK["updateRate"], 0, 100, fileName, ++rowIndex);
            
            rowIndex++;

            var tempN = sourceK["orthoSettingz"].Split(elementSeparator);

            orthoSettingN = new int[tempN.Length];
            
            for(int i = 0; i < orthoSettingN.Length; i++)
            {
                orthoSettingN[i] = ParseToInt(tempN[i], -100, 100, fileName, rowIndex);
            }


            tempN = sourceK["frustumSettingz"].Split(elementSeparator);

            frustumSettingN = new int[tempN.Length];

            for (int i = 0; i < frustumSettingN.Length; i++)
            {
                frustumSettingN[i] = ParseToInt(tempN[i], -100, 500, fileName, rowIndex);
            }

            tempN = sourceK["perspectiveFieldOfViewSrttingz"].Split(elementSeparator);

            perspectiveFieldOfViewSettingN = new float[tempN.Length];

            for (int i = 0; i < perspectiveFieldOfViewSettingN.Length; i++)
            {
                perspectiveFieldOfViewSettingN[i] = float.Parse(tempN[i], CultureInfo.InvariantCulture);
            }

            perspectiveFieldOfViewSettingN[0] = perspectiveFieldOfViewSettingN[0] * (float)Math.PI / 180.0f;

            this.frontPolygonModeIndex = ParseToInt(sourceK["frontPolygonMode"], 0, 10, fileName, ++rowIndex);
            this.backPolygonModeIndex = ParseToInt(sourceK["backPolygonMode"], 0, 10, fileName, ++rowIndex);

        }



        private int ParseToInt(string value, int minimalValue, int maximalValue, string path, int rowNumber)
        {
            var isValueInt = int.TryParse(value, out int result);

            if (isValueInt)
            {
                if (minimalValue > result || result > maximalValue) throw new Exception($"Данные в файле {path} , в строке номер {rowNumber} переходят предел " +
                    $"допустимых значений {minimalValue} < {value} < {maximalValue}");

                return result;
            }

            throw new Exception($"в файле {path} в строке номер {rowNumber} недопустимые символы");
        }


    }
}
