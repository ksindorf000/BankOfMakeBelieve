using System;
using BankOfMakeBelieve.Models;

namespace BankOfMakeBelieve.MethodClasses
{
    public class CreateNewAccount
    {

        private static string accType;
        private static double startBal;

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
         * GetTypeAndBal()
         *      Gets Account Type from user
         *      Gets starting Balance from user
         *      Calls AddAccount()
         ****************************************************/
        public static void GetTypeAndBal(BankContext db, User newUser)
        {
            bool invalidType = true;

            Console.Clear();

            Console.WriteLine($"------- Welcome to the Bank of Make Believe, {newUser.FirstName}! -------");
            accType = WriteRead("What type of account would you like to create? \n" +
                "(C)hecking or (S)aving: ");

            while (invalidType)
            {
                switch (accType.ToUpper())
                {
                    case "C":
                        accType = "Checking";
                        invalidType = false;
                        break;
                    case "S":
                        accType = "Savings";
                        invalidType = false;
                        break;
                    default:
                        invalidType = true;
                        accType = WriteRead("Sorry, you must choose (C)hecking or (S)avings: ");
                        break;
                }
            }

            bool validBal = false;

            while (!validBal)
            {
                validBal = double.TryParse(WriteRead($"How much would you like to deposit into " +
                    "your {accType} account? (100.00) "), out startBal);
                Console.Clear();
            }
            
            AddNewAccount(db, newUser);
        }

        /*****************************************************
         * AddNewAccount()
         *      Adds Account record
         *      Adds UserAccounts record
         ****************************************************/
        private static void AddNewAccount(BankContext db, User newUser)
        {
            //Creates new Account record
            Account newAccount = new Account
            {
                Balance = startBal,
                Type = accType,
                DateOpened = DateTime.Now
            };

            //Creates new Account record
            UserAccounts newUserAcc = new UserAccounts
            {
                User = newUser,
                Account = newAccount,
            };

            db.Account.Add(newAccount);
            db.UserAccounts.Add(newUserAcc);
            db.SaveChanges();
        }
    }
}
