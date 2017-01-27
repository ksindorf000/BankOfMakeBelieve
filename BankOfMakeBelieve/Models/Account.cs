using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.Models
{
    public class Account
    {
        public int Id { get; set; }
        public double Balance { get; set; }
        public DateTime DateOpened { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Type} - {Id}";
        }
    }
}
