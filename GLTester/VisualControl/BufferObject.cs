using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace GLTester
{
    public class BufferObject: IDisposable
    {
        private readonly BufferTarget TypeOfBuffer;

        private readonly int errorCode = -1;

        private bool isBufferActive = false;

        private IntPtr sizeOfElement;

        public int bufferIndex { get; private set; } = 0;

        

        public void Activate()
        {
            GL.BindBuffer(TypeOfBuffer, bufferIndex);
            isBufferActive = true;
        }

        public void Deactivate()
        {
            GL.BindBuffer(TypeOfBuffer, 0);
            isBufferActive = false;
        }


        public bool IsBufferActive() => isBufferActive;


        public void LoadData<T>(T[] vertexToDrawN, BufferHint HintOfBuffer) where T:struct
        {
            if (vertexToDrawN.Length < 1) throw new ArgumentException("Массив пустой", "dataN");

            Activate();

            sizeOfElement = (IntPtr)(vertexToDrawN.Length * Marshal.SizeOf(typeof(T)));

            GL.BufferData(TypeOfBuffer, sizeOfElement, vertexToDrawN, BufferUsageHint.StaticDraw);

        }


        public void Delete()
        {
            if (bufferIndex == errorCode) return;

            Deactivate();
            GL.DeleteBuffer(bufferIndex);

            bufferIndex = errorCode;
        }



        public void Dispose()
        {
            Delete();
            GC.SuppressFinalize(this);
        }

        public BufferObject(BufferType TypeOfBuffer)
        {
            this.TypeOfBuffer = (BufferTarget)TypeOfBuffer;

            bufferIndex = GL.GenBuffer();

           
        }



    }
}
