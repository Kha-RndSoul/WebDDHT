using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        /// <summary>
        /// Lấy products theo category
        /// </summary>
        IEnumerable<Product> GetByCategory(int categoryId);

        /// <summary>
        /// Lấy products theo brand
        /// </summary>
        IEnumerable<Product> GetByBrand(int brandId);

        /// <summary>
        /// Lấy products đang active
        /// </summary>
        IEnumerable<Product> GetActiveProducts();

        /// <summary>
        /// Lấy products đang sale (SalePrice < Price)
        /// </summary>
        IEnumerable<Product> GetOnSaleProducts();

        /// <summary>
        /// Lấy products bán chạy nhất
        /// </summary>
        IEnumerable<Product> GetBestSellers(int top = 10);

        /// <summary>
        /// Lấy products mới nhất
        /// </summary>
        IEnumerable<Product> GetNewestProducts(int top = 10);

        /// <summary>
        /// Tìm kiếm products theo keyword (trong name, description)
        /// </summary>
        IEnumerable<Product> Search(string keyword);

        /// <summary>
        /// Lấy product với tất cả related data (Category, Brand, Images, Reviews)
        /// </summary>
        Product GetProductWithDetails(int productId);

        /// <summary>
        /// Kiểm tra còn hàng không
        /// </summary>
        bool IsInStock(int productId);

        /// <summary>
        /// Update stock quantity
        /// </summary>
        void UpdateStock(int productId, int quantityChange);

        /// <summary>
        /// Lấy products với pagination và filter
        /// </summary>
        IEnumerable<Product> GetProductsPaged(
            int page,
            int pageSize,
            out int totalRecords,
            int? categoryId = null,
            int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortBy = "name",
            bool ascending = true
        );
    }
}