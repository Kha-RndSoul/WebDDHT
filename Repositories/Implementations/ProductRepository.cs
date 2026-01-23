using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDDHT.Data;
using WebDDHT.Models;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(SchoolSuppliesContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return _dbSet
                .Where(p => p.CategoryId == categoryId && p.IsActive)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.ProductName)
                .ToList();
        }

        public IEnumerable<Product> GetByBrand(int brandId)
        {
            return _dbSet
                .Where(p => p.BrandId == brandId && p.IsActive)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.ProductName)
                .ToList();
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            return _dbSet
                .Where(p => p.IsActive)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.ProductName)
                .ToList();
        }

        public IEnumerable<Product> GetOnSaleProducts()
        {
            return _dbSet
                .Where(p => p.IsActive && p.SalePrice > 0 && p.SalePrice < p.Price)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => (p.Price - p.SalePrice) / p.Price) // % discount DESC
                .ToList();
        }

        public IEnumerable<Product> GetBestSellers(int top = 10)
        {
            return _dbSet
                .Where(p => p.IsActive)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.SoldCount)
                .Take(top)
                .ToList();
        }

        public IEnumerable<Product> GetNewestProducts(int top = 10)
        {
            return _dbSet
                .Where(p => p.IsActive)
                .Include(p => p.ProductImages)
                .OrderByDescending(p => p.CreatedAt)
                .Take(top)
                .ToList();
        }

        public IEnumerable<Product> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Product>();

            keyword = keyword.ToLower().Trim();

            return _dbSet
                .Where(p => p.IsActive &&
                           (p.ProductName.ToLower().Contains(keyword) ||
                            p.Description.ToLower().Contains(keyword)))
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .OrderBy(p => p.ProductName)
                .ToList();
        }

        public Product GetProductWithDetails(int productId)
        {
            return _dbSet
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductReviews.Select(r => r.Customer))
                .FirstOrDefault(p => p.Id == productId);
        }

        public bool IsInStock(int productId)
        {
            var product = GetById(productId);
            return product != null && product.StockQuantity > 0;
        }

        public void UpdateStock(int productId, int quantityChange)
        {
            var product = GetById(productId);
            if (product != null)
            {
                product.StockQuantity += quantityChange;
                product.UpdatedAt = System.DateTime.Now;
                Update(product);
            }
        }

        public IEnumerable<Product> GetProductsPaged(
            int page,
            int pageSize,
            out int totalRecords,
            int? categoryId = null,
            int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortBy = "name",
            bool ascending = true)
        {
            // Start with active products
            IQueryable<Product> query = _dbSet.Where(p => p.IsActive);

            // Apply filters
            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (brandId.HasValue)
                query = query.Where(p => p.BrandId == brandId.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            // Count total before paging
            totalRecords = query.Count();

            // Include related data
            query = query
                .Include(p => p.ProductImages)
                .Include(p => p.Category)
                .Include(p => p.Brand);

            // Apply sorting
            switch (sortBy.ToLower())
            {
                case "price":
                    query = ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
                    break;
                case "newest":
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
                case "bestseller":
                    query = query.OrderByDescending(p => p.SoldCount);
                    break;
                default:  // "name"
                    query = ascending ? query.OrderBy(p => p.ProductName) : query.OrderByDescending(p => p.ProductName);
                    break;
            }

            // Apply paging
            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}