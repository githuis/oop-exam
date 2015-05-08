using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class TransactionNotFoundException : Exception
    {
        public TransactionNotFoundException()
            :base()
        {}

        public TransactionNotFoundException(string message)
            :base(message)
        {}
    }
}
