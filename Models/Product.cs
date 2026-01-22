using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDDHT.Models
{
    [Table("Products", Schema = "shop")]
    public class Product
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("product_name")]
        public string ProductName { get; set; }

        [Column("description", TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [Column("brand_id")]
        public int BrandId { get; set; }

        [Required]
        [Column("price", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Column("sale_price", TypeName = "decimal(10,2)")]
        public decimal SalePrice { get; set; }

        [Column("color")]
        [StringLength(50)]
        public string Color { get; set; }

        [Column("size")]
        [StringLength(50)]
        public string Size { get; set; }

        [Column("stock_quantity")]
        public int StockQuantity { get; set; }

        [Column("sold_count")]
        public int SoldCount { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        // Navigation Properties
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }

        // Constructors
        public Product()
        {
            ProductImages = new List<ProductImage>();
            OrderDetails = new List<OrderDetail>();
            CartItems = new List<CartItem>();
            ProductReviews = new List<ProductReview>();
            IsActive = true;
            StockQuantity = 0;
            SoldCount = 0;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Product(
            int id,
            string productName,
            string description,
            int categoryId,
            int brandId,
            decimal price,
            decimal salePrice,
            string color,
            string size,
            int stockQuantity,
            int soldCount,
            bool isActive,
            DateTime createdAt,
            DateTime updatedAt
        )
        {
            Id = id;
            ProductName = productName;
            Description = description;
            CategoryId = categoryId;
            BrandId = brandId;
            Price = price;
            SalePrice = salePrice;
            Color = color;
            Size = size;
            StockQuantity = stockQuantity;
            SoldCount = soldCount;
            IsActive = isActive;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;

            // Initialize collections
            ProductImages = new List<ProductImage>();
            OrderDetails = new List<OrderDetail>();
            CartItems = new List<CartItem>();
            ProductReviews = new List<ProductReview>();
        }

        // Methods
        public override string ToString()
        {
            return $"Product{{" +
                   $"Id={Id}, " +
                   $"ProductName='{ProductName}', " +
                   $"Description='{Description}', " +
                   $"CategoryId={CategoryId}, " +
                   $"BrandId={BrandId}, " +
                   $"Price={Price}, " +
                   $"SalePrice={SalePrice}, " +
                   $"Color='{Color}', " +
                   $"Size='{Size}', " +
                   $"StockQuantity={StockQuantity}, " +
                   $"SoldCount={SoldCount}, " +
                   $"IsActive={IsActive}, " +
                   $"CreatedAt={CreatedAt}, " +
                   $"UpdatedAt={UpdatedAt}" +
                   $"}}";
        }

        /// <summary>
        /// Tính phần trăm giảm giá
        /// </summary>
        public decimal GetDiscountPercentage()
        {
            if (Price <= 0) return 0;
            if (SalePrice >= Price) return 0;

            return Math.Round(((Price - SalePrice) / Price) * 100, 0);
        }

        /// <summary>
        /// Kiểm tra có đang giảm giá không
        /// </summary>
        public bool IsOnSale()
        {
            return SalePrice > 0 && SalePrice < Price;
        }

        /// <summary>
        /// Lấy giá hiện tại (nếu có sale thì lấy sale price, không thì lấy price)
        /// </summary>
        public decimal GetCurrentPrice()
        {
            return IsOnSale() ? SalePrice : Price;
        }

        /// <summary>
        /// Kiểm tra còn hàng không
        /// </summary>
        public bool IsInStock()
        {
            return StockQuantity > 0;
        }

        /// <summary>
        /// Kiểm tra sắp hết hàng (< 10 items)
        /// </summary>
        public bool IsLowStock()
        {
            return StockQuantity > 0 && StockQuantity < 10;
        }
    }
}