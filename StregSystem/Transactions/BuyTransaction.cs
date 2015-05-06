using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
{
    class BuyTransaction : Transaction
    {
        private Product _product;

        public Product TransProduct
        {
            get { return _product; }
        }
        public BuyTransaction(int id, User user, DateTime date, Product item) 
            : base (id, user, date, item.Price)
        {
            _product = item;
        }

        public override string ToString()
        {
            return TransUser.Firstname + " købte " + TransProduct + " for " + Amount.ToString() + " d. " + Date.Date;
        }

        public override void Execute()
        {
            if (TransProduct.Active)
            {
                if (TransProduct.CanBeBoughtOnCredit)
                    TransUser.Balance -= TransProduct.Price;
                else if (TransUser.Balance >= TransProduct.Price)
                    TransUser.Balance -= TransProduct.Price;

            }
            else
                throw new ProductInactiveException("Product is not active, so it cannot be bought");
                
        }
    }
}
