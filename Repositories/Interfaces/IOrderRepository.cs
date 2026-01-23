using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        /// <summary>
        /// Lấy orders của customer
        /// </summary>
        IEnumerable<Order> GetByCustomer(int customerId);

        /// <summary>
        /// Lấy order theo OrderCode
        /// </summary>
        Order GetByOrderCode(string orderCode);

        /// <summary>
        /// Lấy order với full details (OrderDetails, Products, Customer)
        /// </summary>
        Order GetOrderWithDetails(int orderId);

        /// <summary>
        /// Lấy orders theo status
        /// </summary>
        IEnumerable<Order> GetByStatus(string status);

        /// <summary>
        /// Generate unique OrderCode
        /// </summary>
        string GenerateOrderCode();
    }
}