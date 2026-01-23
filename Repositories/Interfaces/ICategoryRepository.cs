using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        /// <summary>
        /// Lấy categories đang active
        /// </summary>
        IEnumerable<Category> GetActiveCategories();

        /// <summary>
        /// Lấy category với products
        /// </summary>
        Category GetCategoryWithProducts(int categoryId);

        /// <summary>
        /// Tìm category theo tên
        /// </summary>
        Category GetByName(string categoryName);

        /// <summary>
        /// Đếm số products trong category
        /// </summary>
        int CountProducts(int categoryId);
    }
}