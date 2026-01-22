using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDDHT.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }

        public Category(int id, string categoryName, string imageUrl)
        {
            Id = id;
            CategoryName = categoryName;
            ImageUrl = imageUrl;
        }
        public Category(int id, string categoryName, string imageUrl, DateTime createdAt)
        {
            Id = id;
            CategoryName = categoryName;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
        }
        public override string ToString()
        {
            return $"Category{{Id={Id}, CategoryName='{CategoryName}', " +
                   $"ImageUrl='{ImageUrl}', CreatedAt={CreatedAt}}}";
        }
    }
}