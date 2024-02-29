using System;
using System.Collections.Generic;

namespace BANK_MANAGEMENT_API.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public decimal? AccountNo { get; set; }
        public DateTime? TransactionTime { get; set; }
        public bool TransactionType { get; set; }
        public int TransactionAmount { get; set; }
        public int AvailableBalance { get; set; }

        //public virtual Account? AccountNoNavigation { get; set; }
    }
}
