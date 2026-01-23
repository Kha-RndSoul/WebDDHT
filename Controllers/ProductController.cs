using System;
using System.Linq;
using System.Web.Mvc;
using WebDDHT.Helpers;
using WebDDHT.ViewModels;

namespace WebDDHT.Controllers
{
    public class ProductController : BaseController
    {
        /// <summary>
        /// Danh sách sản phẩm với filters và pagination
        /// GET: /Product hoặc /Product/Index
        /// </summary>
        public ActionResult Index(
            int? categoryId,
            int? brandId,
            decimal? minPrice,
            decimal? maxPrice,
            string keyword,
            string sortBy = "name",
            bool ascending = true,
            int page = 1,
            int pageSize = 12)
        {
            try
            {
                ViewBag.Title = "Danh sách sản phẩm";

                // Validate page
                if (page < 1) page = 1;

                int totalRecords, totalPages;

                // Get products with filters
                var products = ProductService.GetProductsPaged(
                    page,
                    pageSize,
                    out totalRecords,
                    out totalPages,
                    categoryId,
                    brandId,
                    minPrice,
                    maxPrice,
                    sortBy,
                    ascending
                );

                // Search if keyword provided
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    products = ProductService.SearchProducts(keyword);
                    totalRecords = products.Count();
                    totalPages = PaginationHelper.CalculateTotalPages(totalRecords, pageSize);
                }

                // Validate page after getting total
                page = PaginationHelper.ValidatePageNumber(page, totalPages);

                var viewModel = new ProductListViewModel
                {
                    Products = products,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords,
                    SelectedCategoryId = categoryId,
                    SelectedBrandId = brandId,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice,
                    SearchKeyword = keyword,
                    SortBy = sortBy,
                    Ascending = ascending,
                    Categories = CategoryService.GetActiveCategories()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi:  {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Chi tiết sản phẩm
        /// GET: /Product/Details/5
        /// </summary>
        public ActionResult Details(int id)
        {
            try
            {
                var product = ProductService.GetProductWithDetails(id);

                if (product == null)
                {
                    ViewBag.ErrorMessage = "Sản phẩm không tồn tại.";
                    return View("NotFound");
                }

                if (!product.IsActive)
                {
                    ViewBag.ErrorMessage = "Sản phẩm đã ngừng kinh doanh.";
                    return View("NotFound");
                }

                ViewBag.Title = product.ProductName;

                // Get related products (same category, exclude current)
                var relatedProducts = ProductService.GetProductsByCategory(product.CategoryId)
                    .Where(p => p.Id != id)
                    .Take(4)
                    .ToList();

                // Calculate average rating
                double averageRating = 0;
                if (product.ProductReviews != null && product.ProductReviews.Any())
                {
                    averageRating = product.ProductReviews.Average(r => r.Rating);
                }

                var viewModel = new ProductDetailViewModel
                {
                    Product = product,
                    RelatedProducts = relatedProducts,
                    Reviews = product.ProductReviews,
                    TotalReviews = product.ProductReviews?.Count ?? 0,
                    AverageRating = averageRating
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi:  {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Tìm kiếm sản phẩm
        /// GET: /Product/Search? keyword=...
        /// </summary>
        public ActionResult Search(string keyword, int page = 1, int pageSize = 12)
        {
            try
            {
                ViewBag.Title = $"Tìm kiếm:  {keyword}";

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return RedirectToAction("Index");
                }

                var products = ProductService.SearchProducts(keyword).ToList();

                int totalRecords = products.Count();
                int totalPages = PaginationHelper.CalculateTotalPages(totalRecords, pageSize);
                page = PaginationHelper.ValidatePageNumber(page, totalPages);

                // Apply pagination to search results
                products = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var viewModel = new ProductListViewModel
                {
                    Products = products,
                    SearchKeyword = keyword,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalRecords = totalRecords,
                    Categories = CategoryService.GetActiveCategories()
                };

                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }

        /// <summary>
        /// Lọc sản phẩm theo category
        /// GET: /Product/Category/5
        /// </summary>
        public ActionResult Category(int id, int page = 1, int pageSize = 12)
        {
            try
            {
                var category = CategoryService.GetCategoryById(id);

                if (category == null)
                {
                    ViewBag.ErrorMessage = "Danh mục không tồn tại.";
                    return View("NotFound");
                }

                ViewBag.Title = category.CategoryName;

                return Index(
                    categoryId: id,
                    brandId: null,
                    minPrice: null,
                    maxPrice: null,
                    keyword: null,
                    sortBy: "name",
                    ascending: true,
                    page: page,
                    pageSize: pageSize
                );
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View("Error");
            }
        }
    }
}