using BankOfMakeBelieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.MethodClasses
{
    public class CreateNewUser //Also adds account to user
    {
        public static string first;
        public static string last;
        public static string username;
        private static string password;
        
        /*****************************************************
         * GetVerifyInput()
         *     Gets and verifies User Data
         ****************************************************/
        public static void GetVerifyInput(BankContext db)
        {
            bool notUnique = true;
            string tryAgain;

            first = Helpers.WriteRead("First Name? ");
            last = Helpers.WriteRead("Last Name? ");

            while (notUnique)
            {
                username = Helpers.WriteRead("UserName? ");

                //Check for existing username: false if match not found, true if match found
                notUnique = db.User.Any(u => u.username == username);

                if (!notUnique) { break; };

                Console.WriteLine("That username is already in use.");
                tryAgain = Helpers.WriteRead("Would you like to (L)og In or (T)ry another username?");

                if (tryAgain.ToUpper() == "L")
                {
                    Console.Clear();
                    LogIn.ValidateUser(db);
                }
            }

            password = Helpers.WriteRead("Password? ");

            AddNewUser(db);
        }

        /*****************************************************
         * AddNewUser()
         ****************************************************/
        private static void AddNewUser(BankContext db)
        {
            User newUser = new User
            {
                FirstName = first,
                LastName = last,
                username = username,
                password = password,
                DateJoined = DateTime.Now
            };

            db.User.Add(newUser);
            db.SaveChanges();

            CreateNewAccount.GetTypeAndBal(db, newUser);
            AccountActions.AccountMenu(db, newUser);
        }

    }
}
