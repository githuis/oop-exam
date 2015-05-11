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
            Console.WriteLine("Ingen bruger med brugernavn[" + username +"]");
        }

        public void DisplayProductNotFound(string id)
        {
            Error();
            Console.WriteLine("Intet produkt med id [" + id + "] fundet");
        }

        public void DisplayProductInactive()
        {
            Error();
            Console.WriteLine("Forsøgte at købe inaktivt produkt");
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
            Console.WriteLine( "[" + args + "] for mange argumenter til denne kommando");
        }

        public void DisplayAdminCommandNotFoundMessage(string args)
        {
            Error();
            Console.WriteLine("[" + args + "] er ikke en korrekt admin kommando");
            //TODO list valid admin commands?
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserBuysProduct(int count, Product p)
        {
            Console.WriteLine("Købte " +  count.ToString() + "x " + p.Name);            
        }

        public void Close()
        {
            Console.WriteLine("Lukker Program");
            Console.Read();
            Environment.Exit(0);
        }

        public void DisplayInsufficientCash(User u)
        {
            Error();
            Console.WriteLine("[" + u.Username + "] Ikke nok penge til at købe produktet");
        }

        public void DisplayInsufficientCash(User u, int count)
        {
            Error();
            Console.WriteLine("[" + u.Username + "] Ikke nok penge til at købe " + count.ToString() + "x produkter");
        }

        public void DisplayGeneralError(string msg)
        {
            Error();
            Console.WriteLine( msg);
        }

        public void DisplayReadyForCommand()
        {
            Console.Write(">");
        }

        private void Error()
        {
            Console.Write("FEJL: ");
        }

        public void DisplayAddedCreditsToUser(User u, double amount)
        {
            Console.WriteLine("Tilføjede " + amount + "kr til bruger [" + u.Username + "]");
        }
    }
}
