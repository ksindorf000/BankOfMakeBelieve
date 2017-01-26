using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public double Amount { get; set; }

        //Virtual Accessors
        public virtual User User { get; set; }
        public virtual Account Account { get; set; }
    }
}
