using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class Model : IActualizable
    {
        private readonly TimeCounter CounterOfTime;

        private readonly Logic World = new Logic();

        private readonly FileLoader LoaderOfFilez = new FileLoader();
        private readonly PrimitivezFabric FabricOfObjectz = new PrimitivezFabric();

        private readonly Camera CameraObject = new Camera();

        private readonly GameSettingzKeeper KeeperOfGameSettingz;


        private readonly Dictionary<string, int> objectVaoK = new Dictionary<string, int>();
        private readonly Dictionary<string, int> staticSpriteVaoK = new Dictionary<string, int>();
        private readonly Dictionary<string, int> dynamicSpriteVaoK = new Dictionary<string, int>();
        private readonly Dictionary<string, int> textureVaoK = new Dictionary<string, int>();


        private readonly ShaderCommand[] shaderCommandN; 

        private readonly Action[] keyDownN = new Action[200];

        private bool exitCommand;


        private int shaderCommandIndex = -1;


        private int systemTime;



        public void InitializeWorld()
        {
            var rowN = LoaderOfFilez.LoadWorldSettingz();

            var elementVaoIndex = -1;
            var textureVaoIndex = -1;

            for (int i = 0; i < rowN.Count; i++)
            {
                if (rowN[i].GetEntityType() == "Sprite")
                {
                    if (dynamicSpriteVaoK.ContainsKey(rowN[i].GetObjectFileName())) elementVaoIndex = dynamicSpriteVaoK[rowN[i].GetObjectFileName()];

                    if (textureVaoK.ContainsKey(rowN[i].GetTextureFileName())) textureVaoIndex = textureVaoK[rowN[i].GetTextureFileName()];

                    World.AddSpriteObject(rowN[i], elementVaoIndex, textureVaoIndex);
                }
                else if (rowN[i].GetEntityType() == "Object")
                {
                    if (objectVaoK.ContainsKey(rowN[i].GetObjectFileName())) elementVaoIndex = objectVaoK[rowN[i].GetObjectFileName()];

                    if (textureVaoK.ContainsKey(rowN[i].GetTextureFileName())) textureVaoIndex = textureVaoK[rowN[i].GetTextureFileName()];

                    World.AddObject(rowN[i], elementVaoIndex, textureVaoIndex);
                }
                else if (rowN[i].GetEntityType() == "staticSprite")
                {
                    if (staticSpriteVaoK.ContainsKey(rowN[i].GetObjectFileName())) elementVaoIndex = staticSpriteVaoK[rowN[i].GetObjectFileName()];

                    if (textureVaoK.ContainsKey(rowN[i].GetTextureFileName())) textureVaoIndex = textureVaoK[rowN[i].GetTextureFileName()];

                    World.AddSpriteObject(rowN[i], elementVaoIndex, textureVaoIndex);
                }


            }

            

        }


        public MarkingRulezContainer GetMarkingRulez() 
        { 
            var toReturn = LoaderOfFilez.LoadMarkingRulez();

            FabricOfObjectz.SetMarkingRulez(toReturn);

            return toReturn;
        }




        #region GameSettingzArea

        public int GetScreenWidth() => KeeperOfGameSettingz.GetScreenWidth();
        public int GetScreenHeight() => KeeperOfGameSettingz.GetScreenHeight();
        public int GetGraphicsModeIndex() => KeeperOfGameSettingz.GetGraphicsModeIndex();
        public string GetWindowTitle() => KeeperOfGameSettingz.GetWindowTitle();
        public int GetGameWindowFlagsIndex() => KeeperOfGameSettingz.GetGameWindowFlagsIndex();
        public int GetDisplayDeviceIndex() => KeeperOfGameSettingz.GetDisplayDeviceIndex();
        public int GetManorOpenGLVersion() => KeeperOfGameSettingz.GetManorOpenGLVersion();
        public int GetMinorOpenGLVersion() => KeeperOfGameSettingz.GetMinorOpenGLVersion();
        public int GetContextFlagIndex() => KeeperOfGameSettingz.GetContextFlagIndex();
        public int GetUpdateRate() => KeeperOfGameSettingz.GetUpdateRate();
        public int GetFrontPoligineModeIndex() => KeeperOfGameSettingz.GetFrontPolygonModeIndex();
        public int GetBackPoligineModeIndex() => KeeperOfGameSettingz.GetBackPolygonModeIndex();


        public int[] GetOrthoSettingz() => KeeperOfGameSettingz.GetOrthoSettingz();
        public int[] GetFrustumSettingN() => KeeperOfGameSettingz.GetFrustumSettingN();
        public float[] GetPerspectiveFieldOfViewSettingN() => KeeperOfGameSettingz.GetPerspectiveFieldOfViewSettingN();

        #endregion

        #region ObjectzContainerArea

        public void AddStaticSpriteIndex(string name, int index) => staticSpriteVaoK.Add(name, index);
        public void AddDynamicSpriteIndex(string name, int index) => dynamicSpriteVaoK.Add(name, index);

        public void AddObjectIndex(string name, int index) => objectVaoK.Add(name, index);

        #endregion

        #region ShaderArea

        public bool HasNextShaderCommand()
        {
            if(shaderCommandIndex < shaderCommandN.Length - 1) return true;

            shaderCommandIndex = -1;
            return false;
        }


        public ShaderCommand GetNextShaderCommand() 
        {
            shaderCommandIndex++;
            return shaderCommandN[shaderCommandIndex];
        }


        public ShaderCommand GetShader(int index) => shaderCommandN[index];

        #endregion

        #region TexturingArea

        public void AddTextureIndex(string name, int index) => textureVaoK.Add(name, index);

        #endregion


        #region CameraCommandzArea

        public float GetCameraPositionX() => CameraObject.GetPositionX();
        public float SetCameraPositionX(float value) => CameraObject.SetCameraPositionX(value);
        public float GetCameraPositionY() => CameraObject.GetPositionY();
        public float SetCameraPositionY(float value) => CameraObject.SetCameraPositionY(value);
        public float GetCameraPositionZ() => CameraObject.GetPositionZ();
        public float SetCameraPositionZ(float value) => CameraObject.SetCameraPositionZ(value);

        public float GetCameraAngleX() => CameraObject.GetAngleX();
        public float GetCameraAngleY() => CameraObject.GetAngleY();
        public float GetCameraAngleZ() => CameraObject.GetAngleZ();

        #endregion


        #region ShadersAttributezArea

        public string GetProjectionMatrixUniformName() => "projectionMatrix";
        public string GetTransformationMatrixUniformName() => "transformationMatrix";
        public string GetViewMatrixUniformName() => "viewMatrix";

        public string GetlightPositionUniformName() => "lightPosition";
        public string GetLightColorUniformName() => "lightColor";

        public string GetShineDamperUniformName() => "shineDamper";
        public string GetReflectivityUniformName() => "shineDamper";

        public string GetAmbientLightUniformName() => "ambientLight";

        #endregion



        #region LightArea

        
        public float GetAmbientLight() => World.GetAmbientLight();

        public bool IsLightPositionChanged() => World.IsLightPositionChanged();

        public float GetLightPositionX() => World.GetLightPositionX();
        public float GetLightPositionY() => World.GetLightPositionY();
        public float GetLightPositionZ() => World.GetLightPositionZ();


        public bool IsLightColorChanged() => World.IsLightColorChanged();

        public float GetLightColorR() => World.GetLightColorR();
        public float GetLightColorG() => World.GetLightColorG();
        public float GetLightColorB() => World.GetLightColorB();


        public Color GetSkyColor() => World.GetSkyColor();


        #endregion



        public void DoWork()
        {
            World.PointSpritez(CameraObject.GetPositionX(), CameraObject.GetPositionY());
        }

        public void MouseMove(float X, float Y)
        {
            CameraObject.IncrementAngleZ(X);
            CameraObject.IncrementAngleX(Y);
        }
        



        public void ButtonDown(int keyCode)
        {
            keyDownN[keyCode]();
        }


        public bool CheckExitCommand() => exitCommand;


        #region GameObjectzInteraction

        public int SelectVaoStaticSpriteBlock(int vaoIndex) => World.SelectVaoStaticSpriteBlock(vaoIndex);
        public int SelectVaoDynamicSpriteBlock(int vaoIndex) => World.SelectVaoDynamicSpriteBlock(vaoIndex);
        public int SelectVaoEntityBlock(int vaoIndex) => World.SelectVaoEntityBlock(vaoIndex);


        public bool HasNext() => World.HasNext();


        public float GetCurrentObjectPositionX() => World.GetCurrentObjectPositionX();
        public float GetCurrentObjectPositionY() => World.GetCurrentObjectPositionY();
        public float GetCurrentObjectPositionZ() => World.GetCurrentObjectPositionZ();

        public float GetCurrentObjectAngleX() => World.GetCurrentObjectAngleX();
        public float GetCurrentObjectAngleY() => World.GetCurrentObjectAngleY();
        public float GetCurrentObjectAngleZ() => World.GetCurrentObjectAngleZ();

        public int GetCurrentObjectTextureIndex() => World.GetCurrentObjectTextureIndex();


        public float GetCurrentObjectShineDamper() => World.GetCurrentObjectShineDamper();
        public float GetCurrentObjectReflectivity() => World.GetCurrentObjectReflectivity();


        #endregion

        public PrimitiveMessager[] MakeStaticSpritezMessager() => LoaderOfFilez.MakeStaticSpritezMessager();
        public PrimitiveMessager[] MakeDynamicSpritezMessager() => LoaderOfFilez.MakeDynamicSpritezMessager();
        public ObjFileMessager[] MakeObjectzMessager() => LoaderOfFilez.MakeObjectzMessager();


        public GameObjectMessager MakeSprite(PrimitiveMessager objectArgument) => FabricOfObjectz.Make(objectArgument);


        public TextureMessager[] LoadTexturez() => LoaderOfFilez.LoadTexturez();



        public void Actualize()
        {
            systemTime++;

            World.Actualize();
        }



        #region SystemCommandz

        public float GetDebugData() => World.GetDebugInfo();

        public int GetSystemTime() => systemTime;

        public int GetWorldSecondz() => World.GetWorldSecondz();
        public int GetWorldMinutez() => World.GetWorldMinutez();
        public int GetWorldHourz() => World.GetWorldHourz();




        #endregion


        public Model()
        {
            KeeperOfGameSettingz = LoaderOfFilez.MakeGameSettingzKeeper();
            shaderCommandN = LoaderOfFilez.MakeShaderzCommandzContainer();

            CounterOfTime = new TimeCounter(this, 1000);

            FabricOfObjectz = new PrimitivezFabric();


            keyDownN[50] = () => exitCommand = true;

            keyDownN[83] = () => CameraObject.MoveLeft();

            keyDownN[86] = () => CameraObject.MoveRight();

            keyDownN[87] = () => CameraObject.MoveDown();

            keyDownN[99] = () => CameraObject.MoveUp();

            keyDownN[101] = () => CameraObject.MoveBackward();

            keyDownN[105] = () => CameraObject.MoveForward();

        }


    }
}
