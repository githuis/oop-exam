using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StregExtensions;

namespace StregSystemProject
{
    class CommandParser
    {
        private IStregSystemUI _ui;
        private StregSystem _sys;
        private Dictionary<string, Action<dynamic, dynamic>> _adminCommands;
                
        #region Properties
        public IStregSystemUI UI 
        {
            get { return _ui; }
        }

        public StregSystem Sys
        {
            get { return _sys; }
        }
        #endregion

        public CommandParser(IStregSystemUI ui, StregSystem sys)
        {
            _ui = ui;
            _sys = sys;

            _adminCommands = new Dictionary<string, Action<dynamic, dynamic>>();
            _adminCommands.Add(":q", (x, y) => { UI.Close(); });
            _adminCommands.Add(":quit", (x, y) => { UI.Close(); });
            _adminCommands.Add(":activate", (x, y) => { Sys.ChangeProductActive(x, true); });
            _adminCommands.Add(":deactivate", (x, y) => { Sys.ChangeProductActive(x, false); });
            _adminCommands.Add(":crediton", (x, y) => { Sys.ChangeProductCredit(x, true); });
            _adminCommands.Add(":creditoff", (x, y) => { Sys.ChangeProductCredit(x, false); });
            _adminCommands.Add(":addcredits", (x, y) => { Sys.AddCreditsToUser(Sys.GetUser(x), y); UI.DisplayAddedCreditsToUser(Sys.GetUser(x), y); });

            try
            {
                Sys.LoadProdutcs();
            }
            catch (ArgumentOutOfRangeException e)
            {
                UI.DisplayGeneralError(e.Message);
            }
            catch (ArgumentNullException e)
            {
                UI.DisplayGeneralError(e.Message);
            }


            GetReadyForInput();
        }

        public void ParseCommand(string command)
        {
            string[] split = command.Split(' ');
            User user = null;
            Product product;
            int numOfProducts = 1;
            if (command == "" || command == null || split[0] == " ")
                return;
   
            if(!command.IsAdminCommand()) //User Commands
            {
                if(command.IsBuyCommand())
                {
                    try
                    {
                        user = Sys.GetUser(split[0]);

                        if (split.Length < 2)
                        {
                            UI.DisplayUserInfo(user);
                            return;
                        }                      

                        if (command.ContainsAmount())
                        {
                            if(split[1] == " " || split[2] == " ")
                                return;

                            product = Sys.GetProduct(int.Parse(split[2]));
                            numOfProducts = int.Parse(split[1]);
                        }
                        else
                            product = Sys.GetProduct(int.Parse(split[1]));

                        Console.WriteLine(product.ProductID);

                        for (int i = 0; i < numOfProducts; i++)                        
                            Sys.NewBuyTransaction(user, product);

                        if (numOfProducts == 1)
                        UI.DisplayUserBuysProduct((BuyTransaction) Sys.GetLastestTransacion());
                        else if (numOfProducts > 1)
                            UI.DisplayUserBuysProduct(numOfProducts, product, user);  
                        
                    }
                    catch (UserNotFoundException e)
                    {
                        UI.DisplayUserNotFound(e.Message);
                    }
                    catch (ProductNotFoundException e)
                    {
                        UI.DisplayProductNotFound(e.Message);
                    }    
                    catch (ArgumentOutOfRangeException e) // Probably transaction list index error
                    {
                        UI.DisplayGeneralError(e.Message);
                    }
                    catch (ArgumentException e)
                    {
                        UI.DisplayGeneralError(e.Message);
                    }
                    catch (InsufficientCreditsException e)
                    {
                        if (user != null)
                            UI.DisplayInsufficientCash(user);
                        else
                            UI.DisplayGeneralError(e.Message);
                    }
                    catch (ProductInactiveException e)
                    {
                        UI.DisplayProductInactive();
                    }                 
                    catch (FormatException e)
                    {
                        UI.DisplayGeneralError(e.Message + "\nID must be numeric");
                    }
                    catch (TransactionNotFoundException e)
                    {
                        UI.DisplayTransactionNotFound(e.Message);
                    }
                }
            }

            else //Admin commands
            {
                string comm, strArg1 = null;
                int arg1 = 0, arg2 = 0;
                bool firstArgIsInt = false;

                comm = split[0];
                if (split.Length > 1)
                {
                    if (int.TryParse(split[1], out arg1))
                        firstArgIsInt = true;
                    else
                        strArg1 = split[1];
                }
                    
                if (split.Length == 3)
                    int.TryParse(split[2], out arg2);

                try
                {
                    if (firstArgIsInt)
                        _adminCommands[split[0]](arg1, arg2);
                    else
                        _adminCommands[split[0]](strArg1, arg2);
                }
                catch (KeyNotFoundException e)
                {
                    UI.DisplayAdminCommandNotFoundMessage(command);
                }
                catch (UserNotFoundException e)
                {
                    UI.DisplayUserNotFound(e.Message);
                }
                catch (ArgumentException e)
                {
                    UI.DisplayGeneralError(e.Message);
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException e)
                {
                    UI.DisplayGeneralError(e.Message);
                }
                
            }
        }

        private void GetReadyForInput()
        {
            UI.DisplayReadyForCommand();
            ParseCommand(Console.ReadLine());
            ((StregSystemCLI)UI).DisplayEnterToCont();
            Console.ReadKey();
            GetReadyForInput();
        }

    }
}
