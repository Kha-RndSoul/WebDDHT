using System;
using System.Collections.Generic;

namespace WebDDHT.Helpers
{
    /// <summary>
    /// Helper for pagination calculations
    /// </summary>
    public static class PaginationHelper
    {
        /// <summary>
        /// Calculate total pages
        /// </summary>
        public static int CalculateTotalPages(int totalRecords, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            return (int)Math.Ceiling((double)totalRecords / pageSize);
        }

        /// <summary>
        /// Get page numbers for pagination display
        /// </summary>
        /// <param name="currentPage">Current page number</param>
        /// <param name="totalPages">Total pages</param>
        /// <param name="maxDisplayPages">Max pages to display (default 5)</param>
        /// <returns>List of page numbers to display</returns>
        public static List<int> GetPageNumbers(int currentPage, int totalPages, int maxDisplayPages = 5)
        {
            var pages = new List<int>();

            if (totalPages <= maxDisplayPages)
            {
                // Display all pages
                for (int i = 1; i <= totalPages; i++)
                {
                    pages.Add(i);
                }
            }
            else
            {
                // Display pages around current page
                int halfMax = maxDisplayPages / 2;
                int startPage = Math.Max(1, currentPage - halfMax);
                int endPage = Math.Min(totalPages, startPage + maxDisplayPages - 1);

                // Adjust if end page is at boundary
                if (endPage == totalPages)
                {
                    startPage = Math.Max(1, endPage - maxDisplayPages + 1);
                }

                for (int i = startPage; i <= endPage; i++)
                {
                    pages.Add(i);
                }
            }

            return pages;
        }

        /// <summary>
        /// Validate page number
        /// </summary>
        public static int ValidatePageNumber(int page, int totalPages)
        {
            if (page < 1) return 1;
            if (page > totalPages && totalPages > 0) return totalPages;
            return page;
        }
    }
}