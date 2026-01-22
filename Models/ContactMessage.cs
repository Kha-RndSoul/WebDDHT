using System;

namespace Shop.Models
{
	
	public class ContactMessage
	{
		// Properties matching database columns
		public int Id { get; set; }
		public int CustomerId { get; set; } // Nullable - guest can send message
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }
		public string Status { get; set; } // NEW, READ, REPLIED
		public string AdminReply { get; set; }
		public string IpAddress { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? RepliedAt { get; set; }
        
		// Navigation Properties
        public virtual Customer Customer { get; set; }

        public ContactMessage()
		{
		}

		public ContactMessage(int customerId, string fullName, string email, string phone,
							 string subject, string message, string ipAddress)
		{
			CustomerId = customerId;
			FullName = fullName;
			Email = email;
			Phone = phone;
			Subject = subject;
			Message = message;
			IpAddress = ipAddress;
			Status = "NEW"; 
		}

		
		public ContactMessage(int id, int customerId, string fullName, string email,
							 string phone, string subject, string message, string status,
							 string adminReply, string ipAddress, DateTime createdAt, DateTime? repliedAt)
		{
			Id = id;
			CustomerId = customerId;
			FullName = fullName;
			Email = email;
			Phone = phone;
			Subject = subject;
			Message = message;
			Status = status;
			AdminReply = adminReply;
			IpAddress = ipAddress;
			CreatedAt = createdAt;
			RepliedAt = repliedAt;
		}

		public override string ToString()
		{
			return $"ContactMessage{{Id={Id}, CustomerId={CustomerId}, FullName='{FullName}', " +
				   $"Email='{Email}', Phone='{Phone}', Subject='{Subject}', Status='{Status}', " +
				   $"IpAddress='{IpAddress}', CreatedAt={CreatedAt}, RepliedAt={RepliedAt}}}";
		}
	}
}