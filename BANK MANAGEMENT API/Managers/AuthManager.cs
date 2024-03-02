using BANK_MANAGEMENT_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace BANK_MANAGEMENT_API.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfiguration _configuration;
        private readonly BankManagementContext _context;
        public AuthManager(IConfiguration configuration, BankManagementContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<int> Register(UserRegistration userRequest)
        {
            try
            {
                var user = await _context.Users.FindAsync(userRequest.Username);
                if (user != null)
                {
                    return 1;
                }
                CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var newUser = new User();
                newUser.Username = userRequest.Username;
                newUser.PasswordHash = passwordHash;
                newUser.PasswordSalt = passwordSalt;
                _context.Add(newUser);
                await _context.SaveChangesAsync();
                return 0;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return 2;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<string?> Login( UserRegistration loginRequest)
        {
            var user = await _context.Users.FindAsync(loginRequest.Username);
            if (user==null)
            {
                return null;

            }

            if (!VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            string token = loginRequest.Username.Equals("ADMIN")? CreateToken(user,"Admin"):CreateToken(user);
            return token;
        }

        private string CreateToken(User user,string role="User")
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(-4),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
