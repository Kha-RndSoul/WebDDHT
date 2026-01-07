using System;

namespace Shop.Models
{
	/// <summary>
	/// Model class for customers table
	/// Represents a customer in the system
	/// </summary>
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

		// Constructors

		public Customer()
		{
		}

		/// <summary>
		/// Constructor for new customer registration (without ID)
		/// </summary>
		public Customer(string email, string passwordHash, string fullName, string phone, string address)
		{
			Email = email;
			PasswordHash = passwordHash;
			FullName = fullName;
			Phone = phone;
			Address = address;
		}

		/// <summary>
		/// Full constructor with all fields
		/// </summary>
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