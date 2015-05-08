using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class StregSystemCLI : IStregSystemUI
    {
        private StregSystem _sys;

        public StregSystem Sys 
        {
            get { return _sys; }
            set { _sys = value; }
        }

        public StregSystemCLI(StregSystem system)
        {
            _sys = system;
            //Print menu
            //Recieve quick command
        }

        public static void PrintError(string msg)
        {
            Console.WriteLine("ERROR: " + msg);
        }
    }
}
