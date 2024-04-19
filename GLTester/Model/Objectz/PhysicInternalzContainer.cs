using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class PhysicInternalzContainer
    {
        private readonly float shineDamper;
        private readonly float reflectivity;

        public float GetShineDamper() => shineDamper;
        public float GettReflectivity() => reflectivity;


        public PhysicInternalzContainer(float shineDamper, float reflectivity)
        {
            this.shineDamper = shineDamper;
            this.reflectivity = reflectivity;
        }
    }
}
