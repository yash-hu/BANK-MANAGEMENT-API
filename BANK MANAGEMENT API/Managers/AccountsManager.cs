using AutoMapper;
using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BANK_MANAGEMENT_API.Managers
{
    public class AccountsManager : IAccountsManager
    {
        private readonly BankManagementContext _context;
        private readonly IMapper _mapper;
        public AccountsManager(BankManagementContext bankManagementContext, IMapper mapper)
        {
            _context = bankManagementContext;
            _mapper = mapper;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            var accountsList = await _context.Accounts.ToListAsync();
            return accountsList;
        }

        public async Task<Account> GetAccount(Decimal accountNo)
        {
            if (_context.Accounts == null)
            {
                return null;
            }

            var account = await _context.Accounts.FindAsync(accountNo);
            return account;
        }

       public async Task<int> UpdateAccountStatus(Decimal accountNo,Boolean newStatus)
        {
            var account = await GetAccount(accountNo);
            if (account == null)
            {
                return 1;
            }

            if(account.AccountStatus == newStatus)
            {
                return 2;
            }
            account.AccountStatus = newStatus;
            try
            {
               int changed=await _context.SaveChangesAsync();
                if (changed == 1) return 0;
                return 3;
            }
            catch(Exception ex)
            {
                return 3;
            }
        }

        public async Task<int> DeleteAccount(Decimal accountId)
        {
            try
            {
                var account = await _context.Accounts.FindAsync(accountId);
                if (account == null)
                {
                    return 1;
                }
                _context.Accounts.Remove(account);
                int deleted = await _context.SaveChangesAsync();
                if (deleted > 0)
                {
                    return 0;
                }
                return 3;
            }
            catch (Exception ex)
            {
                return 3;
            }
        }

        public async Task<Decimal> AddAccount(AddAccountDTO newAccountData)
        {
            try
            {
                SqlParameter accountNo = new SqlParameter("@ACCOUNT_NO", SqlDbType.Decimal,0);
                accountNo.Direction = ParameterDirection.Output;

                SqlParameter firstName=new SqlParameter("@FIRSTNAME",newAccountData.FirstName);
                SqlParameter middleName=new SqlParameter("@MIDDLENAME", newAccountData.MiddleName);
                SqlParameter lastName=new SqlParameter("@LASTNAME",newAccountData.LastName);
                SqlParameter address=new SqlParameter("@ADDRESS", newAccountData.Address);
                SqlParameter dob=new SqlParameter("@DOB", newAccountData.DateOfBirth);
                SqlParameter mobile=new SqlParameter("@MOBILE", newAccountData.Phone);
                SqlParameter email=new SqlParameter("@EMAIL", newAccountData.Email);
                SqlParameter aadhar=new SqlParameter("@AADHAR", newAccountData.AadharNo);
                SqlParameter accountType=new SqlParameter("@ACCOUNT_TYPE", newAccountData.AccountType);
                SqlParameter balance=new SqlParameter("@BALANCE", newAccountData.Balance);

                await _context.Database.ExecuteSqlRawAsync(
                        $"EXEC ADDACCOUNT @FIRSTNAME,@MIDDLENAME,@LASTNAME,@ADDRESS,@DOB,@MOBILE,@EMAIL,@AADHAR,@ACCOUNT_TYPE,@BALANCE,@ACCOUNT_NO OUTPUT",
                        firstName,
                        middleName,
                        lastName,
                        address,
                        dob,
                        mobile,
                        email,
                        aadhar,
                        accountType,
                        balance,
                        accountNo
                    );

                return (Decimal)accountNo.Value;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        
    }
}
