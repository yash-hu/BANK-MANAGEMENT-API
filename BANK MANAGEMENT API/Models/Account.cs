using System;
using System.Collections.Generic;

namespace BANK_MANAGEMENT_API.Models
{
    public partial class Account
    {
        public Account()
        {
            //Transactions = new HashSet<Transaction>();
        }

        public decimal AccountNo { get; set; }
        public int? CustomerId { get; set; }
        public int AccountType { get; set; }
        public int Balance { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? AccountStatus { get; set; }

        //public virtual Interest AccountTypeNavigation { get; set; } = null!;
        //public virtual Customer? Customer { get; set; }
        //public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
