using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfMakeBelieve.MethodClasses;
using BankOfMakeBelieve.Models;

namespace BankOfMakeBelieve.MethodClasses
{
    class TransValidations
    {
        public static Account useAccount = new Account();
        public static double acctBalance;
        public static User transfUser = new User();

        /*****************************************************
         * AccountNum()
         *      Gets and validates account number
         ****************************************************/
        internal static Account AccountNum(BankContext db, User currentUser, string wOrD) //Hehe, "wOrD" -- get it?
        {
            bool validAcctNum = false;
            int wchAcct = 0;

            //Get and validate account number
            while (!validAcctNum)
            {
                if (wOrD != "transfer")
                {
                    Console.Clear();
                    AccountActions.DisplayWelcomeMsg(db, currentUser);
                }

                validAcctNum = int.TryParse(CWLandCRL.WriteRead($"In which account would you like to create a {wOrD}? (Acct #): "),
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
         * Amount()
         *     If input = (C)ancel, kick back to AccountMenu()
         *     Else TryParse input and return amount
         ****************************************************/
        internal static double Amount(BankContext db, User currentUser, string wOrD, double acctBalance)
        {
            string userInput;
            double amount = 0;
            bool validAmnt = false;

            while (!validAmnt)
            {
                Console.Clear();
                AccountActions.DisplayWelcomeMsg(db, currentUser);

                userInput = CWLandCRL.WriteRead($"How much would you like to {wOrD}? \n " +
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
                        CWLandCRL.WriteRead($"Sorry, your account will not support a ${amount} withdrawal.");
                        validAmnt = false;
                    }
                }
            }

            return amount;
        }

        /*****************************************************
         * TransToAcct()
         *     Get UserId of user to transfer to
         *     Get Account to transfer to
         *     Return Account
         ****************************************************/
        internal static Account TransToAcct(BankContext db)
        {
            bool valid = false;
            int transfUserId;
            Account transAcct = new Account();

            //Validate transfUserId as int and as existing user
            while (!valid)
            {
                valid = int.TryParse(CWLandCRL.WriteRead("\nTo which user would you like to transfer funds? (UserId): "), out transfUserId);

                //Check for existing username
                if (valid && db.User.Any(u => u.Id == transfUserId))
                {
                    valid = true;

                    //Get User
                    transfUser = db.User.First(u => u.Id == transfUserId);

                    //Display user's accounts
                    Console.WriteLine($"\n{transfUser.FirstName}'s available accounts:");
                    foreach (var acct in transfUser.userAccounts)
                    {
                        Console.WriteLine(acct.Account.Type + acct.Account.Id);
                    }
                    transAcct = AccountNum(db, transfUser, "transfer");
                }
                else
                {
                    CWLandCRL.WriteRead("Sorry, that UserId is invalid.");
                    valid = false;
                }
            }

            return transAcct;
        }

        /*****************************************************
        * ReturnUser()
        *       Returns transfUser for transfer to account
        ****************************************************/
        internal static User ReturnUser()
        {
            return transfUser;
        }

    }
}
