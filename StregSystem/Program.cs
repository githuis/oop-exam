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
            Console.WriteLine("Start");
            StregSystem sys = new StregSystem();
            Console.WriteLine("One");
            StregSystemCLI.CLI.GetRef(sys);
            Console.WriteLine("Two");
            
            User per = new User("Per", "yo", "-zh_-e.x@l-i_-ve.dk");
            per.Balance += 700;
            sys.AllUsers.Add(per);
            foreach (var item in sys.AllProducts)
            {
                Console.WriteLine(item.ToString());
            }

            CommandParser cmd = new CommandParser(StregSystemCLI.CLI, sys);

            

            
            User userTwo = new User("Jens", "Pedersen", "eksempel@mit._domain.dk", 200);
            Product p = new Product(1, "1L mælk", 20, true, false);

            

            
            //sys.AllProducts.ForEach(Console.WriteLine);

            Console.WriteLine("Exit");
            Console.Read();
        }
    }
}
