using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BankOfMakeBelieve.Models;

namespace BankOfMakeBelieve
{
    public class BankContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<UserAccounts> UserAccounts { get; set; }
    }
}
