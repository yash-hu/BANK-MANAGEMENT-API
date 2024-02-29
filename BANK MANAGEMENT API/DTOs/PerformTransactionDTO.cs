namespace BANK_MANAGEMENT_API.DTOs
{
    public class PerformTransactionDTO
    {
        public decimal? AccountNo { get; set; }
        public bool TransactionType { get; set; }
        public int TransactionAmount { get; set; }
    }
}
