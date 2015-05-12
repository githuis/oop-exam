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
            User per = new User("Per", "Hans", "-zh_-e.x@l-i_-ve.dk", 700);

            CommandParser cmd = new CommandParser(cli, sys);

        }
    }
}
