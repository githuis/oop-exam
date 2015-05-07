using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class StregSystem
    {
        public static List<Transaction> AllTransactions = new List<Transaction>();

        public void BuyProduct(User user, Product product)
        {
            //TODO Actually buy something
            BuyTransaction transaction = new BuyTransaction(NewTransactionID(), user, DateTime.Now, product);
            ExecuteTransaction(transaction);
        }

        public void AddCreditsToUser(User user, int amount)
        {
            //TODO set id properly
            InsertCashTransaction transaction = new InsertCashTransaction(NewTransactionID(), user, DateTime.Now, amount);
            ExecuteTransaction(transaction);
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            try
            {
                transaction.Execute();
                //TODO Send to communication class
                Console.WriteLine(transaction.ToString());

                AllTransactions.Add(transaction);
            }
            catch (ArgumentException e)
            {
                //TODO send to communication class
                Console.WriteLine(e.Message);
            }
        }

        private int NewTransactionID()
        {
            return AllTransactions.Count();
        }
    }
}
