using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class WorldSettingzRowAnalyzeLogic
    {
        private readonly Dictionary<string, Action> commandK;


        private readonly char[] assignerDividerN = new char[] { '=' };


        public List<ObjectSettingzMessager> Analyze(StreamReader Reader) 
        {
            string[] elementN;
            string[] assignerN;

            List<ObjectSettingzMessager> toReturn = new List<ObjectSettingzMessager>();

            var currentRow = "";

            while (!Reader.EndOfStream)
            {
                currentRow = Reader.ReadLine();

                if (currentRow.StartsWith("#")) continue;

                elementN = currentRow.Split(';');

                var assignerK = new Dictionary<string, string>();

                for (int i = 0; i < elementN.Length; i++)
                {
                    assignerN = elementN[i].Split(assignerDividerN, 2);

                    if(assignerN[0] != "")
                        assignerK.Add(assignerN[0], assignerN[1]);

                }
                toReturn.Add(new ObjectSettingzMessager(assignerK));
            }

            return toReturn;
        }



        public WorldSettingzRowAnalyzeLogic()
        {
            commandK = new Dictionary<string, Action>
            {
                { "usingCommand", () => { } }
            };


        }





    }
}
