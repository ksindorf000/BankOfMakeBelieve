using BankOfMakeBelieve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOfMakeBelieve.MethodClasses;

namespace BankOfMakeBelieve
{
    class Program
    {
        /*****************************************************
         * Main()
         ****************************************************/
        public static void Main(string[] args)
        {
            LogIn.CreateOrLogin();
        }
    }
}
