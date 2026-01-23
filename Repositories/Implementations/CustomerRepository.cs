using System.Data.Entity;
using System.Linq;
using WebDDHT.Data;
using WebDDHT.Models;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories.Implementations
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SchoolSuppliesContext context) : base(context)
        {
        }

        public Customer GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(c => c.Email == email);
        }

        public bool EmailExists(string email)
        {
            return _dbSet.Any(c => c.Email == email);
        }

        public Customer ValidateCredentials(string email, string passwordHash)
        {
            return _dbSet.FirstOrDefault(c => c.Email == email && c.PasswordHash == passwordHash);
        }

        public Customer GetCustomerWithOrders(int customerId)
        {
            return _dbSet
                .Include(c => c.Orders.Select(o => o.OrderDetails))
                .FirstOrDefault(c => c.Id == customerId);
        }
    }
}