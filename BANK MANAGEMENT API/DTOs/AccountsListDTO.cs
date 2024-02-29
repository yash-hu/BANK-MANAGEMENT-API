namespace BANK_MANAGEMENT_API.DTOs
{
    public class AccountsListDTO
    {
        public decimal AccountNo { get; set; }
        public int? CustomerId { get; set; }
        public int AccountType { get; set; }
        public int Balance { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? AccountStatus { get; set; }
        //public double InterestRate { get; set; }
    }
}
