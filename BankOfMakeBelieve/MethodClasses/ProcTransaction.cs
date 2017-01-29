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
        public static string wOrD;
        public static double acctBalance;
        public static Account useAccount = new Account();

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
         * Deposit()
         *     Gets account to deposit into
         *     Gets amount to deposit
         *     Selects current balance
         *     Display end balance
         *     Recall account menu
         ****************************************************/
        public static void Deposit(BankContext db, User currentUser)
        {
            wOrD = "deposit";
            double dAmount;

            //Get Account balance
            GetValidateAcctNum(db, currentUser, wOrD);
            acctBalance = useAccount.Balance;

            //Get Validate Amount
            dAmount = ValidateAmount(db, currentUser);

            //Update Account.Balance
            useAccount.Balance += dAmount;
            db.SaveChanges();

            //Add record to Transactions
            AddDisplayTransactions.AddTransactionRec(db, currentUser, useAccount, dAmount);

            Console.Clear();
            AccountActions.AccountMenu(db, currentUser);

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
            wOrD = "withdraw";
            double wAmount;

            //Get Account balance
            GetValidateAcctNum(db, currentUser, wOrD);
            acctBalance = useAccount.Balance;

            //Get Validate Amount
            wAmount = ValidateAmount(db, currentUser);

            //Update Account.Balance
            useAccount.Balance += wAmount;
            db.SaveChanges();

            //Add record to Transactions
            AddDisplayTransactions.AddTransactionRec(db, currentUser, useAccount, wAmount);

            Console.Clear();
            AccountActions.AccountMenu(db, currentUser);

        }

        /*****************************************************
         * GetValidateAccount()
         ****************************************************/
        internal static Account GetValidateAcctNum(BankContext db, User currentUser, string wOrD) //Hehe, "wOrD" -- get it?
        {
            bool validAcctNum = false;
            int wchAcct = 0;

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
                        validAcctNum = true;
                        break;
                    }
                    else { validAcctNum = false; }
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

        /*****************************************************
         * ValidateAmount()
         *     If input = (C)ancel, kick back to AccountMenu()
         *     Else TryParse input and return amount
         ****************************************************/
        private static double ValidateAmount(BankContext db, User currentUser)
        {
            string userInput;
            double amount = 0;
            bool validAmnt = false;

            while (!validAmnt)
            {
                Console.Clear();
                AccountActions.DisplayWelcomeMsg(db, currentUser);

                userInput = WriteRead($"How much would you like to {wOrD}? \n " +
                    "(100.00) or (C)ancel: ").ToUpper();

                //If (C)ancel, break. Else, try to parse
                if (userInput == "C")
                {
                    AccountActions.AccountMenu(db, currentUser);
                    break;
                }
                else
                {
                    validAmnt = double.TryParse(userInput, out amount);
                }

                //Check for overdraft
                if (validAmnt && wOrD == "withdraw")
                {
                    if ((acctBalance - amount) >= 0)
                    {
                        amount *= -1;
                        validAmnt = true;
                    }
                    else
                    {
                        WriteRead($"Sorry, your account will not support a ${amount} withdrawal.");
                        validAmnt = false;
                    }
                }
            }

            return amount;
        }
    }
}
