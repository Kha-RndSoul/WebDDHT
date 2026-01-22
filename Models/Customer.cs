using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDDHT.Models
{
    [Table("Customers", Schema = "shop")]
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(20)]
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
    }
}