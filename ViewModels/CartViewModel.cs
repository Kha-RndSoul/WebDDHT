using System.Collections.Generic;
using System.Linq;
using WebDDHT.Models;

namespace WebDDHT.ViewModels
{
	/// <summary>
	/// ViewModel cho trang giỏ hàng
	/// </summary>
	public class CartViewModel
	{
		public IEnumerable<CartItem> CartItems { get; set; }

		// Totals
		public decimal Subtotal { get; set; }
		public decimal Discount { get; set; }
		public decimal Total { get; set; }

		// Coupon
		public string AppliedCouponCode { get; set; }
		public bool HasCoupon => !string.IsNullOrWhiteSpace(AppliedCouponCode);

		// Item count
		public int TotalItems => CartItems?.Sum(c => c.Quantity) ?? 0;

		// Validation
		public bool IsValid { get; set; }
		public string ValidationMessage { get; set; }

		public CartViewModel()
		{
			CartItems = new List<CartItem>();
			IsValid = true;
		}
	}
}