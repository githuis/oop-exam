using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class ProductInactiveException : Exception
    {
        public ProductInactiveException()
            :base()
        {}

        public ProductInactiveException(string message)
            :base(message)
        {}
    }
}
