using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal struct Color
    {
        private readonly float R;
        private readonly float G;
        private readonly float B;
        private readonly float A;

        public float GetR() => R;
        public float GetG() => G;
        public float GetB() => B;
        public float GetA() => A;


        public Color(int R, int G, int B, float A)
        {
            this.R = R / 255.0f;
            this.G = G / 255.0f;
            this.B = B / 255.0f;
            this.A = A / 255.0f;
        }

        public Color(float R, float G, float B, float A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }


    }
}
