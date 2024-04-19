using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class ObjFileLoader
    {
        private readonly MarkingRulezContainer Rulez;

        private readonly Action<string>[] commandN;

        private readonly List<float> vertexRawN = new List<float>();
        private readonly List<float> textureCoordinateRawN = new List<float>();
        private readonly List<float> vertexNormalRawN = new List<float>();

        private readonly List<int[]> faceN = new List<int[]>();

        private readonly string[] rowTypeN = new string[] { "# ", "o ", "v " , "vt ", "vn ", "s ", "f " };

        private readonly char[] dividerChar = new char[] { ' ' };

        private readonly char[] subBlockDividerChar = new char[] { '/' };

        private string[] rowElementN;
        private string[] rowSubElementN;

        private float[] entityElementN;
        private uint[] indexN;



        public ObjFileMessager Load(string path)
        {
            StreamReader Reader = new StreamReader(path);

            string currentRow;

            int indexOfCommand = -1;

            while (!Reader.EndOfStream)
            {
                currentRow = Reader.ReadLine();

                indexOfCommand = Array.FindIndex(rowTypeN, (e) => currentRow.StartsWith(e));

                if(indexOfCommand > -1)
                {
                    commandN[indexOfCommand](currentRow);
                }
            }

            Reader.Close();

            var ToReturn = CompileResult(Path.GetFileNameWithoutExtension(path));

            vertexRawN.Clear();
            textureCoordinateRawN.Clear();
            vertexNormalRawN.Clear();
            faceN.Clear();

            return ToReturn;

        }


        public MarkingRulezContainer LoadMarkingRulez() => Rulez;




        private void Default(string rowToAnalyze)
        {

        }


        private void AnalyzeVertexRow(string rowToAnalyze)
        {
            rowElementN = rowToAnalyze.Split(dividerChar, StringSplitOptions.RemoveEmptyEntries);

            for(int i = 1; i < 4; i++)
                vertexRawN.Add(ParseToFloat(rowElementN[i]));

        }


        private void AnalyzeTextureCoordinateRow(string rowToAnalyze)
        {
            rowElementN = rowToAnalyze.Split(dividerChar, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < 3; i++)
                textureCoordinateRawN.Add(ParseToFloat(rowElementN[i]));

        }


        private void AnalyzeVertexNormalRow(string rowToAnalyze)
        {
            rowElementN = rowToAnalyze.Split(dividerChar, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < 4; i++)
                vertexNormalRawN.Add(ParseToFloat(rowElementN[i]));

        }

        private void AnalyzeFaceRow(string rowToAnalyze)
        {
            rowElementN = rowToAnalyze.Split(dividerChar, StringSplitOptions.RemoveEmptyEntries);

            for(int i = 1; i < rowElementN.Length; i++)
            {
                rowSubElementN = rowElementN[i].Split(subBlockDividerChar);

                faceN.Add(new int[] { int.Parse(rowSubElementN[0]), int.Parse(rowSubElementN[1]), int.Parse(rowSubElementN[2]) });

            }
            


        }


        private ObjFileMessager CompileResult(string objectName)
        {
            entityElementN = new float[Rulez.CalculateLengthOfWholeArray(vertexRawN.Count / 3)];

            indexN = new uint[faceN.Count];

            var blockSize = Rulez.GetLengthOfSingleBlock();

            var vertexezSubBlockLength = Rulez.GetAmountOfElementzOfSubBlock(0);

            for (int i = 0; i < vertexRawN.Count / 3; i++)
            {
                entityElementN[i * blockSize] = vertexRawN[i * vertexezSubBlockLength];
                entityElementN[i * blockSize + 1] = vertexRawN[i * vertexezSubBlockLength + 1];
                entityElementN[i * blockSize + 2] = vertexRawN[i * vertexezSubBlockLength + 2];
            }


            int currentIndex = 0;

            int indexOfCurrentVertex;
            int indexOfCurrentTexture;
            int indexOfCurrentNormal;

            var textureCoordinateStartPosition = Rulez.GetSubblockPosition(2);

            var normalStartPosition = Rulez.GetSubblockPosition(3);

            for (int i = 0; i < faceN.Count; i++)
            {
                indexOfCurrentVertex = faceN[i][0] - 1;

                indexN[currentIndex++] = (uint)indexOfCurrentVertex;

                indexOfCurrentTexture = faceN[i][1] - 1;

                entityElementN[indexOfCurrentVertex * blockSize + textureCoordinateStartPosition] = textureCoordinateRawN[indexOfCurrentTexture * 2];
                entityElementN[indexOfCurrentVertex * blockSize + textureCoordinateStartPosition + 1] = 1 - textureCoordinateRawN[indexOfCurrentTexture * 2 + 1];

                //Код для присвоения нормалей

                indexOfCurrentNormal = faceN[i][2] - 1;

                entityElementN[indexOfCurrentVertex * blockSize + normalStartPosition] = vertexNormalRawN[indexOfCurrentNormal];
                entityElementN[indexOfCurrentVertex * blockSize + normalStartPosition + 1] = vertexNormalRawN[indexOfCurrentNormal + 1];
                entityElementN[indexOfCurrentVertex * blockSize + normalStartPosition + 2] = vertexNormalRawN[indexOfCurrentNormal + 2];

                //Конец присвоения нормалей


            }


            return new ObjFileMessager(entityElementN, indexN, blockSize, objectName);

        }


        private float ParseToFloat(string value) => float.Parse(value, CultureInfo.InvariantCulture);



        public ObjFileLoader()
        {
            var argumentN = new string[]
            {
                "AttributePosition=3",
                "AttributeColor=4",
                "AttributeTextureCoordinate=2",
                "AttibuteNormal=3"
            };


            Rulez = new MarkingRulezContainer(0, argumentN);


            commandN = new Action<string>[]
            {
                (row) => Default(row),
                (row) => Default(row),
                (row) => AnalyzeVertexRow(row),
                (row) => AnalyzeTextureCoordinateRow(row),
                (row) => AnalyzeVertexNormalRow(row),
                (row) => Default(row),
                (row) => AnalyzeFaceRow(row)
            };







        }




    }
}
