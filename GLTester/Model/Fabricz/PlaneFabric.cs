using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class PlaneFabric: IObjectzFabric
    {
        private MarkingRulezContainer refToRulez;

        private float width;
        private float height;
        private int horizontalCellz;
        private int verticalCellz;

        private float colorR;
        private float colorG;
        private float colorB;
        private float colorA;

        private float[] entityElementN;
        private uint[] indexN;

        private float textureWidth = 512.0f;
        private float textureHeight = 512.0f;


        public void SetMarkingRulez(MarkingRulezContainer refToRulez) => this.refToRulez = refToRulez;

        public void SetSize(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        public void SetCeiling(int horizontaCellz, int verticalCellz)
        {
            this.horizontalCellz = horizontaCellz + 1;
            this.verticalCellz = verticalCellz + 1;
        }

        public void SetColor(int R, int G, int B, int A)
        {
            colorR = R / 255.0f;
            colorG = G / 255.0f;
            colorB = B / 255.0f;
            colorA = A / 255.0f;
        }


        public GameObjectMessager Make()
        {
            entityElementN = MakeEntityArray();
            indexN = MakeIndexezArray();

            return new GameObjectMessager(entityElementN, indexN);
        }


        private float[] MakeEntityArray()
        {
            var stepX = width / (float)horizontalCellz;
            var stepY = height / (float)verticalCellz;

            var toReturn = new float[(horizontalCellz) * (verticalCellz) * refToRulez.GetLengthOfSingleBlock()];

            var edgePointX = -width / 2.0f;
            var edgePointY = -height / 2.0f;

            var currentPointX = edgePointX;
            var currentPointY = edgePointY;

            var currentIndex = 0;

            var textureXKoefficient = textureWidth / horizontalCellz;
            var textureYKoefficient = textureHeight / verticalCellz;


            for (int i = 0; i < verticalCellz; i++)
            {
                currentPointX = edgePointX;
                

                for (int j = 0; j < horizontalCellz; j++)
                {
                    currentIndex = i * horizontalCellz * refToRulez.GetLengthOfSingleBlock() + j * refToRulez.GetLengthOfSingleBlock();

                    toReturn[currentIndex++] = currentPointX;
                    toReturn[currentIndex++] = currentPointY;
                    toReturn[currentIndex++] = 0.0f;
                    toReturn[currentIndex++] = colorR;
                    toReturn[currentIndex++] = colorG;
                    toReturn[currentIndex++] = colorB;
                    toReturn[currentIndex++] = colorA;
                    toReturn[currentIndex++] = j / (float)(horizontalCellz - 1);
                    toReturn[currentIndex++] = i / (float)(verticalCellz - 1);
                    toReturn[currentIndex++] = 0;
                    toReturn[currentIndex++] = 1;
                    toReturn[currentIndex++] = 0;


                    currentPointX += stepX;                   
                }

                currentPointY += stepY;
            }

            return toReturn;
        }


        private uint[] MakeIndexezArray()
        {
            var currentIndex = 0;

            uint[] toReturn = new uint[(horizontalCellz - 1) * (verticalCellz - 1) * 6];


            for (int i = 0; i < verticalCellz - 1; i++)
            {

                for (int j = 0; j < horizontalCellz - 1; j++)
                {
                    toReturn[currentIndex++] = (uint)(i * horizontalCellz + j);
                    toReturn[currentIndex++] = (uint)(j + horizontalCellz + horizontalCellz * i);
                    toReturn[currentIndex++] = (uint)(j + horizontalCellz + horizontalCellz * i + 1);
                    toReturn[currentIndex++] = (uint)(i * horizontalCellz + j);
                    toReturn[currentIndex++] = (uint)(j + horizontalCellz + horizontalCellz * i + 1);
                    toReturn[currentIndex++] = (uint)(j + horizontalCellz * i + 1);


                }
            }

            return toReturn;
        }



    }
}
