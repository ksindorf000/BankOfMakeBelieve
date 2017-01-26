using BankOfMakeBelieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve
{
    class Program
    {
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
         * Main()
         ****************************************************/
        static void Main(string[] args)
        {
            CreateOrLogin();

        }

        /*****************************************************
         * CreateOrLogin()
         ****************************************************/
        private static void CreateOrLogin()
        {
            string newOrOldUser;
            bool valid = false;

            using (var db = new BankContext())
            {
                while (!valid)
                {
                    newOrOldUser = WriteRead("(L)og In or (C)reate New User: ").ToUpper();

                    switch (newOrOldUser)
                    {
                        case "L":
                            LogIn(db);
                            break;
                        case "C":
                            CreateNewUser(db);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /*****************************************************
         * LogIn()
         ****************************************************/
        static void LogIn(BankContext db)
        {
            string inputUN;
            string inputPW;
            bool userNotFound = true;

            while (userNotFound)
            {
                inputUN = WriteRead("Username: ");
                inputPW = WriteRead("Password: ");

                //Get User
                List<User> findUser = db.User.Where(u => u.username == inputUN && u.password == inputPW).ToList();

                if (!findUser.Any())
                {
                    WriteRead("Invalid Username or Password");
                    Console.Clear();
                    CreateOrLogin();
                }
                else
                {
                    Console.WriteLine("You've been logged in! Press ENTER to view account menu.");
                    //AccountMenu();
                }
            }

        }

    }
}
