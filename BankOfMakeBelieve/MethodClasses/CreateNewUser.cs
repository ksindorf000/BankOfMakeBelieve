using BankOfMakeBelieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.MethodClasses
{
    class CreateNewUser
    {
        public string first;
        public string last;
        public string username;
        private string password;
        public DateTime dateJoined = DateTime.Now;

        /*****************************************************
         * WriteRead()
         *     Combines C.W() and C.RL()
         ****************************************************/
        static string WriteRead(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }

        /*****************************************************
         * GetVerifyInput()
         *     Gets and verifies User Data
         ****************************************************/
        public void GetVerifyInput(BankContext db)
        {
            bool usernameUnique = true;

            while (!usernameUnique)
            {
                string first = WriteRead("First Name? ");
                string last = WriteRead("Last Name? ");
                string username = WriteRead("UserName? ");
                string password = WriteRead("Password? ");

                //Check for existing username
                usernameUnique = db.User.Any(u => u.username == username);
            }

            AddNewUser(db);
        }

        /*****************************************************
         * AddNewUser()
         ****************************************************/
        private void AddNewUser(BankContext db)
        {
            User newUser = new User
            {
                FirstName = first,
                LastName = last,
                username = username,
                DateJoined = dateJoined
            };

            db.User.Add(newUser);
            db.SaveChanges();
        }
    }
}
