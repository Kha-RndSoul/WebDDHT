using System;

namespace Shop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Category()
        {
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