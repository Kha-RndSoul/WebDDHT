using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartItem>
    {
        /// <summary>
        /// Lấy cart items của customer
        /// </summary>
        IEnumerable<CartItem> GetByCustomer(int customerId);

        /// <summary>
        /// Lấy cart item cụ thể (customer + product)
        /// </summary>
        CartItem GetCartItem(int customerId, int productId);

        /// <summary>
        /// Xóa tất cả cart items của customer
        /// </summary>
        void ClearCart(int customerId);

        /// <summary>
        /// Đếm số items trong cart
        /// </summary>
        int CountItems(int customerId);

        /// <summary>
        /// Tính tổng giá trị cart
        /// </summary>
        decimal GetCartTotal(int customerId);
    }
}