﻿using System;
using BankOfMakeBelieve.Models;

namespace BankOfMakeBelieve.MethodClasses
{
    class ProcTransaction
    {
        public static string wOrD;
        public static double acctBalance;
        public static Account useAccount = new Account();


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
            useAccount = TransValidations.AccountNum(db, currentUser, wOrD);
            acctBalance = useAccount.Balance;

            //Get Validate Amount
            dAmount = TransValidations.Amount(db, currentUser, wOrD, acctBalance);

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
            useAccount = TransValidations.AccountNum(db, currentUser, wOrD);
            acctBalance = useAccount.Balance;

            //Get Validate Amount
            wAmount = TransValidations.Amount(db, currentUser, wOrD, acctBalance);

            //Update Account.Balance
            useAccount.Balance += wAmount;
            db.SaveChanges();

            //Add record to Transactions
            AddDisplayTransactions.AddTransactionRec(db, currentUser, useAccount, wAmount);

            Console.Clear();
            AccountActions.AccountMenu(db, currentUser);

        }

        /*****************************************************
         * Transfer()
         *     Gets account to withdraw from
         *     Gets amount to withdraw
         *     Selects current balance
         *     Error if amount would overdraft account
         *     Gets transfUserId if external transfer
         *     Gets accountId if internal transfer
         *     Display end balance
         *     Recall account menu
         ****************************************************/
        public static void Transfer(BankContext db, User currentUser)
        {
            wOrD = "withdraw";
            double tAmount;
            string intOrExt; //Internal or External
            Account transToAccount = new Account();
            User transfUser = new User();

            //Get Account balance for withdrawal
            useAccount = TransValidations.AccountNum(db, currentUser, wOrD);
            acctBalance = useAccount.Balance;

            //Get Validate Amount for withdrawal
            tAmount = TransValidations.Amount(db, currentUser, wOrD, acctBalance);

            //Ask if transfer is internal or external
            intOrExt = Helpers.WriteRead($"\nWould you like to deposit {tAmount / -1} into another of \n" +
                "(Y)our accounts or into the account of (A)nother user?").ToUpper();

            switch (intOrExt)
            {
                case "Y":
                    transToAccount = TransValidations.AccountNum(db, currentUser, "deposit");
                    break;
                case "A":
                    transToAccount = TransValidations.TransToAcct(db); //Get acct of other User
                    transfUser = TransValidations.ReturnUser(); //Return other User
                    break;
                default:
                    Helpers.WriteRead("Sorry, that was not an option.");
                    break;
            }

            //Update Account.Balance for withdrawal account
            useAccount.Balance += tAmount;

            //Add withdrawal record to Transactions
            AddDisplayTransactions.AddTransactionRec(db, currentUser, useAccount, tAmount);

            //Update Account.Balance for deposit account
            transToAccount.Balance += tAmount /= -1;

            if (intOrExt == "Y")
            {
                //Add deposit record to current user's other account
                AddDisplayTransactions.AddTransactionRec(db, currentUser, transToAccount, tAmount);
            }
            else if (intOrExt == "A")
            {
                //Add deposit record to other user's account
                AddDisplayTransactions.AddTransactionRec(db, currentUser, transToAccount, tAmount);
            }
            db.SaveChanges();

            Console.Clear();
            AccountActions.AccountMenu(db, currentUser);

        }
    }
}
