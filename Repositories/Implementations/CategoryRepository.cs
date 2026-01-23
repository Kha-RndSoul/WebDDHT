using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDDHT.Data;
using WebDDHT.Models;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(SchoolSuppliesContext context) : base(context)
        {
        }

        public IEnumerable<Category> GetActiveCategories()
        {
            return _dbSet
                .Where(c => c.IsActive)
                .OrderBy(c => c.CategoryName)
                .ToList();
        }

        public Category GetCategoryWithProducts(int categoryId)
        {
            return _dbSet
                .Include(c => c.Products.Select(p => p.ProductImages))
                .FirstOrDefault(c => c.Id == categoryId);
        }

        public Category GetByName(string categoryName)
        {
            return _dbSet
                .FirstOrDefault(c => c.CategoryName == categoryName);
        }

        public int CountProducts(int categoryId)
        {
            var category = _dbSet
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == categoryId);

            return category?.Products?.Count(p => p.IsActive) ?? 0;
        }
    }
}