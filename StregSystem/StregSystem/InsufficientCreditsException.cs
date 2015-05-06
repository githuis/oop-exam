using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException() 
            : base("Insufficient Funds, no information found")
        {
            
        }

        public InsufficientCreditsException(User u, Product p)
            : base ("Insufficient funds, user " + u.Username + "'s balance is: " + u.Balance + ". Product price is: " + p.Price)
        {
            
        }
    }
}
