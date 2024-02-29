using BANK_MANAGEMENT_API.DTOs;
using BANK_MANAGEMENT_API.Models;

namespace BANK_MANAGEMENT_API.Managers
{
    public interface ICustomersManager
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomer(string aadharNo);
        Task<int> UpdateCustomer(int customerId, CustomerUpdateDTO updatedCustomer);
        Task<int> DeleteCustomer(int customerId);
        Task<List<AccountsListDTO>> GetAccountsByCustomerId(int customerId);
        Task<List<Customer>> GetCustomersByName(string name);
        Task<Customer> GetCustomerByAccountNo(Decimal accountNo);
    }
}