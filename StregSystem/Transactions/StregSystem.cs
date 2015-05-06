using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem.Transactions
{
    class StregSystem
    {
        public void BuyProduct(User user, Product product)
        {
            //TODO Actually buy something
            BuyTransaction transaction = new BuyTransaction(10, user, DateTime.Now, product);
        }
    }
}
