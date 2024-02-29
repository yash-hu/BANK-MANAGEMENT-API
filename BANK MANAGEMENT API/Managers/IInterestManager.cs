using BANK_MANAGEMENT_API.Models;

namespace BANK_MANAGEMENT_API.Managers
{
    public interface IInterestManager
    {
        Task<List<Interest>> getAllInterest();
    }
}