using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDDHT.Models
{
    [Table("Categories", Schema = "shop")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        // ⭐ THÊM IsActive
        public bool IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
            IsActive = true; // Default
        }

        public Category(int id, string categoryName, string imageUrl)
        {
            Id = id;
            CategoryName = categoryName;
            ImageUrl = imageUrl;
            IsActive = true;
        }

        public Category(int id, string categoryName, string imageUrl, DateTime createdAt)
        {
            Id = id;
            CategoryName = categoryName;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
            IsActive = true;
        }

        public override string ToString()
        {
            return $"Category{{Id={Id}, CategoryName='{CategoryName}', " +
                   $"ImageUrl='{ImageUrl}', IsActive={IsActive}, CreatedAt={CreatedAt}}}";
        }
    }
}