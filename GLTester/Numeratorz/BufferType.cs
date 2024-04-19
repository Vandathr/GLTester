using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    public enum BufferType
    {
       ArrayBuffer = OpenTK.Graphics.OpenGL4.BufferTarget.ArrayBuffer,
       ElementArrayBuffer = OpenTK.Graphics.OpenGL4.BufferTarget.ElementArrayBuffer,
       TextureBuffer = OpenTK.Graphics.OpenGL4.BufferTarget.TextureBuffer
    }
}
