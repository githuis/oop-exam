using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class InsertCashTransaction : Transaction
    {

        public InsertCashTransaction(int id, User user, DateTime date, double amount) 
            : base (id, user, date, amount)
        {}

        public override string ToString()
        {
            return "[" + TransUser.Username + "] added " + Amount.ToString() + "kr d." + Date.ToShortDateString() + " T-ID: " + TransactionID;
        }

        public override void Execute()
        {
            if (Amount > 0)
            {
                TransUser.Balance += Amount;
            }
            else
                throw new ArgumentException("Cannot insert credits zero or less money");
        }
    }
}
