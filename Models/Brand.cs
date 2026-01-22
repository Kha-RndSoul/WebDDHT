using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDDHT.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedAt { get; set; }
        // Navigation Properties
        public virtual ICollection<Product> Products { get; set; }

        public Brand()
        {
            Products = new List<Product>();
        }

        public Brand(int id, string brandName)
        {
            Id = id;
            BrandName = brandName;
        }
        
        public Brand(int id, string brandName, DateTime createdAt)
        {
            Id = id;
            BrandName = brandName;
            CreatedAt = createdAt;
        }
        public override string ToString()
        {
            return $"Brand{{Id={Id}, BrandName='{BrandName}', CreatedAt={CreatedAt}}}";
        }
    }
}