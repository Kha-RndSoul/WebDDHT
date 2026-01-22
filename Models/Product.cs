using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public int StockQuantity { get; set; }
        public int SoldCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ProductImage> Images { get; set; }
        
        // Navigation Properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }

        
        public Product()
        {
            ProductImages = new List<ProductImage>();
            OrderDetails = new List<OrderDetail>();
            CartItems = new List<CartItem>();
            ProductReviews = new List<ProductReview>();
        }
        
        public Product(int id, string productName, string description, int categoryId,
                      int brandId, double price, double salePrice, int stockQuantity,
                      int soldCount, bool isActive)
        {
            Id = id;
            ProductName = productName;
            Description = description;
            CategoryId = categoryId;
            BrandId = brandId;
            Price = price;
            SalePrice = salePrice;
            StockQuantity = stockQuantity;
            SoldCount = soldCount;
            IsActive = isActive;
        }
        public override string ToString()
        {
            return $"Product{{Id={Id}, ProductName='{ProductName}', Description='{Description}', " +
                   $"CategoryId={CategoryId}, BrandId={BrandId}, Price={Price}, " +
                   $"SalePrice={SalePrice}, StockQuantity={StockQuantity}, SoldCount={SoldCount}, " +
                   $"IsActive={IsActive}, CreatedAt={CreatedAt}, UpdatedAt={UpdatedAt}, " +
                   $"Images={Images?.Count ?? 0}}}";
        }
    }
}