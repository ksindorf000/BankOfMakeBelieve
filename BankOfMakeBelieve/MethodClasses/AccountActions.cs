using BankOfMakeBelieve.Models;
using System;
using System.Linq;

namespace BankOfMakeBelieve.MethodClasses
{
    class AccountActions
    {
        public static string input;
        
        /*****************************************************
         * AccountMenu()
         ****************************************************/
        public static void AccountMenu(BankContext db, User currentUser)
        {
            DisplayWelcomeMsg(db, currentUser);
            
            input = Helpers.WriteRead("How can we help you today?: \n" +
                            "Make a (D)eposit \n" +
                            "Make a (W)ithdrawal \n" +
                            "Create (N)ew Account \n" +
                            "Make a (T)ransfer \n" +
                            //"Add New (U)ser To Your Accounts \n" +
                            //"Change (P)assword \n" +
                            "See all (A)ctivity for an Account \n" +
                            "(L)og Out \n\n");

            ProcessSelection(db, currentUser, input);
        }

        /*****************************************************
        * DisplayWelcomeMsg()
        *   Personalized welcome
        *   Displays accounts with balances
        ****************************************************/
        public static void DisplayWelcomeMsg(BankContext db, User currentUser)
        {
            DisplayBankName.Banner();

            Console.WriteLine
                ($"HELLO, {currentUser.FirstName.ToUpper()}! \n\n");

            Console.WriteLine("Available Accounts:");

            /* Get Accounts owned by user
            * 
            * SELECT a.Type, a.Id, a.Balance
            * FROM Account a
            * INNER JOIN UserAccounts ua ON ua.AccountId = a.Id
            * INNER JOIN User u ON u.Id = ua.UserId
            * WHERE u.Id = {{ currentUser.Id }}
            */

            foreach (var acct in currentUser.userAccounts)
            {
                Console.WriteLine(acct.Account);
            }

            Console.WriteLine("\n\n");
        }

        /*****************************************************
        * ProcessSelection()
        *   Validates input and calls appropriate method
        ****************************************************/
        private static void ProcessSelection(BankContext db, User currentUser, string input)
        {
            bool invalidInput = true;

            while (invalidInput)
            {
                switch (input.ToUpper())
                {
                    case "D": //Deposit
                        ProcTransaction.Deposit(db, currentUser);
                        invalidInput = false;
                        break;
                    case "W": //Withdraw
                        ProcTransaction.Withdraw(db, currentUser);
                        invalidInput = false;
                        break;
                    case "N": //New Account
                        CreateNewAccount.GetTypeAndBal(db, currentUser);
                        invalidInput = false;
                        break;
                    case "T": //Transfer
                        ProcTransaction.Transfer(db, currentUser);
                        invalidInput = false;
                        break;
                    //case "U": //New User
                    //    CreateNewUser.AddToExistingAcct(db, currentUser);
                    //    invalidInput = false;
                    //    break;
                    case "A": //All Activity
                        AddDisplayTransactions.DisplayAllAcctActivity(db, currentUser);
                        invalidInput = false;
                        break;
                    case "L":
                        invalidInput = false;
                        Console.Clear();
                        break;
                    default:
                        Helpers.WriteRead("Sorry, you must choose one of the options above.");
                        AccountMenu(db, currentUser);
                        break;
                }
            }
        }
    }
}
