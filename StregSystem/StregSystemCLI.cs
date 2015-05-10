using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    sealed class StregSystemCLI : IStregSystemUI
    {
        static readonly StregSystemCLI _instance = new StregSystemCLI();
        private StregSystem _sys;

        public static StregSystemCLI CLI
        {
            get { return _instance; }
        }        

        public StregSystem Sys 
        {
            get { return _sys; }
        }

        StregSystemCLI()
        {

        }

        public void GetRef(StregSystem sy)
        {
            _sys = sy;
        }

        public void DisplayUserNotFound(string username)
        {
            Error();
            Console.WriteLine("No user found with username: [" + username +"]");
        }

        public void DisplayProductNotFound()
        {
            Error();
            Console.WriteLine("Product not found");
        }

        public void DisplayProductNotFound(int id)
        {
            Error();
            Console.WriteLine("No product with id [" + id + "] found");
        }

        public void DisplayProductInactive()
        {
            Error();
            Console.WriteLine("Attempted to buy an inactive product");
        }

        public void DisplayUserInfo(User u)
        {
            Console.WriteLine("*------------------------------");
            Console.WriteLine("* ID: " + u.UserID);
            Console.WriteLine("* Username: " + u.Username);
            Console.WriteLine("* Name: " + u.Firstname + " " + u.Lastname);
            Console.WriteLine("* E-mail: " + u.Email);
            Console.WriteLine("* Balance: " + u.Balance);
            Console.WriteLine("*------------------------------");
        }

        public void DisplayTooManyArgumentsError(string args)
        {
            Error();
            Console.WriteLine( "[" + args + "] contains too many arguments for this command");
        }

        public void DisplayAdminCommandNotFoundMessage(string args)
        {
            Error();
            Console.WriteLine("[" + args + "] is not a valid argument command");
            //TODO list valid admin commands?
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserBuysProduct(int count, Product p)
        {
            Console.WriteLine("Bought " +  count.ToString() + "x " + p.Name);            
        }

        public void Close()
        {
            
        }

        public void DisplayInsufficientCash(User u)
        {
            Error();
            Console.WriteLine("[" + u.Username + "] Not enough funds to buy product");
        }

        public void DisplayInsufficientCash(User u, int count)
        {
            Error();
            Console.WriteLine("[" + u.Username + "] Not enough funds to buy " + count.ToString() + "x product");
        }

        public void DisplayGeneralError(string msg)
        {
            Error();
            Console.WriteLine( msg);
        }

        private void Error()
        {
            Console.Write("ERROR: ");
        }
    }
}
