using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StregSystemProject
{
    interface IStregSystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string id);
        void DisplayUserInfo(User u);
        void DisplayTooManyArgumentsError(string args);
        void DisplayAdminCommandNotFoundMessage(string args);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, Product p);
        void Close();
        void DisplayInsufficientCash(User u);
        void DisplayGeneralError(string errorString);
        void DisplayReadyForCommand();
        void DisplayAddedCreditsToUser(User u, double amount);
    }
}
