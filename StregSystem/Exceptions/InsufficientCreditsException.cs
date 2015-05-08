using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException() 
            :base()
        {}

        public InsufficientCreditsException(string message)
            :base(message)
        {}
    }
}
