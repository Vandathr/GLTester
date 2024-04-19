using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GLTester
{
    internal class Program
    {
        private static readonly Model MainModel = new Model();
        private static readonly VisualControl MainVisualControl = new VisualControl(MainModel);

        //private static readonly OpenTK.GameWindow Game = new OpenTK.GameWindow();

        static void Main(string[] args)
        {
            MainVisualControl.RunGame();  
        }


        public Program()
        {
            

        }


    }
}
