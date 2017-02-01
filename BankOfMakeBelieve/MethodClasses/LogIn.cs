using BankOfMakeBelieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankOfMakeBelieve.MethodClasses
{
    class LogIn
    {
        /*****************************************************
         * CreateOrLogin()
         ****************************************************/
        public static void CreateOrLogin()
        {
            string newOrOldUser = "";
            bool valid = false;

            using (var db = new BankContext())
            {
                while (!valid)
                {
                    DisplayBankName.Banner();
                    newOrOldUser = Helpers.WriteRead("(L)og In or (C)reate New User: ").ToUpper();

                    switch (newOrOldUser)
                    {
                        case "L":
                            ValidateUser(db);
                            break;
                        case "C":
                            CreateNewUser.GetVerifyInput(db); //Threw static error
                            Console.Clear();
                            valid = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }


        /**********************************************************
         * ValidateUser()
         *      Attempts to find user
         *      Errors if no username && password combo is found
         *      Calls AccountMenu() if successful match
         **********************************************************/
        public static void ValidateUser(BankContext db)
        {
            string inputUN;
            string inputPW;
            bool userNotFound = true;

            while (userNotFound)
            {
                DisplayBankName.Banner();

                inputUN = Helpers.WriteRead("Username: ");
                inputPW = Helpers.WriteRead("Password: ");

                //Get User
                List<User> findUser = db.User.Where(u => u.username == inputUN && u.password == inputPW).ToList();

                if (!findUser.Any())
                {
                    Helpers.WriteRead("Invalid Username or Password");
                    Console.Clear();
                    CreateOrLogin();
                }
                else
                {
                    var currentUser = db.User.First(u => u.username == inputUN && u.password == inputPW);
                    AccountActions.AccountMenu(db, currentUser);
                    userNotFound = false;
                }
            }
        }
    }
}
