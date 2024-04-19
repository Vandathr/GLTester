using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLTester
{
    internal class DefaultRowAnalyzeLogic
    {
        private readonly char[] assignerDividerN = new char[] { '=' }; 


        public string[] Analyze(string row) => row.Split(assignerDividerN, 2);
        


    }
}
