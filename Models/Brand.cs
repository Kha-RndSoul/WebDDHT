using System;

namespace Shop.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Brand()
        {
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