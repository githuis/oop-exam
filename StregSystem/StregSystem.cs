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
        public List<Transaction> AllTransactions = new List<Transaction>();
        public List<Product> AllProducts;
        public List<User> AllUsers;

        public StregSystem()
        {
            AllProducts = new List<Product>();
            LoadProdutcs();
        }

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(NewTransactionID(), user, DateTime.Now, product);
            ExecuteTransaction(transaction);
        }

        public void AddCreditsToUser(User user, double amount)
        {
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

                using (StreamWriter w = File.AppendText(@"Data\Log.txt"))
                    transaction.LogTransaction(transaction.ToString(), w);
                    
            }
            catch (ArgumentException e)
            {
                //TODO send to communication class
                Console.WriteLine(e.Message);
            }
            catch (InsufficientCreditsException e)
            {
                //TODO send to communication class
                Console.WriteLine(e.Message);
            }
            catch (ProductInactiveException e)
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
                    try
                    {
                        AllProducts.Add(new Product(int.Parse(split[0]), split[1], int.Parse(split[2]), IntToBool(int.Parse(split[3])), false));
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        //TODO Send to comm class
                        Console.WriteLine(e.Message);
                    }
                    catch ( ArgumentNullException e)
                    {
                        //TODO send to comm class
                        Console.WriteLine(e.Message);
                    }
                    
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

        public Product GetProduct(int id)
        {
            foreach (Product p in AllProducts)
            {
                if (p.ProductID == id)
                    return p;
            }

            throw new ProductNotFoundException("Product with id " + id + " not found");
        }

        public User GetUser(string username)
        {
            foreach (User u in AllUsers)
            {
                if (u.Username == username)
                    return u;
            }
            throw new UserNotFoundException("No user found with username: " + username);
        }



    }
}
