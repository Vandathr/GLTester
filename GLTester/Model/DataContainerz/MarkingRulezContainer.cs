using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class MarkingRulezContainer
    {
        private int currentSubBlock = -1;

        private readonly int[][] ruleN;

        private readonly string[] subBlockNameN;

        private readonly int lengthOfSingleBlock;



        public string GetNextSubBlockName() => subBlockNameN[currentSubBlock];
        public int GetNextAmountOfElementzOfSubblock() => ruleN[currentSubBlock][amountOfElementzOfSubblockIndex];
        public int GetNextTypeOfElement() => ruleN[currentSubBlock][typeOfElementIndex];
        public int GetNextSubBlockLengthWithTypeLength() => ruleN[currentSubBlock][subBlockLengthIndex] * sizeof(float);
        
        public int GetNextSubBlockOffsetWithTypeLength() => ruleN[currentSubBlock][3] * sizeof(float);


        public bool HasNext() 
        {
            if (currentSubBlock < ruleN.Length - 1) 
            {
                currentSubBlock++;
                return true;
            }

            currentSubBlock = -1;
            return false;
        }


        public int GetLengthOfSingleBlock() => lengthOfSingleBlock;



        public int CalculateLengthOfWholeArray(int amountOfSubblockz)
        {
            var lengthOfWholeArray = 0;

            for (int i = 0; i < ruleN.Length; i++)
            {
                lengthOfWholeArray += ruleN[i][amountOfElementzOfSubblockIndex] * amountOfSubblockz;
            }

            return lengthOfWholeArray;
        }



        public int GetAmountOfElementzOfSubBlock(int indexOfSubBlock) => ruleN[indexOfSubBlock][amountOfElementzOfSubblockIndex];


        public int GetAmountOfSubBlockz() => subBlockNameN.Length;

        


        public int GetSubBlockLength(int indexOfSubBlock) => ruleN[indexOfSubBlock][subBlockLengthIndex];

        public int GetSubBlockOffset(int indexOfSubBlock) => ruleN[indexOfSubBlock][subBlockOffsetIndex];



        public int GetOffset(int indexOfBlock) => ruleN[indexOfBlock][subBlockOffsetIndex];




        public int GetSubblockPosition(int indexOfSubBlock)
        {
            var subBlockEndPosition = 0;

            for (int i = 0; i < indexOfSubBlock; i++)
            {
                subBlockEndPosition += ruleN[i][amountOfElementzOfSubblockIndex];
            }

            return subBlockEndPosition;
        }



        public MarkingRulezContainer(int elementType, string[] elementArgumentN)
        {
            ruleN = new int[elementArgumentN.Length][];
            subBlockNameN = new string[elementArgumentN.Length];

            var divider = '=';

            var currentOffset = 0;

            string[] assignerN;

            int value;

            var isDataCorrect = false;

            for (int i = 0; i < elementArgumentN.Length; i++)
            {
                assignerN = elementArgumentN[i].Split(divider);

                if (assignerN.Length != 2) throw new Exception($"Ошибка оформления данных. В строке {elementArgumentN[i]} неправильное количество разделителей {divider}.");

                subBlockNameN[i] = assignerN[0];

                isDataCorrect = int.TryParse(assignerN[1], out value);

                if(!isDataCorrect) throw new Exception($"Ошибка оформления данных. В строке {elementArgumentN[i]} элемент {assignerN[1]} - не число.");

                ruleN[i] = new int[4];

                ruleN[i][amountOfElementzOfSubblockIndex] = value;
                ruleN[i][typeOfElementIndex] = elementType;

                ruleN[i][subBlockOffsetIndex] = currentOffset;

                currentOffset += value;
            }

            lengthOfSingleBlock = currentOffset;

            for (int i = 0; i < ruleN.Length; i++)
            {
                ruleN[i][subBlockLengthIndex] = lengthOfSingleBlock;
            }


        }

        public static readonly int amountOfElementzOfSubblockIndex = 0;
        public static readonly int typeOfElementIndex = 1;
        public static readonly int subBlockLengthIndex = 2;
        public static readonly int subBlockOffsetIndex = 3;


    }
}
