using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebDDHT.Models;

namespace WebDDHT.ViewModels
{
    /// <summary>
    /// ViewModel cho trang checkout
    /// </summary>
    public class CheckoutViewModel
    {
        // Cart info
        public IEnumerable<CartItem> CartItems { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        // Shipping info
        [Required(ErrorMessage = "Họ tên người nhận không được để trống")]
        [StringLength(100)]
        public string ShippingName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(20)]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại không hợp lệ (10 số, bắt đầu bằng 0)")]
        public string ShippingPhone { get; set; }

        [Required(ErrorMessage = "Địa chỉ giao hàng không được để trống")]
        [StringLength(500)]
        public string ShippingAddress { get; set; }

        // Payment method
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        // Coupon
        public string CouponCode { get; set; }

        // Note
        [StringLength(500)]
        public string Note { get; set; }

        // Customer info (pre-filled from logged-in customer)
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }

        public CheckoutViewModel()
        {
            CartItems = new List<CartItem>();
            PaymentMethod = "COD"; // Default
        }
    }
}