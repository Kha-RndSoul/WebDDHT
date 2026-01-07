using System;

namespace Shop.Model
{
	public class OrderDetail
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal SubTotal { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

	
		public OrderDetail()
		{
		}

	
		public OrderDetail(int orderId, int productId, int quantity, decimal unitPrice)
		{
			OrderId = orderId;
			ProductId = productId;
			Quantity = quantity;
			UnitPrice = unitPrice;
			SubTotal = unitPrice * quantity;
		}

		public override string ToString()
		{
			return $"OrderDetail{{" +
				   $"Id={Id}, " +
				   $"OrderId={OrderId}, " +
				   $"ProductId={ProductId}, " +
				   $"Quantity={Quantity}, " +
				   $"UnitPrice={UnitPrice}, " +
				   $"SubTotal={SubTotal}, " +
				   $"CreatedAt={CreatedAt}, " +
				   $"UpdatedAt={UpdatedAt}" +
				   $"}}";
		}
	}
}
