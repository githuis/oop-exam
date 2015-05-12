using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    class StregSystemCLI : IStregSystemUI
    {
        private StregSystem _sys;      

        public StregSystem Sys 
        {
            get { return _sys; }
        }

        public StregSystemCLI(StregSystem sy)
        {
            _sys = sy;
        }

        public void DisplayUserNotFound(string username)
        {
            Error();
            Console.WriteLine("No user with username [" + username +"] found");
        }

        public void DisplayProductNotFound(string id)
        {
            Error();
            Console.WriteLine("Intet produkt med id [" + id + "] found");
        }

        public void DisplayProductInactive()
        {
            Error();
            Console.WriteLine("Attempted to purchase inacctive product");
        }

        public void DisplayUserInfo(User u)
        {
            List<Transaction> t;
            Console.WriteLine("*------------------------------");
            Console.WriteLine("* ID: " + u.UserID);
            Console.WriteLine("* Username: " + u.Username);
            Console.WriteLine("* Name: " + u.Firstname + " " + u.Lastname);
            Console.WriteLine("* E-mail: " + u.Email);
            Console.WriteLine("* Balance: " + u.Balance);
            Console.WriteLine("*------------------------------");
            if(u.Balance < 50)
                DisplayBalanceBelowFifty();

            int i = 0;
            t = Sys.GetTransactionList(u);
            t = t.OrderByDescending(x => x.TransactionID).ToList();
            Console.WriteLine("Last transactions:");
            if (t.Count > 10)
                for (i = 0; i < 11; i++)                    
                    Console.WriteLine(i.ToString() + ". " + t[i].ToString());                    
            else
                foreach (var tran in t)
                    Console.WriteLine((++i).ToString() + ". " + tran.ToString());
            
        }

        public void DisplayTooManyArgumentsError(string args)
        {
            Error();
            Console.WriteLine( "[" + args + "] too many arguments for this command");
        }

        public void DisplayAdminCommandNotFoundMessage(string args)
        {
            Error();
            Console.WriteLine("[" + args + "] is not a valid admin commando");
            //TODO list valid admin commands?
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction.ToString());
            if(transaction.TransUser.Balance < 50)
                DisplayBalanceBelowFifty();
        }

        public void DisplayUserBuysProduct(int count, Product p, User u)
        {
            Console.WriteLine("[" + u.Username + "] Bought " +  count.ToString() + "x " + p.Name + "for " + (p.Price * (double) count).ToString() + "kr" );
            if (u.Balance < 50)
                DisplayBalanceBelowFifty();
        }

        public void Close()
        {
            Console.WriteLine("Shutting down");
            Console.Read();
            Environment.Exit(0);
        }

        public void DisplayInsufficientCash(User u)
        {
            Error();
            Console.WriteLine("[" + u.Username + "] Not enough credit to purchase product");
        }

        public void DisplayInsufficientCash(User u, int count)
        {
            Error();
            Console.WriteLine("[" + u.Username + "] Not enough credit to purchase " + count.ToString() + "x products");
        }

        public void DisplayGeneralError(string msg)
        {
            Error();
            Console.WriteLine( msg);
        }

        public void DisplayReadyForCommand()
        {
            System.Console.Clear();
            DisplayActiveProducts();
            Console.Write("\n>");
        }

        private void Error()
        {
            Console.Write("ERROR: ");
        }

        public void DisplayAddedCreditsToUser(User u, double amount)
        {
            Console.WriteLine("Added " + amount + "kr to user [" + u.Username + "]");
        }

        private void DisplayBalanceBelowFifty()
        {
            Console.WriteLine("Please note that your balance is below 50kr!");
        }

        private void DisplayActiveProducts()
        {
            Console.Write(string.Format("{0, -4}|{1, 6} - {2}", "ID", "Price", "Product"));
            DisplayHelpOptions();
            foreach (Product p in Sys.GetActiveProducts())
            {
                Console.WriteLine(p.ToString());
            }
        }

        public void DisplayEnterToCont()
        {
            Console.WriteLine("\nPress enter to return to menu");
        }

        public void DisplayTransactionNotFound(string username)
        {
            Console.WriteLine("No transactions found for user [" +username+"]");
        }

        private void DisplayHelpOptions()
        {
            Console.WriteLine("\t\tEnter ? for help");
        }
    }
}
