using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystem
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
            TransUser.Balance += Amount;
            LogTransaction();
        }
    }
}
