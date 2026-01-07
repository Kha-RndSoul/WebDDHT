using System;

namespace Shop.Model
{
	public class PaymentTransaction
	{
		public long TransactionId { get; set; }
		public long OrderId { get; set; }
		public string PaymentMethod { get; set; }
		public string TransactionCode { get; set; }
		public decimal Amount { get; set; }
		public string Status { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public PaymentTransaction()
		{
		}


		public PaymentTransaction(
			long transactionId,
			long orderId,
			string paymentMethod,
			string transactionCode,
			decimal amount,
			string status,
			DateTime? createdAt,
			DateTime? updatedAt
		)
		{
			TransactionId = transactionId;
			OrderId = orderId;
			PaymentMethod = paymentMethod;
			TransactionCode = transactionCode;
			Amount = amount;
			Status = status;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public override string ToString()
		{
			return $"PaymentTransaction{{" +
				   $"TransactionId={TransactionId}, " +
				   $"OrderId={OrderId}, " +
				   $"PaymentMethod='{PaymentMethod}', " +
				   $"TransactionCode='{TransactionCode}', " +
				   $"Amount={Amount}, " +
				   $"Status='{Status}', " +
				   $"CreatedAt={CreatedAt}, " +
				   $"UpdatedAt={UpdatedAt}" +
				   $"}}";
		}
	}
}
