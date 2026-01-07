using System;

namespace Shop.Model
{
	public class Order
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public string OrderCode { get; set; }
		public string OrderStatus { get; set; }
		public string PaymentMethod { get; set; }
		public string PaymentStatus { get; set; }
		public decimal TotalAmount { get; set; }
		public string ShippingName { get; set; }
		public string ShippingPhone { get; set; }
		public string ShippingAddress { get; set; }
		public string Note { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Order()
		{
		}

		public Order(
			int customerId,
			string orderCode,
			string orderStatus,
			string paymentMethod,
			string paymentStatus,
			decimal totalAmount,
			string shippingName,
			string shippingPhone,
			string shippingAddress,
			string note
		)
		{
			CustomerId = customerId;
			OrderCode = orderCode;
			OrderStatus = orderStatus;
			PaymentMethod = paymentMethod;
			PaymentStatus = paymentStatus;
			TotalAmount = totalAmount;
			ShippingName = shippingName;
			ShippingPhone = shippingPhone;
			ShippingAddress = shippingAddress;
			Note = note;
		}

		public override string ToString()
		{
			return $"Order{{" +
				   $"Id={Id}, " +
				   $"CustomerId={CustomerId}, " +
				   $"OrderCode='{OrderCode}', " +
				   $"OrderStatus='{OrderStatus}', " +
				   $"PaymentMethod='{PaymentMethod}', " +
				   $"PaymentStatus='{PaymentStatus}', " +
				   $"TotalAmount={TotalAmount}, " +
				   $"ShippingName='{ShippingName}', " +
				   $"ShippingPhone='{ShippingPhone}', " +
				   $"ShippingAddress='{ShippingAddress}', " +
				   $"Note='{Note}', " +
				   $"CreatedAt={CreatedAt}, " +
				   $"UpdatedAt={UpdatedAt}" +
				   $"}}";
		}
	}
}
