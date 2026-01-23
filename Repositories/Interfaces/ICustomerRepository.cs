using WebDDHT.Models;

namespace WebDDHT.Repositories.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        /// <summary>
        /// Lấy customer theo email
        /// </summary>
        Customer GetByEmail(string email);

        /// <summary>
        /// Kiểm tra email đã tồn tại chưa
        /// </summary>
        bool EmailExists(string email);

        /// <summary>
        /// Validate credentials (email + password)
        /// </summary>
        Customer ValidateCredentials(string email, string passwordHash);

        /// <summary>
        /// Lấy customer với orders
        /// </summary>
        Customer GetCustomerWithOrders(int customerId);
    }
}