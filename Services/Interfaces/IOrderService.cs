using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Lấy orders của customer
        /// </summary>
        IEnumerable<Order> GetCustomerOrders(int customerId);

        /// <summary>
        /// Lấy order theo ID
        /// </summary>
        Order GetOrderById(int orderId);

        /// <summary>
        /// Lấy order theo OrderCode
        /// </summary>
        Order GetOrderByCode(string orderCode);

        /// <summary>
        /// Lấy order với full details
        /// </summary>
        Order GetOrderWithDetails(int orderId);

        /// <summary>
        /// Tạo order từ cart
        /// </summary>
        bool CreateOrderFromCart(
            int customerId,
            string shippingName,
            string shippingPhone,
            string shippingAddress,
            string paymentMethod,
            string couponCode,
            string note,
            out int orderId,
            out string errorMessage
        );

        /// <summary>
        /// Update order status
        /// </summary>
        bool UpdateOrderStatus(int orderId, string newStatus, out string errorMessage);

        /// <summary>
        /// Cancel order
        /// </summary>
        bool CancelOrder(int orderId, out string errorMessage);

        /// <summary>
        /// Calculate order total
        /// </summary>
        decimal CalculateOrderTotal(int customerId, string couponCode, out decimal discount);
    }
}