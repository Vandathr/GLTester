using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL4;

namespace GLTester
{
    public class ArrayObject: IDisposable
    {
        private readonly int errorCode = -1;

        private readonly int indexezAmount;

        private readonly TextureTarget TextureType = TextureTarget.Texture2D;

        private bool isArrayActive = false;

        private List<int> attributeN;
        private List<BufferObject> bufferN;


        public int vertexArrayObjectIndex { get; private set; } = 0;

        //private int indexOfTexture = -1;

        

        public void Activate()
        {
            GL.BindVertexArray(vertexArrayObjectIndex);
            isArrayActive = true;
        }

        public void Deactivate()
        {
            GL.BindVertexArray(0);
            isArrayActive = false;
        }

        public bool IsArrayActive() => isArrayActive;


        public void AttachBuffer(BufferObject ToAttach)
        {
            if (!IsArrayActive()) Activate();

            ToAttach.Activate();

            bufferN.Add(ToAttach);

        }


        public void AttachAtribute(int vertexAttributeIndex, int elementzPerVertex, AttributePointerType TypeOfAttributePointer, bool normalized, int stride, int offset)
        {
            attributeN.Add(vertexAttributeIndex);
            GL.EnableVertexAttribArray(vertexAttributeIndex);
            GL.VertexAttribPointer(vertexAttributeIndex, elementzPerVertex, (VertexAttribPointerType)TypeOfAttributePointer, normalized, stride, offset);
        }


        public void DrawArrayz(int startIndex, int length)
        {
            Activate();
            GL.DrawArrays(PrimitiveType.Triangles, startIndex, length);
        }


        public void DrawElementz(int startIndex, int length, ElementsType typeOfElement)
        {
            Activate();
            GL.DrawElements(PrimitiveType.Triangles, length, (DrawElementsType)typeOfElement, startIndex);
        }


        public void DrawElementz(int startIndex, ElementsType typeOfElement)
        {
            Activate();
            GL.DrawElements(PrimitiveType.Triangles, indexezAmount, (DrawElementsType)typeOfElement, startIndex);
        }


        public void EnableAllAttributez()
        {
            for (int i = 0; i < attributeN.Count; i++)
                GL.EnableVertexAttribArray(attributeN[i]);
        }


        public void DisableAllAttributez()
        {
            for(int i = 0; i < attributeN.Count; i++)
                GL.DisableVertexAttribArray(attributeN[i]);
        }

        private void Delete()
        {
            if (vertexArrayObjectIndex == errorCode) return;

            Deactivate();
            GL.DeleteVertexArray(vertexArrayObjectIndex);

            for (int i = 0; i < bufferN.Count; i++)
                bufferN[i].Dispose();

            vertexArrayObjectIndex = errorCode;
        }


        public void Dispose()
        {
            Delete();
            GC.SuppressFinalize(this);
        }


        public int GetIndexezAmount() => indexezAmount;

        //public int GetTextureIndex() => indexOfTexture;

        public TextureTarget GetTextureType() => TextureType;

        public ArrayObject(int indexezAmount, TextureTarget TextureType)
        {
            attributeN = new List<int>();
            bufferN = new List<BufferObject>();
            vertexArrayObjectIndex = GL.GenVertexArray();
            this.indexezAmount = indexezAmount;

            this.TextureType = TextureType;
        }


    }
}
