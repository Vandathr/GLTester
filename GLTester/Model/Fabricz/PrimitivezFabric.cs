using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GLTester
{
    internal class PrimitivezFabric
    {
        private readonly IObjectzFabric[] fabricN = new IObjectzFabric[] { new DefaultFabric(), new PlaneFabric() };
        private readonly Action<PrimitiveMessager>[] actionN;



        public GameObjectMessager Make(PrimitiveMessager argumentz)
        {
            int commandIndex;

            commandIndex = argumentz[0];

            actionN[commandIndex](argumentz);

            var toReturn = fabricN[commandIndex].Make();
            
            return toReturn;
        }


        public void SetMarkingRulez(MarkingRulezContainer refToRulez) => ((PlaneFabric)fabricN[1]).SetMarkingRulez(refToRulez);



        public PrimitivezFabric()
        {

            actionN = new Action<PrimitiveMessager>[]
            {
                (argumentz) => DoNothing(argumentz),
                (argumentz) => SetPlaneArgumentz(argumentz)
            };
        }


        private void DoNothing(PrimitiveMessager argumentN)
        {

        }



        private void SetPlaneArgumentz(PrimitiveMessager argumentz)
        {
            var refToFabric = (PlaneFabric)fabricN[argumentz[0]];

            refToFabric.SetSize(argumentz[1], argumentz[2]);
            refToFabric.SetCeiling(argumentz[3], argumentz[4]);
            refToFabric.SetColor(argumentz[5], argumentz[6], argumentz[7], argumentz[8]);
        }





    }


    




}
