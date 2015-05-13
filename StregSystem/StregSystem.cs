using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace StregSystemProject
{
    class StregSystem
    {
        public List<Transaction> AllTransactions = new List<Transaction>();
        public List<Product> AllProducts;
        public List<User> AllUsers;

        public StregSystem()
        {
            AllUsers = User.All;
            AllProducts = new List<Product>();
        }

        public void BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(NewTransactionID(), user, DateTime.Now, product);
            AllTransactions.Add(transaction);
            ExecuteTransaction(transaction);
        }

        public void AddCreditsToUser(User user, double amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(NewTransactionID(), user, DateTime.Now, amount);
            AllTransactions.Add(transaction);
            ExecuteTransaction(transaction);
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();

            using (StreamWriter w = File.AppendText(@"Data\Log.txt"))
                transaction.LogTransaction(transaction.ToString(), w);
        }

        private int NewTransactionID()
        {
            return AllTransactions.Count();
        }

        public void LoadProdutcs()
        {
            //Endcoding is found on stack overflow. http://stackoverflow.com/questions/8089357/how-to-read-special-character-like-%C3%A9-%C3%A2-and-others-in-c-sharp
            string[] productLines = File.ReadAllLines( (Directory.GetCurrentDirectory() + "\\Data\\products.csv"), Encoding.GetEncoding("iso-8859-1"));

            foreach (string item in productLines)
            {
                string[] split = item.Split(';');

                if (split[0] == "id")
                    continue;
                //If product id is larger than zero
                if (int.Parse(split[0]) > 0)
                {
                    AllProducts.Add(new Product(int.Parse(split[0]), StripString(split[1]), double.Parse(split[2])/100, IntToBool(int.Parse(split[3])), false));
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
            return Regex.Replace(Regex.Replace(s, "\"", ""), "<.*?>", "");
        }

        public Product GetProduct(int id)
        {
            foreach (Product p in AllProducts)
            {
                if (p.ProductID == id)
                    return p;
            }
            throw new ProductNotFoundException(id.ToString());
        }

        public User GetUser(string username)
        {
            if (username == "" || username == null)
                throw new UserNotFoundException("");
            foreach (User u in AllUsers)
            {
                if (u.Username == username)
                    return u;
            }
            throw new UserNotFoundException(username);
        }

        public bool UserExists(string username)
        {
            if (username == "" || username == null)
                return false;

            foreach (User u in AllUsers)
            {
                if (u.Username == username)
                    return true;
            }

            return false;
        }

        public List<Transaction> GetTransactionList(User u)
        {
            List<Transaction> list = new List<Transaction>();

            foreach (Transaction t in AllTransactions)
            {
                if (u == t.TransUser)
                    list.Add(t);
            }
            if (list.Count != 0)
                return list;
            else
                throw new TransactionNotFoundException(u.Username);
        }

        public List<Transaction> GetTransactionList(string username)
        {
            List<Transaction> list = new List<Transaction>();

            foreach (Transaction t in AllTransactions)
            {
                if (username == t.TransUser.Username)
                    list.Add(t);
            }
            if (list.Count != 0)
                return list;
            else
                throw new TransactionNotFoundException(username);
        }

        public List<Transaction> GetTransactionList(User u, DateTime from, DateTime to)
        {
            List<Transaction> list = new List<Transaction>();

            foreach (Transaction t in AllTransactions)
            {
                if (u == t.TransUser && t.Date > from && t.Date < to)
                    list.Add(t);
            }
            if (list.Count != 0)
                return list;
            else
                throw new TransactionNotFoundException("No transactions found for user " + u.Username + " between "
                    + from.ToShortDateString() + " and " + to.ToShortDateString());
        }

        public List<Product> GetActiveProducts()
        {
            List<Product> actives = new List<Product>();
            foreach (Product p in AllProducts)
            {
                if (p.Active)
                    actives.Add(p);
            }
            if (actives.Count != 0)
                return actives;
            else
                throw new ProductNotFoundException("No active products found");
        }

        public Transaction GetLastestTransacion()
        {          
            return AllTransactions[AllTransactions.Count-1];
        }

        public void ChangeProductActive(int id, bool val)
        {
            GetProduct(id).Active = val;
        }

        public void ChangeProductCredit(int id, bool val)
        {
            GetProduct(id).CanBeBoughtOnCredit = val;
        }


    }
}
