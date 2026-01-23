using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Services.Interfaces
{
    public interface ICartService
    {
        /// <summary>
        /// Lấy cart items của customer
        /// </summary>
        IEnumerable<CartItem> GetCartItems(int customerId);

        /// <summary>
        /// Thêm product vào cart
        /// </summary>
        bool AddToCart(int customerId, int productId, int quantity, out string errorMessage);

        /// <summary>
        /// Update quantity của cart item
        /// </summary>
        bool UpdateCartItemQuantity(int customerId, int productId, int quantity, out string errorMessage);

        /// <summary>
        /// Xóa item khỏi cart
        /// </summary>
        bool RemoveFromCart(int customerId, int productId, out string errorMessage);

        /// <summary>
        /// Xóa tất cả items trong cart
        /// </summary>
        bool ClearCart(int customerId, out string errorMessage);

        /// <summary>
        /// Đếm số items trong cart
        /// </summary>
        int GetCartItemCount(int customerId);

        /// <summary>
        /// Tính tổng giá trị cart
        /// </summary>
        decimal GetCartTotal(int customerId);

        /// <summary>
        /// Validate cart (check stock availability)
        /// </summary>
        bool ValidateCart(int customerId, out string errorMessage);
    }
}