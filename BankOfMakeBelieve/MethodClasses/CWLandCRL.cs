using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.MethodClasses
{
    class CWLandCRL
    {
        /*****************************************************
         * WriteRead()
         *     Combines C.W() and C.RL()
         ****************************************************/
        internal static string WriteRead(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }
    }
}
