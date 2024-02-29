using System;
using System.Collections.Generic;

namespace BANK_MANAGEMENT_API.Models
{
    public partial class Customer
    {
        public Customer()
        {
            //Accounts = new HashSet<Account>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string AadharNo { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = null!;

        //public virtual ICollection<Account> Accounts { get; set; }
    }
}
