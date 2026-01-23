using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Services.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Lấy tất cả categories
        /// </summary>
        IEnumerable<Category> GetAllCategories();

        /// <summary>
        /// Lấy active categories
        /// </summary>
        IEnumerable<Category> GetActiveCategories();

        /// <summary>
        /// Lấy category theo ID
        /// </summary>
        Category GetCategoryById(int id);

        /// <summary>
        /// Lấy category với products
        /// </summary>
        Category GetCategoryWithProducts(int id);

        /// <summary>
        /// Đếm số products trong category
        /// </summary>
        int CountProducts(int categoryId);
    }
}