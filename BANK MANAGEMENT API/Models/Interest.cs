using System;
using System.Collections.Generic;

namespace BANK_MANAGEMENT_API.Models
{
    public partial class Interest
    {
        public Interest()
        {
            //Accounts = new HashSet<Account>();
        }

        public int AccountType { get; set; }
        public double InterestRate { get; set; }

        //public virtual ICollection<Account> Accounts { get; set; }
    }
}
