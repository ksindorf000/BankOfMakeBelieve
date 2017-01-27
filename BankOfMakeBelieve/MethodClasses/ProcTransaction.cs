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



        }
    }
}
