using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDDHT.Models
{
	public class Admin
	{
		// Properties matching database columns
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string FullName { get; set; }
		public string Role { get; set; } // SUPER_ADMIN, ADMIN, STAFF
		public bool IsActive { get; set; }
		public DateTime CreatedAt { get; set; }

		// Constructors

		public Admin()
		{
		}

		/// <summary>
		/// Constructor for creating new admin (without ID)
		/// </summary>
		public Admin(string username, string email, string passwordHash, string fullName, string role)
		{
			Username = username;
			Email = email;
			PasswordHash = passwordHash;
			FullName = fullName;
			Role = role;
			IsActive = true; // Default active
		}

		/// <summary>
		/// Full constructor with all fields
		/// </summary>
		public Admin(int id, string username, string email, string passwordHash,
					 string fullName, string role, bool isActive, DateTime createdAt)
		{
			Id = id;
			Username = username;
			Email = email;
			PasswordHash = passwordHash;
			FullName = fullName;
			Role = role;
			IsActive = isActive;
			CreatedAt = createdAt;
		}

		public override string ToString()
		{
			return $"Admin{{Id={Id}, Username='{Username}', Email='{Email}', " +
				   $"FullName='{FullName}', Role='{Role}', IsActive={IsActive}, " +
				   $"CreatedAt={CreatedAt}}}";
		}
	}
}