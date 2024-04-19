using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    public enum BufferHint
    {
        StaticDraw = OpenTK.Graphics.OpenGL4.BufferUsageHint.StaticDraw,
        DynamicDraw = OpenTK.Graphics.OpenGL4.BufferUsageHint.DynamicDraw
    }
}
