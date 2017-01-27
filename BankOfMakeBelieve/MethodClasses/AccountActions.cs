using BankOfMakeBelieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.MethodClasses
{
    class AccountActions
    {
        /*****************************************************
         * WriteRead()
         *     Combines C.W() and C.RL()
         ****************************************************/
        private static string WriteRead(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }

        /*****************************************************
         * AccountMenu()
         ****************************************************/
        public static void AccountMenu(BankContext db, User currentUser)
        {
            string input;
            bool invalidInput = true;

            Console.Clear();

            Console.WriteLine
                ("---------------------------------------------------------\n" +
                $"\t ------- HELLO, {currentUser.FirstName.ToUpper()}! -------\n" +
                "---------------------------------------------------------\n");

            Console.WriteLine("Available Accounts:");

            /* Get Accounts owned by user
             * 
             * SELECT a.Type, a.Id, a.Balance
             * FROM Account a
             * INNER JOIN UserAccounts ua ON ua.AccountId = a.Id
             * INNER JOIN User u ON u.Id = ua.UserId
             * WHERE u.Id = {{ currentUser.Id }}
             */

            foreach (var acct in db.UserAccounts.Where(u => u.UserId == currentUser.Id).ToList())
            {
                Console.WriteLine(acct.Account);
            }

            input = WriteRead("How can we help you today?: \n" +
                            "(C)reate New Account \n" +
                            "Make a (D)eposit \n" +
                            "Make a (W)ithdrawal \n" +
                            //"Transfer Between Accounts" +
                            //"Add New User To Your Accounts" +
                            "(L)og Out \n");

            while (invalidInput)
            {
                switch (input.ToUpper())
                {
                    case "C":
                        CreateNewAccount.GetTypeAndBal(db, currentUser);
                        invalidInput = false;
                        break;
                    //case "D":
                    //    ProcTransaction.Deposit(db, currentUser);
                    //    invalidInput = false;
                    //    break;
                    //case "W":
                    //    ProcTransaction.Withdraw(db, currentUser);
                    //    invalidInput = false;
                    //    break;
                    //case "T":
                    //    ProcTransaction.Transfer(db, currentUser);
                    //    invalidInput = false;
                    //    break;
                    //case "U":
                    //    CreateNewUser.AddToExistingAcct(db);
                    //    invalidInput = false;
                    //    break;
                    case "L":
                        invalidInput = false;
                        Console.Clear();
                        break;
                    default:
                        input = WriteRead("Sorry, you must choose (C)hecking or (S)avings: ");
                        break;
                }
            }
        }
    }
}
