using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class ShaderCommand
    {

        private readonly string vertexShader;
        private readonly string fragmentShader;

        public string GetVertexShader() => vertexShader;
        public string GetFragmentShader() => fragmentShader;

        public ShaderCommand(string vertexShader, string fragmentShader)
        {
            this.vertexShader = vertexShader;
            this.fragmentShader = fragmentShader;
        }
    }
}
