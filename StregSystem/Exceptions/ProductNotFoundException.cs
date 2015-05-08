using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class ProductNotFoundException : Exception
    {
        public ProductNotFoundException()
            :base()
        {}

        public ProductNotFoundException(string message)
            : base (message)
        {}
    }
}
