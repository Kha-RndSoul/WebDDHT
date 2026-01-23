using System;
using System.Text.RegularExpressions;
using WebDDHT.Helpers;
using WebDDHT.Models;
using WebDDHT.Repositories;
using WebDDHT.Services.Interfaces;

namespace WebDDHT.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Register(string email, string password, string fullName, string phone, string address, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Validate email
                if (!IsValidEmail(email))
                {
                    errorMessage = "Email không hợp lệ.";
                    return false;
                }

                // Check email exists
                if (EmailExists(email))
                {
                    errorMessage = "Email đã được sử dụng.";
                    return false;
                }

                // Validate password
                if (!IsValidPassword(password, out errorMessage))
                {
                    return false;
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(fullName))
                {
                    errorMessage = "Họ tên không được để trống.";
                    return false;
                }

                // Hash password
                string passwordHash = PasswordHelper.HashPassword(password);

                // Create customer
                var customer = new Customer
                {
                    Email = email.Trim().ToLower(),
                    PasswordHash = passwordHash,
                    FullName = fullName.Trim(),
                    Phone = phone?.Trim(),
                    Address = address?.Trim(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _unitOfWork.Customers.Add(customer);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi đăng ký: {ex.Message}";
                return false;
            }
        }

        public Customer Login(string email, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    errorMessage = "Email và mật khẩu không được để trống.";
                    return null;
                }

                // Hash password
                string passwordHash = PasswordHelper.HashPassword(password);

                // Validate credentials
                var customer = _unitOfWork.Customers.ValidateCredentials(email.Trim().ToLower(), passwordHash);

                if (customer == null)
                {
                    errorMessage = "Email hoặc mật khẩu không đúng.";
                    return null;
                }

                return customer;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi đăng nhập: {ex.Message}";
                return null;
            }
        }

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Simple email regex
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidPassword(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Mật khẩu không được để trống.";
                return false;
            }

            if (password.Length < 6)
            {
                errorMessage = "Mật khẩu phải có ít nhất 6 ký tự.";
                return false;
            }

            // Add more validation rules if needed
            // - Must contain uppercase
            // - Must contain lowercase
            // - Must contain digit
            // - Must contain special character

            return true;
        }

        public bool EmailExists(string email)
        {
            return _unitOfWork.Customers.EmailExists(email.Trim().ToLower());
        }
    }
}