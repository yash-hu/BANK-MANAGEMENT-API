using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Models;

namespace BANK_MANAGEMENT_API.Managers
{
    public interface IAccountsManager
    {
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccount(Decimal accountNo);
        Task<int> UpdateAccountStatus(Decimal accountNo, Boolean newStatus);
        Task<int> DeleteAccount(Decimal accountId);
        Task<Decimal> AddAccount(AddAccountDTO newAccountData);
    }
}