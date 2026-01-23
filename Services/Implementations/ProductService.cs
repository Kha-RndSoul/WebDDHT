using System;
using System.Collections.Generic;
using System.Linq;
using WebDDHT.Models;
using WebDDHT.Repositories;
using WebDDHT.Services.Interfaces;

namespace WebDDHT.Services.Implementations
{
    /// <summary>
    /// Product Service Implementation
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // ========== READ OPERATIONS ==========

        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.Products.GetAll();
        }

        public Product GetProductById(int id)
        {
            return _unitOfWork.Products.GetById(id);
        }

        public Product GetProductWithDetails(int id)
        {
            return _unitOfWork.Products.GetProductWithDetails(id);
        }

        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            return _unitOfWork.Products.GetByCategory(categoryId);
        }

        public IEnumerable<Product> GetProductsByBrand(int brandId)
        {
            return _unitOfWork.Products.GetByBrand(brandId);
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            return _unitOfWork.Products.GetActiveProducts();
        }

        public IEnumerable<Product> GetOnSaleProducts()
        {
            return _unitOfWork.Products.GetOnSaleProducts();
        }

        public IEnumerable<Product> GetBestSellers(int top = 10)
        {
            return _unitOfWork.Products.GetBestSellers(top);
        }

        public IEnumerable<Product> GetNewestProducts(int top = 10)
        {
            return _unitOfWork.Products.GetNewestProducts(top);
        }

        public IEnumerable<Product> SearchProducts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Product>();

            return _unitOfWork.Products.Search(keyword);
        }

        public IEnumerable<Product> GetProductsPaged(
            int page,
            int pageSize,
            out int totalRecords,
            out int totalPages,
            int? categoryId = null,
            int? brandId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string sortBy = "name",
            bool ascending = true)
        {
            var products = _unitOfWork.Products.GetProductsPaged(
                page,
                pageSize,
                out totalRecords,
                categoryId,
                brandId,
                minPrice,
                maxPrice,
                sortBy,
                ascending
            );

            // Calculate total pages
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return products;
        }

        public bool IsProductInStock(int productId)
        {
            return _unitOfWork.Products.IsInStock(productId);
        }

        public bool CheckStockAvailability(int productId, int quantity)
        {
            var product = _unitOfWork.Products.GetById(productId);

            if (product == null)
                return false;

            return product.StockQuantity >= quantity;
        }

        // ========== WRITE OPERATIONS ==========

        public bool CreateProduct(Product product, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Validate
                if (!ValidateProduct(product, out errorMessage))
                    return false;

                // Set timestamps
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;

                // Add to repository
                _unitOfWork.Products.Add(product);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi tạo sản phẩm: {ex.Message}";
                return false;
            }
        }

        public bool UpdateProduct(Product product, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Validate
                if (!ValidateProduct(product, out errorMessage))
                    return false;

                // Check exists
                var existingProduct = _unitOfWork.Products.GetById(product.Id);
                if (existingProduct == null)
                {
                    errorMessage = "Sản phẩm không tồn tại.";
                    return false;
                }

                // Update timestamp
                product.UpdatedAt = DateTime.Now;

                // Update repository
                _unitOfWork.Products.Update(product);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi cập nhật sản phẩm: {ex.Message}";
                return false;
            }
        }

        public bool DeleteProduct(int id, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var product = _unitOfWork.Products.GetById(id);

                if (product == null)
                {
                    errorMessage = "Sản phẩm không tồn tại.";
                    return false;
                }

                // Soft delete - set IsActive = false
                product.IsActive = false;
                product.UpdatedAt = DateTime.Now;

                _unitOfWork.Products.Update(product);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi xóa sản phẩm: {ex.Message}";
                return false;
            }
        }

        public bool UpdateStock(int productId, int quantityChange, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var product = _unitOfWork.Products.GetById(productId);

                if (product == null)
                {
                    errorMessage = "Sản phẩm không tồn tại.";
                    return false;
                }

                // Check if new stock would be negative
                int newStock = product.StockQuantity + quantityChange;
                if (newStock < 0)
                {
                    errorMessage = "Số lượng tồn kho không đủ.";
                    return false;
                }

                _unitOfWork.Products.UpdateStock(productId, quantityChange);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi cập nhật kho: {ex.Message}";
                return false;
            }
        }

        // ========== PRIVATE HELPERS ==========

        private bool ValidateProduct(Product product, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(product.ProductName))
            {
                errorMessage = "Tên sản phẩm không được để trống.";
                return false;
            }

            if (product.Price <= 0)
            {
                errorMessage = "Giá sản phẩm phải lớn hơn 0.";
                return false;
            }

            if (product.SalePrice < 0)
            {
                errorMessage = "Giá sale không được âm.";
                return false;
            }

            if (product.SalePrice > 0 && product.SalePrice >= product.Price)
            {
                errorMessage = "Giá sale phải nhỏ hơn giá gốc.";
                return false;
            }

            if (product.StockQuantity < 0)
            {
                errorMessage = "Số lượng tồn kho không được âm.";
                return false;
            }

            if (product.CategoryId <= 0)
            {
                errorMessage = "Phải chọn danh mục sản phẩm.";
                return false;
            }

            if (product.BrandId <= 0)
            {
                errorMessage = "Phải chọn thương hiệu.";
                return false;
            }

            return true;
        }
    }
}