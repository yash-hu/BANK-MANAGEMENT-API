using AutoMapper;
using BANK_MANAGEMENT_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT_API.Managers
{
    public class InterestManager : IInterestManager
    {
        private readonly BankManagementContext _context;

        public InterestManager(BankManagementContext bankManagementContext)
        {
            _context = bankManagementContext;
        }

        public async Task<List<Interest>> getAllInterest()
        {
            if (_context.Interests == null)
            {
                return null;
            }

            var InterestsList = await _context.Interests.ToListAsync();
            return InterestsList;
        }
    }
}
