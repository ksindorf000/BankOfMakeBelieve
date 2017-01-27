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
        public static void AddTransactionRec(BankContext db, User currentUser, Account useAccount, double transAmt)
        {
            Transactions newRecord = new Transactions
            {
                AccountId = useAccount.Id,
                UserId = currentUser.Id,
                Amount = transAmt,
            };
        }


        /*****************************************************
        * DisplayAllAcctActivity()
        ****************************************************/

    }
}
