using System.Web;
using WebDDHT.Models;

namespace WebDDHT.Helpers
{
    /// <summary>
    /// Helper for managing session data
    /// </summary>
    public static class SessionHelper
    {
        private const string CUSTOMER_KEY = "CurrentCustomer";
        private const string CART_COUNT_KEY = "CartItemCount";

        /// <summary>
        /// Lưu customer vào session
        /// </summary>
        public static void SetCustomer(Customer customer)
        {
            if (customer != null)
            {
                HttpContext.Current.Session[CUSTOMER_KEY] = customer;
            }
        }

        /// <summary>
        /// Lấy customer từ session
        /// </summary>
        public static Customer GetCustomer()
        {
            return HttpContext.Current.Session[CUSTOMER_KEY] as Customer;
        }

        /// <summary>
        /// Lấy CustomerId từ session
        /// </summary>
        public static int? GetCustomerId()
        {
            var customer = GetCustomer();
            return customer?.Id;
        }

        /// <summary>
        /// Kiểm tra đã login chưa
        /// </summary>
        public static bool IsLoggedIn()
        {
            return GetCustomer() != null;
        }

        /// <summary>
        /// Clear customer session (logout)
        /// </summary>
        public static void ClearCustomer()
        {
            HttpContext.Current.Session.Remove(CUSTOMER_KEY);
            HttpContext.Current.Session.Remove(CART_COUNT_KEY);
        }

        /// <summary>
        /// Lưu cart item count vào session
        /// </summary>
        public static void SetCartItemCount(int count)
        {
            HttpContext.Current.Session[CART_COUNT_KEY] = count;
        }

        /// <summary>
        /// Lấy cart item count từ session
        /// </summary>
        public static int GetCartItemCount()
        {
            return (int?)HttpContext.Current.Session[CART_COUNT_KEY] ?? 0;
        }
    }
}