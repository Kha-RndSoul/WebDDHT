using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.ViewModels
{
    /// <summary>
    /// ViewModel cho trang danh sách sản phẩm
    /// </summary>
    public class ProductListViewModel
    {
        // Products data
        public IEnumerable<Product> Products { get; set; }

        // Pagination
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        // Filters
        public int? SelectedCategoryId { get; set; }
        public int? SelectedBrandId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string SearchKeyword { get; set; }

        // Sorting
        public string SortBy { get; set; }
        public bool Ascending { get; set; }

        // Categories and Brands for filter dropdowns
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }

        // Helper properties
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public ProductListViewModel()
        {
            Products = new List<Product>();
            Categories = new List<Category>();
            Brands = new List<Brand>();
            CurrentPage = 1;
            PageSize = 12;
            SortBy = "name";
            Ascending = true;
        }
    }
}