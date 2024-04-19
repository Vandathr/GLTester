using OpenTK;
using System;
using OpenTK.Graphics.OpenGL4;

namespace GLTester
{
    internal class ShaderProgramMaker
    {
        private readonly int vertexShaderIndex = 0;
        private readonly int fragmentShaderIndex = 0;
        private readonly int shaderProgramIndex = 0;


        public void Activate() => GL.UseProgram(shaderProgramIndex);
        public void Deactivate() => GL.UseProgram(0);
        public void Delete() => GL.DeleteProgram(shaderProgramIndex);

        public int GetShaderAttribute(string attributeName) => GL.GetAttribLocation(shaderProgramIndex, attributeName);



        public void SendUniformData(string uniformName, float necessaryValue)
        {
            int uniformLocation = GL.GetUniformLocation(shaderProgramIndex, uniformName);

            GL.Uniform1(uniformLocation, necessaryValue);
        }



        public void SendUniformData(string uniformName, Vector3 NecessaryVector)
        {
            int uniformLocation = GL.GetUniformLocation(shaderProgramIndex, uniformName);

            GL.Uniform3(uniformLocation, NecessaryVector);
        }



        public void SendUniformData(string uniformName, Matrix4 NecessaryMatirx)
        {
            int uniformLocation = GL.GetUniformLocation(shaderProgramIndex, uniformName);

            GL.UniformMatrix4(uniformLocation, false, ref NecessaryMatirx);
        }


        public void AttachAttribute(int attributeIndex, string variableName) => GL.BindAttribLocation(shaderProgramIndex, attributeIndex, variableName);


        public ShaderProgramMaker(string vertexShaderCode, string fragmentShaderCode)
        {

            vertexShaderIndex = MakeShader(ShaderType.VertexShader, vertexShaderCode);
            fragmentShaderIndex = MakeShader(ShaderType.FragmentShader, fragmentShaderCode);

            shaderProgramIndex = GL.CreateProgram();

            GL.AttachShader(shaderProgramIndex, vertexShaderIndex);
            GL.AttachShader(shaderProgramIndex, fragmentShaderIndex);

            GL.LinkProgram(shaderProgramIndex);

            GL.GetProgram(shaderProgramIndex, GetProgramParameterName.LinkStatus, out var errorCode);

            if (errorCode != 1)
            {
                var errorInfo = GL.GetProgramInfoLog(shaderProgramIndex);
                throw new Exception($"Ошибка линковки шейдера с индексом {shaderProgramIndex} \n Текст ошибки:{errorInfo}");
            }

            DeleteShader(vertexShaderIndex);
            DeleteShader(fragmentShaderIndex);
        }


        private int MakeShader(ShaderType TypeOfShader, string shaderCode)
        {
            int shaderIndex = GL.CreateShader(TypeOfShader);
            GL.ShaderSource(shaderIndex, shaderCode);
            GL.CompileShader(shaderIndex);

            GL.GetShader(shaderIndex, ShaderParameter.CompileStatus, out var errorCode);

            if (errorCode != 1)
            {
                var errorInfo = GL.GetShaderInfoLog(shaderIndex);
                throw new Exception($"Ошибка компиляции шейдера с индексом {shaderCode} \n Текст ошибки:{errorInfo}");
            }

            return shaderIndex;
        }


        private void DeleteShader(int ShaderIndex)
        {
            GL.DetachShader(shaderProgramIndex, ShaderIndex);
            GL.DeleteShader(ShaderIndex);
        }

    }
}
