using WebDDHT.Models;

namespace WebDDHT.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Register customer mới
        /// </summary>
        bool Register(string email, string password, string fullName, string phone, string address, out string errorMessage);

        /// <summary>
        /// Login customer
        /// </summary>
        Customer Login(string email, string password, out string errorMessage);

        /// <summary>
        /// Validate email format
        /// </summary>
        bool IsValidEmail(string email);

        /// <summary>
        /// Validate password strength
        /// </summary>
        bool IsValidPassword(string password, out string errorMessage);

        /// <summary>
        /// Check email already exists
        /// </summary>
        bool EmailExists(string email);
    }
}