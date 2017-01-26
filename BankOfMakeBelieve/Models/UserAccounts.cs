using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfMakeBelieve.Models
{
    public class UserAccounts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }

        //Virtual Accessors
        public virtual User User { get; set; }
        public virtual Account Account { get; set; }
    }
}
