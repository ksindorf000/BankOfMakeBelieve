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
        private static void Withdraw(BankContext db, User currentUser)
        {

            double wAmount;
            double currentBal;
            bool validAmnt = false;

            //List<int> acctIdList = db.UserAccounts.Select(u => u.AccountId).Where(u => u.UserId == currentUser.Id).ToList();

            AccountActions.DisplayWelcomeMsg(db, currentUser);

            GetValidateAccount(db, currentUser);

            while (!validAmnt)
            {
                validAmnt = double.TryParse(WriteRead("How much would you like to withdraw? (100.00): "),
                    out wAmount);
            }

            //Get Account balance

        }

        /*****************************************************
         * GetValidateAccount()
         ****************************************************/
        private static Account GetValidateAccount(BankContext db, User currentUser)
        {
            bool validAcctNum = false;
            int wchAcct = 0;

            //Get and validate account number
            while (!validAcctNum)
            {
                validAcctNum = int.TryParse(WriteRead("Which account would you like to withdraw from? (Acct #): "),
                    out wchAcct);

                foreach (var acct in currentUser.userAccounts) //can't do .Contains() without extracting just AcctId to list?
                {
                    validAcctNum = (acct.AccountId == wchAcct) ? true : false;
                    if (validAcctNum) { break; }
                    else { Console.WriteLine("Sorry, that account number is invalid."); };
                }
            }

            //Get Account
            var useAccount =
                from Account in currentUser.userAccounts
                where Account.Id == wchAcct
                select new
                {
                    Account
                };

            //Get Account Balance
            /*
             * SELECT a.bal
             * FROM Account a
             * INNER JOIN UserAccounts ua ON ua.AccountId = a.Id
             * INNER JOIN User u ON u.Id = ua.UserId
             * WHERE a.Id = {{ wchAcct }} AND u.Id = {{ currentUser.Id }}
             */

            var acctBal =
                from Account in db.Account
                where Account.Id == useAccount.Id
                select new
                {
                    acctBal = currentUser.userAccounts
                };
        }







    }
}
