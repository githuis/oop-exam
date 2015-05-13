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
            AddAdminCommands(ref _adminCommands);

            SysLoadProducts();
            Start();
        }

        private void SysLoadProducts()
        {
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
        }

        private void ParseCommand(string command)
        {
            string[] split = command.Split(' ');
            User user = null;
            Product product;
            int numOfProducts = 1;
            if (CheckEmpty(command))
            {
                UI.DisplayGeneralError("No command entered");
                return;
            }

            if (command == "?")
            {
                ((StregSystemCLI)UI).DisplayHelp();
                return;
            }
                
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

                        //String extension to check for split length
                        if (command.ContainsAmount())
                        {
                            if(CheckEmpty(split[1]) || CheckEmpty(split[2]))
                            {
                                UI.DisplayTooManyArgumentsError(command);
                                return;
                            }

                            product = Sys.GetProduct(int.Parse(split[2]));
                            numOfProducts = int.Parse(split[1]);
                        }
                        else
                            product = Sys.GetProduct(int.Parse(split[1]));

                        if (user.Balance >= numOfProducts * product.Price && !product.CanBeBoughtOnCredit)
                            for (int i = 0; i < numOfProducts; i++)
                                Sys.BuyProduct(user, product);
                        else if (product.CanBeBoughtOnCredit)
                            for (int i = 0; i < numOfProducts; i++)
                                Sys.BuyProduct(user, product);
                        else
                        {
                            UI.DisplayInsufficientCash(user);
                            return;
                        }

                        UIBuyOneOrMoreProducts(user, product, numOfProducts); 
                        
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
                    catch (ProductInactiveException)
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
                    catch (OverflowException e)
                    {
                        UI.DisplayGeneralError(e.Message);
                    }
                }
            }

            else //Admin commands
            {
                string comm, strArg1 = null;
                int arg1 = 0, arg2 = 0;
                bool firstArgIsInt = false;

                comm = split[0];
                SelectFirstArg(split, ref strArg1, ref arg1, ref firstArgIsInt);
                    
                if (split.Length == 3)
                    int.TryParse(split[2], out arg2);
                else if (split.Length >= 4)
                {
                    UI.DisplayTooManyArgumentsError(command);
                    return;
                }

                RunAdminCommand(command, comm, strArg1, arg1, arg2, firstArgIsInt);
                
            }
        }

        private void RunAdminCommand(string command, string comm, string strArg1, int arg1, int arg2, bool firstArgIsInt)
        {
            try
            {
                if (firstArgIsInt)
                    _adminCommands[comm](arg1, arg2);
                else
                    _adminCommands[comm](strArg1, arg2);
            }
            catch (KeyNotFoundException)
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

        private void UIBuyOneOrMoreProducts(User user, Product product, int numOfProducts)
        {
            if (numOfProducts == 1)
                UI.DisplayUserBuysProduct((BuyTransaction)Sys.GetLastestTransacion());
            else if (numOfProducts > 1)
                UI.DisplayUserBuysProduct(numOfProducts, product, user);
        }

        private void SelectFirstArg(string[] split, ref string strArg1, ref int arg1, ref bool firstArgIsInt)
        {
            if (split.Length > 1)
            {

                if (int.TryParse(split[1], out arg1) && !Sys.UserExists(split[1]))
                    firstArgIsInt = true;
                else
                    strArg1 = split[1];
            }
        }

        private void Start()
        {
            UI.DisplayReadyForCommand();
            ParseCommand(Console.ReadLine());

            //Wait for a keypress to continue
            ((StregSystemCLI)UI).DisplayEnterToCont();
            Console.ReadKey();


            Start();
        }

        private bool CheckEmpty(string str)
        {
            if (str == "" || str == " " || str == "  " || str == null)
            {
                return true;
            }

            return false;
        }

        private void AddAdminCommands(ref Dictionary<string, Action<dynamic, dynamic>> dict)
        {
            dict.Add(":q", (x, y) => { UI.Close(); });
            dict.Add(":quit", (x, y) => { UI.Close(); });
            dict.Add(":activate", (x, y) => { Sys.ChangeProductActive(x, true); ((StregSystemCLI)UI).DisplayActivation(x, true); });
            dict.Add(":deactivate", (x, y) => { Sys.ChangeProductActive(x, false); ((StregSystemCLI)UI).DisplayActivation(x, false); });
            dict.Add(":crediton", (x, y) => { Sys.ChangeProductCredit(x, true); ((StregSystemCLI)UI).DisplayCreditChange(x, false); });
            dict.Add(":creditoff", (x, y) => { Sys.ChangeProductCredit(x, false); ((StregSystemCLI)UI).DisplayCreditChange(x, false); });
            dict.Add(":addcredits", (x, y) => { Sys.AddCreditsToUser(Sys.GetUser(x), y); UI.DisplayAddedCreditsToUser(Sys.GetUser(x), y); });
        }
    }
}
