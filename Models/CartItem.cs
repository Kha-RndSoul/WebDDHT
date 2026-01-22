using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDDHT.Models
{
	public class CartItem
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public DateTime? AddedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
        // Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }

        public CartItem()
		{
		}

		public CartItem(int customerId, int productId, int quantity)
		{
			CustomerId = customerId;
			ProductId = productId;
			Quantity = quantity;
		}

		public override string ToString()
		{
			return $"CartItem{{" +
				   $"Id={Id}, " +
				   $"CustomerId={CustomerId}, " +
				   $"ProductId={ProductId}, " +
				   $"Quantity={Quantity}, " +
				   $"AddedAt={AddedAt}, " +
				   $"UpdatedAt={UpdatedAt}" +
				   $"}}";
		}
	}
}
