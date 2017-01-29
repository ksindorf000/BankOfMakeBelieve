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
         * WriteRead()
         *     Combines C.W() and C.RL()
         ****************************************************/
        private static string WriteRead(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }

        /*****************************************************
        * AddTransactionRec()
        ****************************************************/
        public static void AddTransactionRec(BankContext db, User currentUser, Account useAccount, double transAmt)
        {
            Transactions newRecord = new Transactions
            {
                AccountId = useAccount.Id,
                UserId = currentUser.Id,
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
            Account useAccount = ProcTransaction.GetValidateAcctNum(db, currentUser, "transaction report");

            Console.Clear();
            AccountActions.DisplayWelcomeMsg(db, currentUser);

            Console.WriteLine($"Transactions for {useAccount}: \n");

            //Display column headers
            Console.WriteLine("\t\t ID \t\t DATE \t\t\t INITIATED BY \t\t TRANS AMOUNT");
            
            foreach(var transaction in db.Transactions.Where(t => t.AccountId == useAccount.Id))
            {
                Console.WriteLine(transaction);
            }

            WriteRead("\nPress ENTER to return to the Account Menu.");
            AccountActions.AccountMenu(db, currentUser);


        //Lines 54-
        //http://stackoverflow.com/questions/18929483/unable-to-create-a-constant-value-of-type-only-primitive-types-or-enumeration-ty

        }
    }
}
