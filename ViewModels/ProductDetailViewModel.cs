using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.ViewModels
{
    /// <summary>
    /// ViewModel cho trang chi tiết sản phẩm
    /// </summary>
    public class ProductDetailViewModel
    {
        // Product info
        public Product Product { get; set; }

        // Related products (same category)
        public IEnumerable<Product> RelatedProducts { get; set; }

        // Reviews
        public IEnumerable<ProductReview> Reviews { get; set; }
        public int TotalReviews { get; set; }
        public double AverageRating { get; set; }

        // Stock status
        public bool IsInStock => Product?.StockQuantity > 0;
        public bool IsLowStock => Product?.StockQuantity > 0 && Product?.StockQuantity < 10;

        // Sale info
        public bool IsOnSale => Product?.SalePrice > 0 && Product?.SalePrice < Product?.Price;
        public decimal DiscountPercentage
        {
            get
            {
                if (Product == null || Product.Price <= 0 || !IsOnSale)
                    return 0;
                return ((Product.Price - Product.SalePrice) / Product.Price) * 100;
            }
        }

        public ProductDetailViewModel()
        {
            RelatedProducts = new List<Product>();
            Reviews = new List<ProductReview>();
        }
    }
}