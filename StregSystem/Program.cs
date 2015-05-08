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
            StregSystem system = new StregSystem();

            User per = new User("Per", "Hansen", "-zh_-e.x@l-i_-ve.dk");
            User userTwo = new User("Jens", "Pedersen", "eksempel@mit._domain.dk", 200);
            Product p = new Product(1, "1L mælk", 20, true, false);

            system.AddCreditsToUser(per, 500);
            system.BuyProduct(per, p);
            system.AddCreditsToUser(per, -10);
            

            Console.WriteLine("");

            
            system.AllProducts.ForEach(Console.WriteLine);


            Console.Read();
        }
    }
}
