using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Services.Interfaces
{
    /// <summary>
    /// Product Service - Business logic cho sản phẩm
    /// </summary>
    public interface IProductService
    {
        // ========== READ OPERATIONS ==========

        /// <summary>
        /// Lấy tất cả products
        /// </summary>
        IEnumerable<Product> GetAllProducts();

        /// <summary>
        /// Lấy product theo ID
        /// </summary>
        Product GetProductById(int id);

        /// <summary>
        /// Lấy product với full details
        /// </summary>
        Product GetProductWithDetails(int id);

        /// <summary>
        /// Lấy products theo category
        /// </summary>
        IEnumerable<Product> GetProductsByCategory(int categoryId);

        /// <summary>
        /// Lấy products theo brand
        /// </summary>
        IEnumerable<Product> GetProductsByBrand(int brandId);

        /// <summary>
        /// Lấy products đang active
        /// </summary>
        IEnumerable<Product> GetActiveProducts();

        /// <summary>
        /// Lấy products đang sale
        /// </summary>
        IEnumerable<Product> GetOnSaleProducts();

        /// <summary>
        /// Lấy best sellers
        /// </summary>
        IEnumerable<Product> GetBestSellers(int top = 10);

        /// <summary>
        /// Lấy newest products
        /// </summary>
        IEnumerable<Product> GetNewestProducts(int top = 10);

        /// <summary>
        /// Tìm kiếm products
        /// </summary>
        IEnumerable<Product> SearchProducts(string keyword);

        /// <summary>
        /// Lấy products với pagination và filters
        /// </summary>
        IEnumerable<Product> GetProductsPaged(
            int page,
            int pageSize,
            out int totalRecords,
            out int totalPages,
            int? categoryId = null,
            int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortBy = "name",
            bool ascending = true
        );

        /// <summary>
        /// Kiểm tra product còn hàng không
        /// </summary>
        bool IsProductInStock(int productId);

        /// <summary>
        /// Kiểm tra có đủ quantity không
        /// </summary>
        bool CheckStockAvailability(int productId, int quantity);

        // ========== WRITE OPERATIONS (Admin) ==========

        /// <summary>
        /// Tạo product mới
        /// </summary>
        bool CreateProduct(Product product, out string errorMessage);

        /// <summary>
        /// Update product
        /// </summary>
        bool UpdateProduct(Product product, out string errorMessage);

        /// <summary>
        /// Xóa product (soft delete)
        /// </summary>
        bool DeleteProduct(int id, out string errorMessage);

        /// <summary>
        /// Update stock quantity
        /// </summary>
        bool UpdateStock(int productId, int quantityChange, out string errorMessage);
    }
}