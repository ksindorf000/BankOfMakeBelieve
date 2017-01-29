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
        public DateTime Date { get; set; }
        
        //Virtual Accessors
        public virtual User User { get; set; }
        public virtual Account Account { get; set; }

        public override string ToString()
        {
            return $"\t\t {Id} \t\t {Date.ToShortDateString()} \t\t {User.FirstName} {User.LastName} \t\t {Amount}";
        }

    }
}
