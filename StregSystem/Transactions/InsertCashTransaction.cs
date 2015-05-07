using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class InsertCashTransaction : Transaction
    {

        public InsertCashTransaction(int id, User user, DateTime date, int amount) 
            : base (id, user, date, amount)
        {}

        public override string ToString()
        {
            return TransUser.Firstname + " satte " +  Amount.ToString() + "ind d. " + Date.Date;
        }

        public override void Execute()
        {
            if (Amount > 0)
            {
                TransUser.Balance += Amount;
            }
            else
                throw new ArgumentException("Cannot insert credits less than one credits");
        }
    }
}
