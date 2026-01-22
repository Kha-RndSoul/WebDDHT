using System;

namespace Shop.Models
{
	
	public class Customer
	{
		// Properties matching database columns
		public int Id { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string FullName { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
        
		// Navigation Properties 
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<ContactMessage> ContactMessages { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
            CartItems = new List<CartItem>();
            ProductReviews = new List<ProductReview>();
            ContactMessages = new List<ContactMessage>();
        }
       
        public Customer(string email, string passwordHash, string fullName, string phone, string address)
		{
			Email = email;
			PasswordHash = passwordHash;
			FullName = fullName;
			Phone = phone;
			Address = address;
		}

		
		public Customer(int id, string email, string passwordHash, string fullName,
					   string phone, string address, DateTime createdAt, DateTime updatedAt)
		{
			Id = id;
			Email = email;
			PasswordHash = passwordHash;
			FullName = fullName;
			Phone = phone;
			Address = address;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public override string ToString()
		{
			return $"Customer{{Id={Id}, Email='{Email}', FullName='{FullName}', " +
				   $"Phone='{Phone}', Address='{Address}', CreatedAt={CreatedAt}, " +
				   $"UpdatedAt={UpdatedAt}}}";
		}
	}
}