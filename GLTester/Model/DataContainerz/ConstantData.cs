using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class ConstantData
    {
        private readonly string mainPath = "Main.txt";
        private readonly string settingzDirectoryPath = "SystemData";
        private readonly string objectzDirectoryPath = "Objectz";
        private readonly string shaderzDirectoryPath = "Shaderz";
        private readonly string texturezDirectoryPath = "Texturez";
        private readonly string standardMainFile = "SystemData\\Settings.txt\r\nObjectz\r\nShaderz";
        private readonly string standardSettingzFile = "600\r\n600\r\n0\r\nTest\r\n0\r\n0\r\n4\r\n6\r\n0\r\n40";

        private readonly string vertexShaderEndPart = ".vert";
        private readonly string fragmentShaderEndPart = ".frag";

        public string GetMainPath() => mainPath;
        public string GetSettingzDirectoryPath() => settingzDirectoryPath;
        public string GetObjectzDirectoryPath() => objectzDirectoryPath;
        public string GetShaderzDirectoryPath() => shaderzDirectoryPath;
        public string GetTexturezDirectoryPath() => texturezDirectoryPath;
        public string GetStandardMainFile() => standardMainFile;
        public string GetStandardSettingzFile() => standardSettingzFile;
        public string GetVertexShaderEndPart() => vertexShaderEndPart;
        public string GetFragmentShaderEndPart() => fragmentShaderEndPart;

    }
}
