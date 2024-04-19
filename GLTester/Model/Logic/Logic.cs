using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class Logic: IActualizable
    {
        private readonly WorldTime TimeOfWorld = new WorldTime(0, 0, 10);

        private readonly RandomGenerator RND = new RandomGenerator(1);

        private readonly List<GameObjectzContainer> staticSpritezContainerN = new List<GameObjectzContainer>();
        private readonly List<GameObjectzContainer> dynamicSpritezContainerN = new List<GameObjectzContainer>();
        private readonly List<GameObjectzContainer> entityzContainerN = new List<GameObjectzContainer>();


        private readonly Light MainLight = new Light(1.0f, new Color(1.0f, 1.0f, 1.0f, 1.0f), new MovableObject(0, 0, 0, 0, 0, 0, 0.1f, 0.1f));

        private GameObjectzContainer refToVaoContainerBlock;

        private readonly Color DefaultSkyColor = new Color(170, 215, 230, 255);

        private Color SkyColor = new Color(170, 215, 230, 255);

        private float skyColorKoefficient;

        private bool isLightPositionChanged;

        private bool isLightColorChanged;




        public void Actualize()
        {
            TimeOfWorld.Increment();

            MainLight.SetAmbientLight(CalculateAmbientLight());

            isLightPositionChanged = true;

            MainLight.SetLightColorIntensity(CalculateMainLightIntensity());

            isLightColorChanged = true;

            CalculateSkyColorIntensity();
        }


        public void AddSpriteObject(ObjectSettingzMessager MessagerOfObject, int objectVaoIndex, int textureVaoIndex)
        {
            GameObjectzContainer refToSpritezContainer = null;

            if (MessagerOfObject.GetUsingCommand() == "primitive")
            {

                if (MessagerOfObject.GetEntityType() == "staticSprite")
                    refToSpritezContainer = FindContainerOrCreate(objectVaoIndex, staticSpritezContainerN);
                else if(MessagerOfObject.GetEntityType() == "Sprite")
                    refToSpritezContainer = FindContainerOrCreate(objectVaoIndex, dynamicSpritezContainerN);

                refToSpritezContainer.Add(MakeGameObject(MessagerOfObject, objectVaoIndex, textureVaoIndex));

            }
            else if (MessagerOfObject.GetUsingCommand() == "random")
            {
                refToSpritezContainer = FindContainerOrCreate(objectVaoIndex, dynamicSpritezContainerN);

                int amountOfObjectz = int.Parse(MessagerOfObject.GetAmountOfObjectz());

                string[] elementN = MessagerOfObject.GetPositionX().Split('|');

                var minXValue = int.Parse(elementN[0]);
                var maxXValue = int.Parse(elementN[1]);

                elementN = MessagerOfObject.GetPositionY().Split('|');

                var minYValue = int.Parse(elementN[0]);
                var maxYValue = int.Parse(elementN[1]);


                for (int i = 0; i < amountOfObjectz; i++)
                {
                    RND.SetMinValue(minXValue);
                    RND.SetMaxValue(maxXValue);
                    int randomX = RND.GetRandom();

                    RND.SetMinValue(minYValue);
                    RND.SetMaxValue(maxYValue);
                    int randomY = RND.GetRandom();

                    string[] toSendN = new string[9];

                    toSendN[3] = randomX.ToString();
                    toSendN[4] = randomY.ToString();
                    toSendN[5] = MessagerOfObject.GetPositionZ();

                    toSendN[6] = MessagerOfObject.GetAngleX();
                    toSendN[7] = MessagerOfObject.GetAngleY();
                    toSendN[8] = MessagerOfObject.GetAngleZ();

                    var MoveObject = new MovableObject(randomX, randomY, float.Parse(MessagerOfObject.GetPositionZ(), CultureInfo.InvariantCulture),
                        float.Parse(MessagerOfObject.GetAngleX(), CultureInfo.InvariantCulture),
                        float.Parse(MessagerOfObject.GetAngleY(), CultureInfo.InvariantCulture),
                        float.Parse(MessagerOfObject.GetAngleZ(), CultureInfo.InvariantCulture),
                        0.1f, 0.1f);

                    var ContainerOfPhysicInternalz = new PhysicInternalzContainer(0, 0);

                    refToSpritezContainer.Add(new GameObject(objectVaoIndex, textureVaoIndex, MoveObject, ContainerOfPhysicInternalz));

                }


            }



        }


        public void AddObject(ObjectSettingzMessager MessagerOfObject, int objectVaoIndex, int textureVaoIndex)
        {
            GameObjectzContainer refToObjectzContainer;

            refToObjectzContainer = FindContainerOrCreate(objectVaoIndex, entityzContainerN);

            refToObjectzContainer.Add(MakeGameObject(MessagerOfObject, objectVaoIndex, textureVaoIndex));
        }




        public int SelectVaoStaticSpriteBlock(int vaoIndex) => SelectBlock(vaoIndex, staticSpritezContainerN);
        public int SelectVaoDynamicSpriteBlock(int vaoIndex) => SelectBlock(vaoIndex, dynamicSpritezContainerN);
        public int SelectVaoEntityBlock(int vaoIndex) => SelectBlock(vaoIndex, entityzContainerN);



        public bool HasNext() => refToVaoContainerBlock.HasNext();


        public float GetCurrentObjectPositionX() => refToVaoContainerBlock.GetCurrentObjectPositionX();
        public float GetCurrentObjectPositionY() => refToVaoContainerBlock.GetCurrentObjectPositionY();
        public float GetCurrentObjectPositionZ() => refToVaoContainerBlock.GetCurrentObjectPositionZ();

        public float GetCurrentObjectAngleX() => refToVaoContainerBlock.GetCurrentObjectAngleX();
        public float GetCurrentObjectAngleY() => refToVaoContainerBlock.GetCurrentObjectAngleY();
        public float GetCurrentObjectAngleZ() => refToVaoContainerBlock.GetCurrentObjectAngleZ();

        public int GetCurrentObjectTextureIndex() => refToVaoContainerBlock.GetCurrentObjectTextureIndex();

        public float GetCurrentObjectShineDamper() => refToVaoContainerBlock.GetCurrentObjectShineDamper();
        public float GetCurrentObjectReflectivity() => refToVaoContainerBlock.GetCurrentObjectReflectivity();


        public void PointSpritez(float X, float Y)
        {
            for(int i = 0; i < dynamicSpritezContainerN.Count; i++)
            {
                dynamicSpritezContainerN[i].MoveStart();

                while (dynamicSpritezContainerN[i].HasNext())
                {
                    dynamicSpritezContainerN[i].PointObject(X, Y);
                }

                
            }
        }


         


        public float GetAmbientLight() => MainLight.GetAmbientLight();


        public bool IsLightPositionChanged() 
        {
            if (isLightPositionChanged)
            {
                isLightPositionChanged = false;
                return true;
            }

            return false;
        }


        public float GetLightPositionX() => MainLight.GetPositionX();
        public float GetLightPositionY() => MainLight.GetPositionY();
        public float GetLightPositionZ() => MainLight.GetPositionZ();



        public bool IsLightColorChanged() 
        {
            if (isLightColorChanged)
            {
                isLightColorChanged = false;
                return true;
            }

            return false;
        }

        public float GetLightColorR() => MainLight.GetLightColorR();
        public float GetLightColorG() => MainLight.GetLightColorG();
        public float GetLightColorB() => MainLight.GetLightColorB();


        public Color GetSkyColor() => SkyColor;


        public int GetWorldSecondz() => TimeOfWorld.GetSecondz();
        public int GetWorldMinutez() => TimeOfWorld.GetMinutez();
        public int GetWorldHourz() => TimeOfWorld.GetHourz();





        public float GetDebugInfo() => dynamicSpritezContainerN[0].GetDebugInfo();
        


        private GameObject MakeGameObject(ObjectSettingzMessager Source, int objectVaoIndex, int textureVaoIndex)
        {
            var MoveObject = new MovableObject(float.Parse(Source.GetPositionX(), CultureInfo.InvariantCulture),
                        float.Parse(Source.GetPositionY(), CultureInfo.InvariantCulture),
                        float.Parse(Source.GetPositionZ(), CultureInfo.InvariantCulture),
                        float.Parse(Source.GetAngleX(), CultureInfo.InvariantCulture),
                        float.Parse(Source.GetAngleY(), CultureInfo.InvariantCulture),
                        float.Parse(Source.GetAngleZ(), CultureInfo.InvariantCulture),
                        0.1f, 0.1f);

            var ContainerOfPhysicInternalz = new PhysicInternalzContainer(0, 0);

            return new GameObject(objectVaoIndex, textureVaoIndex, MoveObject, ContainerOfPhysicInternalz);
        }




        private GameObjectzContainer FindContainerOrCreate(int objectVaoIndex, List<GameObjectzContainer> toSearch)
        {

            for (int i = 0; i < toSearch.Count; i++)
            {
                if (toSearch[i].CheckVaoIndex(objectVaoIndex))
                    return toSearch[i];

            }

            toSearch.Add(new GameObjectzContainer(objectVaoIndex));
            return toSearch.Last();

        }

        private int SelectBlock(int vaoIndex, List<GameObjectzContainer> ToSelect)
        {
            for (int i = 0; i < ToSelect.Count; i++)
            {
                if (ToSelect[i].CheckVaoIndex(vaoIndex))
                {
                    refToVaoContainerBlock = ToSelect[i];
                    ToSelect[i].MoveStart();
                    break;
                }
            }
            return 0;
        }


        private float CalculateAmbientLight()
        {
            if (TimeOfWorld.GetHourz() - 12 > 0)
            {
                return (float)((TimeOfWorld.GetHourz() - (TimeOfWorld.GetHourz() - 12) * 2) * 0.08);
            }
            else return (float)(TimeOfWorld.GetHourz() * 0.08);
        }


        private float CalculateMainLightIntensity()
        {
            if (TimeOfWorld.GetHourz() - 12 > 0)
            {
                return (float)((TimeOfWorld.GetHourz() - (TimeOfWorld.GetHourz() - 12) * 2) * 0.08);
            }
            else return (float)(TimeOfWorld.GetHourz() * 0.08);
        }


        private void CalculateSkyColorIntensity()
        {

            if (TimeOfWorld.GetHourz() - 12 > 0)
            {
                skyColorKoefficient = (float)((TimeOfWorld.GetHourz() - (TimeOfWorld.GetHourz() - 12) * 2) * 0.08);
            }
            else skyColorKoefficient = (float)(TimeOfWorld.GetHourz() * 0.08);

            if (skyColorKoefficient < 0.01f) skyColorKoefficient = 0.01f;

            SkyColor = new Color(DefaultSkyColor.GetR() * skyColorKoefficient, DefaultSkyColor.GetG() * skyColorKoefficient, 
                DefaultSkyColor.GetB() * skyColorKoefficient, DefaultSkyColor.GetA());

        }




    }
}
