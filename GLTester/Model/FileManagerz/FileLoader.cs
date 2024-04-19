using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GLTester
{
    internal class FileLoader
    {
        private readonly Dictionary<string, Action<string>> assignVariableK;

        private readonly DefaultRowAnalyzeLogic DefaultAnalyzer = new DefaultRowAnalyzeLogic();
        private readonly WorldSettingzRowAnalyzeLogic WorldSettingzAnalyzer = new WorldSettingzRowAnalyzeLogic();

        private readonly ConstantData ConstantzContainer = new ConstantData();
        private readonly ObjFileLoader LoaderOfObj = new ObjFileLoader();
        private readonly TextureLoader LoaderOfTexturez = new TextureLoader();

        private string settingzPath;
        private string objectzFolder;
        private string[] texturezPathN;
        private string worldSettingzFolder;
        private string spritezFolder;
        private string staticSpritezFolder;

        private readonly char[] dividerN = new char[] { ',' };


        public GameSettingzKeeper MakeGameSettingzKeeper()
        {
            StreamReader Reader = OpenOrDefault(settingzPath, ConstantzContainer.GetStandardSettingzFile());

            var rowNumber = 1;

            var settingK = new Dictionary<string, string>();

            string[] elementN;

            while (!Reader.EndOfStream)
            {
                elementN = DefaultAnalyzer.Analyze(Reader.ReadLine());

                settingK.Add(elementN[0], elementN[1]);
            }

            Reader.Close();

            return new GameSettingzKeeper(settingK);
        }



        public PrimitiveMessager[] MakeStaticSpritezMessager() => MakeSpritezMessager(staticSpritezFolder);

        public PrimitiveMessager[] MakeDynamicSpritezMessager() => MakeSpritezMessager(spritezFolder);



        public PrimitiveMessager[] MakeSpritezMessager(string path)
        {
            var objectPathN = Directory.GetFiles(path);

            PrimitiveMessager[] toReturn = new PrimitiveMessager[objectPathN.Length];

            int currentIndex = 0;

            for (int i = 0; i < objectPathN.Length; i++)
                    toReturn[currentIndex++] = MakeObjectzArgumentzMessager(objectPathN[i]);
            

            return toReturn;
        }


        public ObjFileMessager[] MakeObjectzMessager()
        {
            var objectPathN = Directory.GetFiles(objectzFolder);

            var filezCount = CountAmountOfFilez(objectPathN, "obj");

            ObjFileMessager[] toReturn = new ObjFileMessager[filezCount];

            int currentIndex = 0;

            for (int i = 0; i < objectPathN.Length; i++)
            {
                toReturn[currentIndex++] = LoaderOfObj.Load(objectPathN[i]);
            };

            return toReturn;
        }





        public ShaderCommand[] MakeShaderzCommandzContainer()
        {
            var shaderDirectoriezPathN = Directory.GetDirectories(ConstantzContainer.GetShaderzDirectoryPath());

            var toReturn = new ShaderCommand[shaderDirectoriezPathN.Length];

            for (int i = 0; i < shaderDirectoriezPathN.Length; i++)
            {
                toReturn[i] = MakeShaderzFromFile(shaderDirectoriezPathN[i]);
            };

            return toReturn;
        }




        public TextureMessager[] LoadTexturez() => LoaderOfTexturez.LoadTexturez(texturezPathN);


        public List<ObjectSettingzMessager> LoadWorldSettingz()
        {
            string path = worldSettingzFolder;

            StreamReader Reader = new StreamReader(path);

            List<ObjectSettingzMessager> toReturn = WorldSettingzAnalyzer.Analyze(Reader);

            Reader.Close();

            return toReturn;

        }


        public MarkingRulezContainer LoadMarkingRulez() => LoaderOfObj.LoadMarkingRulez();


        public FileLoader()
        {
            assignVariableK = new Dictionary<string, Action<string>>
            {
                { "settingzPath", (value) => settingzPath = value },
                { "objectzFolder", (value) => objectzFolder = value },
                { "texturezFolder", (value) => texturezPathN = Directory.GetFiles(value) },
                { "worldSettingzFolder", (value) => worldSettingzFolder = value },
                { "spritezFolder", (value) => spritezFolder = value },
                { "staticSpritezFolder", (value) => staticSpritezFolder = value }
            };



            if (!Directory.Exists(ConstantzContainer.GetSettingzDirectoryPath())) Directory.CreateDirectory(ConstantzContainer.GetSettingzDirectoryPath());
            if (!Directory.Exists(ConstantzContainer.GetObjectzDirectoryPath())) Directory.CreateDirectory(ConstantzContainer.GetObjectzDirectoryPath());

            StreamReader Reader = OpenOrDefault(ConstantzContainer.GetMainPath(), ConstantzContainer.GetStandardMainFile());

            string[] elementN;

            while (!Reader.EndOfStream)
            {
                elementN = DefaultAnalyzer.Analyze(Reader.ReadLine());


                assignVariableK[elementN[0]](elementN[1]);
            }

            Reader.Close();
        }


        private PrimitiveMessager MakeObjectzArgumentzMessager(string path)
        {
            StreamReader Reader = new StreamReader(path);

            var objectType = Reader.ReadLine();

            if (!objectType.StartsWith("OpenGLObject")) return null;

            int rowNumber = 1;

            var objectName = Reader.ReadLine();
            rowNumber++;
            var objectIndex = int.Parse(Reader.ReadLine());

            var argumentz = Reader.ReadLine();

            Reader.Close();

            var ToReturn = new PrimitiveMessager(path, objectIndex, argumentz, dividerN);

            return ToReturn;
        }


        private StreamReader OpenOrDefault(string path, string defaultValue)
        {
            if (!File.Exists(path))
            {
                StreamWriter Writer = new StreamWriter(path);
                Writer.Write(defaultValue);
                Writer.Close();
            }

            return new StreamReader(path);
        }





        private ShaderCommand MakeShaderzFromFile(string path) 
        {
            var shaderFilezPath = Directory.GetFiles(path);

            var vertexShaderEndPart = ConstantzContainer.GetVertexShaderEndPart();
            var fragmentShaderEndPart = ConstantzContainer.GetFragmentShaderEndPart();

            string vertexShader = "";
            string fragmentShader = "";

            for (int i = 0; i < shaderFilezPath.Length; i++)
            {
                if (shaderFilezPath[i].EndsWith(vertexShaderEndPart)) vertexShader = File.ReadAllText(shaderFilezPath[i]);
                else if(shaderFilezPath[i].EndsWith(fragmentShaderEndPart)) fragmentShader = File.ReadAllText(shaderFilezPath[i]);
            }

            return new ShaderCommand(vertexShader, fragmentShader);

        }


        private int CountAmountOfFilez(string[] objectPathN, string fileTypeName)
        {
            int count = 0;

            for (int i = 0; i < objectPathN.Length; i++)
                if (objectPathN[i].EndsWith(fileTypeName)) count++;

            return count;
        }


    }
}
