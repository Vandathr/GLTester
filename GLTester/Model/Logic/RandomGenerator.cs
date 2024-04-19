using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class RandomGenerator
    {
        private int seed = 0;

        private int maxValue;
        private int minValue;

        private Random RND = new Random();


        public void ChangeRandom()
        {
            RND = new Random(++seed);
        }

        public void SetMaxValue(int maxValue) => this.maxValue = maxValue;
        public void SetMinValue(int minValue) => this.minValue = minValue;

        public int GetRandom() => RND.Next(minValue, maxValue);

        public int GetRandom(int minValue, int maxValue) => RND.Next(minValue, maxValue);




        public RandomGenerator(int seed)
        {
            this.seed = seed;

            RND = new Random(seed);
        }
        


    }
}
