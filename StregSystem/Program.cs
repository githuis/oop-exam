using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class Program
    {
        static void Main(string[] args)
        {
            StregSystem sys = new StregSystem();
            IStregSystemUI cli = new StregSystemCLI(sys);
            User per = new User("Daniel", "Bol", "dvanbo14@udent.aau.dk", 300);
            CommandParser cmd = new CommandParser(cli, sys);
        }
    }
}
