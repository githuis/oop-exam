using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace StregSystemProject
{
    class StregSystem
    {
        public static List<Transaction> AllTransactions = new List<Transaction>();
        public static List<Product> AllProducts;

        public StregSystem()
        {
            AllProducts = new List<Product>();
            LoadProdutcs();
        }

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

        private void LoadProdutcs()
        {
            string[] productLines = File.ReadAllLines(@"Data\products.csv", Encoding.GetEncoding("iso-8859-1"));

            foreach (string item in productLines)
            {
                string[] split = item.Split(';');

                if (split[0] == "id")
                    continue;
                //If product id is larger than zero
                if( int.Parse(split[0]) > 0)
                {
                    AllProducts.Add(new Product(int.Parse(split[0]),  split[1], int.Parse(split[2]), true, false));

                    //AllProducts.Add(new Product(int.Parse(split[0]), split[2], int.Parse(split[3]), IntToBool(int.Parse(split[4])), false));
                }
            }
        }

        private bool IntToBool(int arg)
        {
            if (arg == 0)
                return false;
            else
                return true;
        }

        private string StripString(string s)
        {
            s = s.Trim(new char[] { '"' });


            return s;
        }

    }
}
