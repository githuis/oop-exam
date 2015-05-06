using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
{
    class Program
    {
        static void Main(string[] args)
        {

            User per = new User("Per", "Hansen", "-zh_-e.x@l-i_-ve.dk");
            User userTwo = new User("Jens", "Pedersen", "eksempel@mit_domain.dk", 200);


            Console.Read();
        }
    }
}
