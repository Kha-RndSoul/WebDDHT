using System.Collections.Generic;
using WebDDHT.Models;
using WebDDHT.Repositories;
using WebDDHT.Services.Interfaces;

namespace WebDDHT.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.Categories.GetAll();
        }

        public IEnumerable<Category> GetActiveCategories()
        {
            return _unitOfWork.Categories.GetActiveCategories();
        }

        public Category GetCategoryById(int id)
        {
            return _unitOfWork.Categories.GetById(id);
        }

        public Category GetCategoryWithProducts(int id)
        {
            return _unitOfWork.Categories.GetCategoryWithProducts(id);
        }

        public int CountProducts(int categoryId)
        {
            return _unitOfWork.Categories.CountProducts(categoryId);
        }
    }
}