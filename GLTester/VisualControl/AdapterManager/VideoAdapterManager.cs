using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class VideoAdapterManager
    {
        private readonly Model refToModel;

        private readonly Func<int, int>[] blockSelectCommandN;

        private readonly TextureManager ManagerOfTexturez = new TextureManager();
          
        private readonly List<ArrayObject> vertexezArrayObjectStaticSpriteN = new List<ArrayObject>();
        private readonly List<ArrayObject> vertexezArrayObjectDynamicSpriteN = new List<ArrayObject>();
        private readonly List<ArrayObject> vertexezArrayObjectObjectN = new List<ArrayObject>();

        private ShaderProgramMaker MakerOfShaderProgram;

        private BufferObject VertexezAndColorBuffer;
        private BufferObject IndexezBuffer;

  
        private readonly Vector3 AxizX = new Vector3(1.0f, 0.0f, 0.0f);
        private readonly Vector3 AxizY = new Vector3(0.0f, 1.0f, 0.0f);
        private readonly Vector3 AxizZ = new Vector3(0.0f, 0.0f, 1.0f);

        private Vector3 LightPosition = new Vector3(10, 10, 20);
        private Vector3 LightColor = new Vector3(1, 1, 1);


        private int errorIndex;

        public void LoadDataFromCPUToGPU()
        {
            var Rulez = refToModel.GetMarkingRulez();


            ShaderCommand BasicShader;

            while (refToModel.HasNextShaderCommand())
            {
                BasicShader = refToModel.GetNextShaderCommand();

                MakeShaderProgramMaker(BasicShader.GetVertexShader(), BasicShader.GetFragmentShader());
            }


            var textureN = refToModel.LoadTexturez();

            var indexOfTexture = -1;

            for (int i = 0; i < textureN.Length; i++)
            {
                 indexOfTexture = MakeTexture(textureN[i].GetPixelz(), textureN[i].GetWidth(), textureN[i].GetHeight());

                refToModel.AddTextureIndex(textureN[i].GetName(), indexOfTexture);
            }



            var staticSpriteMessagerN = refToModel.MakeStaticSpritezMessager();

            var indexOfStaticSprite = -1;

            for (int i = 0; i < staticSpriteMessagerN.Length; i++)
            {
                if (staticSpriteMessagerN[i].GetObjectIndex() == 1)
                {
                    var result = refToModel.MakeSprite(staticSpriteMessagerN[i]);

                    indexOfStaticSprite = MakeVertexArrayObject(vertexezArrayObjectStaticSpriteN, result.GetEntityElementzArray(), result.GetIndexez(), 
                        TextureTarget.Texture2D, Rulez);

                    refToModel.AddStaticSpriteIndex(staticSpriteMessagerN[i].GetObjectName(), indexOfStaticSprite);
                }
            }


            var dynamicSpriteMessagerN = refToModel.MakeDynamicSpritezMessager();

            var indexOfDynamicSprite = -1;

            for (int i = 0; i < dynamicSpriteMessagerN.Length; i++)
            {
                if (dynamicSpriteMessagerN[i].GetObjectIndex() == 1)
                {
                    var result = refToModel.MakeSprite(dynamicSpriteMessagerN[i]);

                    indexOfDynamicSprite = MakeVertexArrayObject(vertexezArrayObjectDynamicSpriteN, result.GetEntityElementzArray(), result.GetIndexez(), 
                        TextureTarget.Texture2D, Rulez);

                    refToModel.AddDynamicSpriteIndex(dynamicSpriteMessagerN[i].GetObjectName(), indexOfDynamicSprite);

                }
            }


            var objectMessagerN = refToModel.MakeObjectzMessager();

            var indexOfObject = -1;

            for (int i = 0; i < objectMessagerN.Length; i++)
            {
                indexOfObject = MakeVertexArrayObject(vertexezArrayObjectObjectN, objectMessagerN[i].GetVertexezAndColorz(), objectMessagerN[i].GetIndexez(), 
                    TextureTarget.ProxyTextureCubeMap, Rulez);

                refToModel.AddObjectIndex(objectMessagerN[i].GetObjectName(), indexOfObject);
            }

            var settignN = refToModel.GetPerspectiveFieldOfViewSettingN();

            var aspectRatio = refToModel.GetScreenWidth() / refToModel.GetScreenHeight();

            var FieldOfView = Matrix4.CreatePerspectiveFieldOfView(settignN[0], aspectRatio, settignN[1], settignN[2]);



            MakerOfShaderProgram.Activate();

            MakerOfShaderProgram.SendUniformData(refToModel.GetProjectionMatrixUniformName(), FieldOfView);

            MakerOfShaderProgram.Deactivate();


            refToModel.InitializeWorld();

        }





        private int MakeVertexArrayObject(List<ArrayObject>receiverN, float[] vertexAndColorN, uint[] indexN, TextureTarget TextureType, MarkingRulezContainer Rulez)
        {
            VertexezAndColorBuffer = new BufferObject(BufferType.ArrayBuffer);
            VertexezAndColorBuffer.LoadData(vertexAndColorN, BufferHint.StaticDraw);

            IndexezBuffer = new BufferObject(BufferType.ElementArrayBuffer);
            IndexezBuffer.LoadData(indexN, BufferHint.StaticDraw);

            receiverN.Add(new ArrayObject(indexN.Length, TextureType));
            receiverN.Last().Activate();

            receiverN.Last().AttachBuffer(IndexezBuffer);
            receiverN.Last().AttachBuffer(VertexezAndColorBuffer);


            int attributeIndex;


            while (Rulez.HasNext())
            {
                attributeIndex = MakerOfShaderProgram.GetShaderAttribute(Rulez.GetNextSubBlockName());

                receiverN.Last().AttachAtribute(attributeIndex, Rulez.GetNextAmountOfElementzOfSubblock(), AttributePointerType.Float, false,
                    Rulez.GetNextSubBlockLengthWithTypeLength(), Rulez.GetNextSubBlockOffsetWithTypeLength());
            }


            receiverN.Last().Deactivate();
            receiverN.Last().DisableAllAttributez();

            return receiverN.Last().vertexArrayObjectIndex;

        }



        private void MakeShaderProgramMaker(string vertexShaderCommand, string fragmentShaderCommand) => MakerOfShaderProgram = new ShaderProgramMaker(
            vertexShaderCommand, fragmentShaderCommand);


        private int MakeTexture(float[] pixelN, int textureWidth, int textureHeight) => ManagerOfTexturez.MakeTexture(pixelN, textureWidth, textureHeight);




        public void Draw()
        {
            MakerOfShaderProgram.Activate();

            DrawVaoz(vertexezArrayObjectStaticSpriteN, staticSpriteBlockCommand);
            DrawVaoz(vertexezArrayObjectDynamicSpriteN, dynamicSpriteBlockCommand);
            DrawVaoz(vertexezArrayObjectObjectN, objectBlockCommand);

            MakerOfShaderProgram.Deactivate();

        }


        public void ClearFromResourcez()
        {
            for (int i = 0; i < vertexezArrayObjectObjectN.Count; i++)
            {
                vertexezArrayObjectObjectN[i].Dispose();
            }

            for (int i = 0; i < vertexezArrayObjectDynamicSpriteN.Count; i++)
            {
                vertexezArrayObjectDynamicSpriteN[i].Dispose();
            }



            ManagerOfTexturez.Dispose();

            MakerOfShaderProgram.Delete();
        }


        public Color GetSkyColor() => refToModel.GetSkyColor();


        private void DrawVaoz(List<ArrayObject> ToDraw, int blockSelectCommandIndex)
        {
            for (int i = 0; i < ToDraw.Count; i++)
            {
                ToDraw[i].Activate();
                ToDraw[i].EnableAllAttributez();

                errorIndex = blockSelectCommandN[blockSelectCommandIndex](ToDraw[i].vertexArrayObjectIndex);

                if (errorIndex > 0) throw new Exception("Индекс VAO объекта не найден методом Model.World.SelectVaoBlock()");


                while (refToModel.HasNext())
                {

                    MakerOfShaderProgram.SendUniformData(refToModel.GetTransformationMatrixUniformName(), CreateTransformationMatrix());
                    MakerOfShaderProgram.SendUniformData(refToModel.GetViewMatrixUniformName(), CreateViewMatrix());

                    MakerOfShaderProgram.SendUniformData(refToModel.GetlightPositionUniformName(), MakeLightPosition());
                    MakerOfShaderProgram.SendUniformData(refToModel.GetLightColorUniformName(), MakeLightColor());

                    MakerOfShaderProgram.SendUniformData(refToModel.GetShineDamperUniformName(), refToModel.GetCurrentObjectShineDamper());
                    MakerOfShaderProgram.SendUniformData(refToModel.GetReflectivityUniformName(), refToModel.GetCurrentObjectReflectivity());

                    MakerOfShaderProgram.SendUniformData(refToModel.GetAmbientLightUniformName(), refToModel.GetAmbientLight());


                    //GL.ActiveTexture(TextureUnit.Texture0);

                    //if (vertexezArrayObjectN[i].GetTextureIndex() > -1) GL.BindTexture(TextureTarget.Texture2D, vertexezArrayObjectN[i].GetTextureIndex());

                    if (refToModel.GetCurrentObjectTextureIndex() > -1) GL.BindTexture(TextureTarget.Texture2D, refToModel.GetCurrentObjectTextureIndex());

                    ToDraw[i].DrawElementz(0, ElementsType.UnsignedInt);

                }

                ToDraw[i].DisableAllAttributez();

                GL.BindTexture(TextureTarget.Texture2D, 0);

                ToDraw[i].Deactivate();
            }
        }




        private Matrix4 CreateTransformationMatrix()
        {
            Matrix4 ToReturn = Matrix4.Identity;

            ToReturn = TranslateMatrix(ToReturn, new Vector3(refToModel.GetCurrentObjectPositionX(), refToModel.GetCurrentObjectPositionY(),
                    refToModel.GetCurrentObjectPositionZ()));

                ToReturn = RotateTransformationMatrix(ToReturn, AxizX, refToModel.GetCurrentObjectAngleX());
                ToReturn = RotateTransformationMatrix(ToReturn, AxizY, refToModel.GetCurrentObjectAngleY());
                ToReturn = RotateTransformationMatrix(ToReturn, AxizZ, refToModel.GetCurrentObjectAngleZ());

            return ToReturn;
        }

        private Matrix4 RotateTransformationMatrix(Matrix4 MainMatrix, Vector3 RotationVector, float angle)
        {
            Matrix4 ToReturn = Matrix4.Identity;
            ToReturn = TranslateMatrix(ToReturn, new Vector3(-1, 0, 0));
            ToReturn = Matrix4.Mult(Matrix4.CreateFromAxisAngle(RotationVector, MathHelper.DegreesToRadians(angle)), ToReturn);
            ToReturn = TranslateMatrix(ToReturn, new Vector3(1, 0, 0));

            return Matrix4.Mult(ToReturn, MainMatrix);
        }



        private Matrix4 CreateViewMatrix()
        {
            Matrix4 ToReturn = Matrix4.Identity;

            ToReturn = RotateMatrix(ToReturn, AxizX, -refToModel.GetCameraAngleX());
            ToReturn = RotateMatrix(ToReturn, AxizY, refToModel.GetCameraAngleY());
            ToReturn = RotateMatrix(ToReturn, AxizZ, refToModel.GetCameraAngleZ());
            ToReturn = TranslateMatrix(ToReturn, new Vector3(refToModel.GetCameraPositionX(), refToModel.GetCameraPositionY(), refToModel.GetCameraPositionZ()));

            return ToReturn;
        }


        private Matrix4 TranslateMatrix(Matrix4 MainMatrix, Vector3 TranslationVector) =>
            Matrix4.Mult(Matrix4.CreateTranslation(TranslationVector), MainMatrix);

        private Matrix4 RotateMatrix(Matrix4 MainMatrix, Vector3 RotationVector, float angle) =>
             Matrix4.Mult(Matrix4.CreateFromAxisAngle(RotationVector, MathHelper.DegreesToRadians(angle)), MainMatrix);


        private Matrix4 ScaleMatrix(Matrix4 MainMatrix, float scale) => Matrix4.Mult(Matrix4.CreateScale(scale), MainMatrix);
        

        private Vector3 MakeLightPosition()
        {
            if (refToModel.IsLightPositionChanged())
            {
                LightPosition = new Vector3(refToModel.GetLightPositionX(), refToModel.GetLightPositionY(), refToModel.GetLightPositionZ());
            }

            return LightPosition;
        }


        private Vector3 MakeLightColor()
        {
            if (refToModel.IsLightColorChanged())
            {
                LightColor = new Vector3(refToModel.GetLightColorR(), refToModel.GetLightColorG(), refToModel.GetLightColorB());
            }

            return LightColor;
        }





        public VideoAdapterManager(Model refToModel)
        {
            this.refToModel = refToModel;


            blockSelectCommandN = new Func<int, int>[]
            {
                (value) => refToModel.SelectVaoStaticSpriteBlock(value),
                (value) => refToModel.SelectVaoDynamicSpriteBlock(value),
                (value) => refToModel.SelectVaoEntityBlock(value)
            };



        }

        public readonly int staticSpriteBlockCommand = 0;
        public readonly int dynamicSpriteBlockCommand = 1;
        public readonly int objectBlockCommand = 2;



    }
}
