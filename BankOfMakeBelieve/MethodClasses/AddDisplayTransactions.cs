using BankOfMakeBelieve.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.MethodClasses
{
    class AddDisplayTransactions
    {
        /*****************************************************
        * AddTransactionRec()
        ****************************************************/
        public static void AddTransactionRec(BankContext db, User theUser, Account useAccount, double transAmt)
        {
            Transactions newRecord = new Transactions
            {
                AccountId = useAccount.Id,
                UserId = theUser.Id,
                Amount = transAmt,
                Date = DateTime.Now
            };

            db.Transactions.Add(newRecord);
            db.SaveChanges();

        }

        /*****************************************************
        * DisplayAllAcctActivity()
        ****************************************************/
        internal static void DisplayAllAcctActivity(BankContext db, User currentUser)
        {
            Account useAccount = TransValidations.AccountNum(db, currentUser, "transaction report");

            Console.Clear();
            AccountActions.DisplayWelcomeMsg(db, currentUser);

            Console.WriteLine($"Transactions for {useAccount}: \n");

            //Display column headers
            Console.WriteLine("\t\t ID \t\t DATE \t\t\t INITIATED BY \t\t TRANS AMOUNT");
            
            foreach(var transaction in db.Transactions.Where(t => t.AccountId == useAccount.Id))
            {
                Console.WriteLine(transaction);
            }

            CWLandCRL.WriteRead("\nPress ENTER to return to the Account Menu.");
            AccountActions.AccountMenu(db, currentUser);
            
        }
    }
}
