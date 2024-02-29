using AutoMapper;
using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BANK_MANAGEMENT_API.Managers
{
    public class CustomersManager : ICustomersManager
    {
        private readonly BankManagementContext _context;
        private readonly IMapper _mapper;
        public CustomersManager(BankManagementContext bankManagementContext,IMapper mapper)
        {
            _context = bankManagementContext;
            _mapper = mapper;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            if(_context.Customers == null)
            {
                return null;
            }
            var customersList=await _context.Customers.ToListAsync();
            return customersList;
        }

        public async Task<Customer> GetCustomer(string aadharNo)
        {
            if (_context.Customers == null)
            {
                return null;
            }
            var customer = await _context.Customers.SingleOrDefaultAsync(customer => customer.AadharNo.Equals(aadharNo) );
            return customer;
        }

        public async Task<int> UpdateCustomer(int customerId,CustomerUpdateDTO updatedCustomer)
        {
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(customer=>customer.CustomerId==customerId);
                if(customer == null)
                {
                    return 1;
                }
                _mapper.Map<CustomerUpdateDTO,Customer>(updatedCustomer, customer);
                try
                {
                    await _context.SaveChangesAsync();
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());   
                    return 2;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 2;
            }
        }

        public async Task<int> DeleteCustomer(int customerId)
        {
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(customer => customer.CustomerId == customerId);
                if(customer == null)
                {
                    return 1;
                }
                _context.Customers.Remove(customer);
                int deleted=await _context.SaveChangesAsync();
                if(deleted>0)
                {
                    return 0;
                }
                return 3;
            }
            catch(Exception ex)
            {
                return 2;
            }
        }

        public async Task<List<AccountsListDTO>> GetAccountsByCustomerId(int customerId)
        {
            if(_context.Accounts == null)
            {
                return null;
            }

            var accounts = await _context.Accounts.Where(account => account.CustomerId == customerId).Select(account =>
                _mapper.Map<AccountsListDTO>(account)
            ).ToListAsync();

            return accounts;
        }

        public async Task<List<Customer>> GetCustomersByName(string name)
        {
            if (_context.Customers == null)
            {
                return null;
            }
            var customersList = await _context.Customers.Where(customer=> EF.Functions.Like(customer.FirstName,"%"+name+"%") || EF.Functions.Like(customer.MiddleName, "%" + name + "%") || EF.Functions.Like(customer.LastName, "%" + name + "%")).ToListAsync();
            return customersList;
        }

        public async Task<Customer> GetCustomerByAccountNo(Decimal accountNo)
        {
            if (_context.Accounts == null)
            {
                return null;
            }
            var account = await _context.Accounts.FindAsync(accountNo);
            if (account == null) return null;
            var customer = await _context.Customers.FindAsync(account.CustomerId);
            return customer;

        }
    }
}
