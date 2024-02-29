using AutoMapper;
using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Reflection;

namespace BANK_MANAGEMENT_API.Managers
{
    public class TransactionsManager : ITransactionsManager
    {
        private readonly BankManagementContext _context;
        private readonly IMapper _mapper;
        public TransactionsManager(BankManagementContext bankManagementContext, IMapper mapper)
        {
            _context = bankManagementContext;
            _mapper = mapper;
        }

        public async Task<List<Transaction>> GetAllTransactions()
        {
            if (_context.Transactions == null)
            {
                return null;
            }
            var transactionsList = await _context.Transactions.ToListAsync();
            return transactionsList;
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            if (_context.Transactions == null)
            {
                return null;
            }

            var transaction = await _context.Transactions.FindAsync(transactionId);
            return transaction;
        }

        public async Task<List<Transaction>> GetTransactionsByAccountNo(Decimal accountNo)
        {
            if(_context.Transactions == null)
            {
                return null;
            }
             var transactions = await _context.Transactions.Where(transaction => transaction.AccountNo == accountNo).ToListAsync();

            return transactions;
        }

        public async Task<int> PerformTransaction(PerformTransactionDTO transactionData)
        {
            try
            {
                SqlParameter status= new SqlParameter("@STATUS",SqlDbType.Int,3);
                status.Direction = ParameterDirection.Output;

                SqlParameter accountNo = new SqlParameter("@ACCOUNT_NO", transactionData.AccountNo);
                SqlParameter transactinType = new SqlParameter("@TRANSACTION_TYPE",transactionData.TransactionType);
                SqlParameter amount = new SqlParameter("@AMOUNT", transactionData.TransactionAmount);

                await _context.Database.ExecuteSqlRawAsync(
                        $"EXEC PERFORMTRANSACTION @ACCOUNT_NO,@TRANSACTION_TYPE,@AMOUNT,@STATUS OUTPUT",
                       accountNo,
                       transactinType,
                       amount,
                       status
                    );

                return (int)status.Value;
            }
            catch (Exception ex)
            {
                return 3;
            }
        }
    }
}
