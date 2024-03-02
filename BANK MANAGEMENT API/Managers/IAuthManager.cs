using BANK_MANAGEMENT_API.Models;

namespace BANK_MANAGEMENT_API.Managers
{
    public interface IAuthManager
    {
        Task<int> Register(UserRegistration userRequest);
        Task<string?> Login(UserRegistration loginRequest);
    }
}