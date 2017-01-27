using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfMakeBelieve.MethodClasses;
using BankOfMakeBelieve.Models;

namespace BankOfMakeBelieve.MethodClasses
{
    class ProcTransaction
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
         * Withdraw()
         *     Gets account to withdraw from
         *     Gets amount to withdraw
         *     Selects current balance
         *     Error if amount would overdraft account
         *     Display end balance
         *     Recall account menu
         ****************************************************/
        public static void Withdraw(BankContext db, User currentUser)
        {
            string userInput;
            double wAmount = 0;
            double acctBalance;
            bool validAmnt = false;

            //Get Account balance
            Account useAcct = GetValidateAccount(db, currentUser, "withdrawal");
            acctBalance = useAcct.Balance;

            while (!validAmnt)
            {
                Console.Clear();
                AccountActions.DisplayWelcomeMsg(db, currentUser);

                userInput = WriteRead("How much would you like to withdraw? \n " +
                    "(100.00) or (C)ancel: ").ToUpper();

                //If (C)ancel, break. Else, try to parse
                if (userInput == "C")
                {
                    AccountActions.AccountMenu(db, currentUser);
                    break;
                }
                else
                {
                    validAmnt = double.TryParse(userInput, out wAmount);
                }

                //If valid amount was entered and will not overdraft account
                if (validAmnt && (acctBalance - wAmount) >= 0)
                {
                    useAcct.Balance = acctBalance - wAmount;
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine($"Sorry, your account will not support a {wAmount} withdrawal.");
                    validAmnt = false;
                }
            }

            Console.Clear();
            AccountActions.AccountMenu(db, currentUser);

        }

        /*****************************************************
         * GetValidateAccount()
         ****************************************************/
        private static Account GetValidateAccount(BankContext db, User currentUser, string wOrD) //Hehe, "wOrD" -- get it?
        {
            bool validAcctNum = false;
            int wchAcct = 0;
            Account useAccount = new Account();

            //Get and validate account number
            while (!validAcctNum)
            {
                Console.Clear();
                AccountActions.DisplayWelcomeMsg(db, currentUser);

                validAcctNum = int.TryParse(WriteRead($"In which account would you like to create a {wOrD}? (Acct #): "),
                    out wchAcct);

                foreach (var acct in currentUser.userAccounts)
                {
                    validAcctNum = acct.AccountId == wchAcct;
                    if (validAcctNum)
                    {
                        useAccount = acct.Account;
                        break;
                    }
                    else { WriteRead("Sorry, that account number is invalid."); };
                }
            }
            return useAccount;

            //Get Account Balance
            /*
             * SELECT a.bal
             * FROM Account a
             * INNER JOIN UserAccounts ua ON ua.AccountId = a.Id
             * INNER JOIN User u ON u.Id = ua.UserId
             * WHERE a.Id = {{ wchAcct }} AND u.Id = {{ currentUser.Id }}
             */

        }
    }
}
