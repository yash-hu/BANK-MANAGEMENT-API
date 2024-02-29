using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Models;

namespace BANK_MANAGEMENT_API.Managers
{
    public interface ITransactionsManager
    {
        Task<List<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int transactionId);
        Task<List<Transaction>> GetTransactionsByAccountNo(Decimal accountNo);
        Task<int> PerformTransaction(PerformTransactionDTO transactionData);
    }
}