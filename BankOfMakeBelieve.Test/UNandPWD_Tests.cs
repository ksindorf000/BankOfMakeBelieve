using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using BankOfMakeBelieve.Models;

namespace BankOfMakeBelieve.Test
{
    [TestClass]
    public class UNandPWD_Tests
    {
        /*****************************************************
         * ValidateUNandPW()
         *      Does LogIn() accurately authenticate entered
         *      username and password?
         *      
         *      NOT WORKING -- something isn't hooked up
         ****************************************************/
        [TestMethod]
        public void ValidateUNandPW()
        {
            string inputUN = "user";
            string inputPW = "pass";
            bool userFound = true;

            using (var db = new BankContext())
            {
                List<User> findUser = db.User.Where(u => u.username == inputUN && u.password == inputPW).ToList();
                userFound = findUser.Any();
            }

            Assert.AreEqual(false, userFound);
        }

        /*****************************************************
         * CheckForUniqueUsername()
         *      Does GetVerifyInput() accurately evaluate
         *      whether or not the user's desired username
         *      is already in use?
         ****************************************************/
        [TestMethod]
        public void CheckForUniqueUsername()
        {
            string username = "testusername";
            bool usernameUnique;

            using (var db = new BankContext())
            {
                usernameUnique = db.User.Any(u => u.username == username);
            }

            Assert.AreEqual(true, usernameUnique);
        }
    }
}

