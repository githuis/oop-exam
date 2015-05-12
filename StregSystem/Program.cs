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
            User per = new User("Per", "0", "-zh_-e.x@l-i_-ve.dk");
            per.Balance += 700;
            sys.AllUsers.Add(per);
            

            CommandParser cmd = new CommandParser(cli, sys);

            

            
            User userTwo = new User("Jens", "Pedersen", "eksempel@mit._domain.dk", 200);
            Product p = new Product(1, "1L mælk", 20, true, false);

            

           

            Console.WriteLine("Exit");
            Console.Read();
        }
    }
}
