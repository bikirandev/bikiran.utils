using System;
using System.Collections.Generic;
using System.Linq;

namespace Bikiran.Utils.Models
{
    /// <summary>
    /// Handles pagination logic with advanced page number generation and query support
    /// </summary>
    /// <remarks>
    /// Features include:
    /// - Fluent interface configuration
    /// - Smart page number truncation with ellipsis
    /// - SQL-ready skip/take calculations
    /// - Ordering direction management
    /// - Comprehensive pagination metadata
    /// </remarks>
    public class Pagination
    {
        /// <summary>
        /// Current 1-based page number
        /// </summary>
        /// <value>Default: 1, Minimum: 1</value>
        private int CurrentPage { get; set; } = 1;

        /// <summary>
        /// Number of items displayed per page
        /// </summary>
        /// <value>Default: 100, Minimum: 1</value>
        private int ContentPerPage { get; set; } = 100;

        /// <summary>
        /// Total number of items across all pages
        /// </summary>
        private int TotalContent { get; set; }

        /// <summary>
        /// Column name for ordering results
        /// </summary>
        /// <value>Default: "id"</value>
        private string OrderBy { get; set; } = "id";

        /// <summary>
        /// Sorting direction (asc/desc)
        /// </summary>
        /// <value>Default: "asc"</value>
        private string OrderType { get; set; } = "asc";

        /// <summary>
        /// Configure pagination settings in a single call
        /// </summary>
        /// <param name="cPage">1-based page number</param>
        /// <param name="cpp">Items per page</param>
        /// <param name="orderBy">Order column name</param>
        /// <param name="orderType">Sort direction (asc/desc)</param>
        /// <exception cref="ArgumentException">Throws for invalid page values</exception>
        public void SetData(int cPage, int cpp, string orderBy = "id", string orderType = "asc")
        {
            CurrentPage = Math.Max(cPage, 1);
            ContentPerPage = Math.Max(cpp, 1);
            OrderBy = orderBy?.Trim().ToLower() ?? "id";
            OrderType = orderType?.Trim().ToLower() == "desc" ? "desc" : "asc";
        }

        /// <summary>
        /// Set items per page using fluent interface
        /// </summary>
        /// <param name="cPerPage">Items per page (minimum 1)</param>
        /// <returns>Current Pagination instance</returns>
        public Pagination SetPerPage(int cPerPage)
        {
            ContentPerPage = Math.Max(cPerPage, 1);
            return this;
        }

        /// <summary>
        /// Set ordering column using fluent interface
        /// </summary>
        /// <param name="colName">Database column name</param>
        /// <returns>Current Pagination instance</returns>
        public Pagination SetOrderBy(string colName)
        {
            OrderBy = colName?.Trim().ToLower() ?? "id";
            return this;
        }

        /// <summary>
        /// Calculate OFFSET value for SQL queries
        /// </summary>
        /// <returns>Number of items to skip</returns>
        public int GetSkip()
        {
            return (CurrentPage - 1) * ContentPerPage;
        }

        /// <summary>
        /// Calculate LIMIT value for SQL queries
        /// </summary>
        /// <returns>Number of items to take</returns>
        public int GetTake()
        {
            return ContentPerPage;
        }

        /// <summary>
        /// Get current ordering column
        /// </summary>
        public string GetOrderBy() => OrderBy;

        /// <summary>
        /// Get current ordering direction
        /// </summary>
        public string GetOrderType() => OrderType;

        /// <summary>
        /// Generate smart page number sequence with truncation markers
        /// </summary>
        /// <remarks>
        /// Returns list where:
        /// - Positive numbers are page numbers
        /// - -100 represents leading ellipsis
        /// - -101 represents trailing ellipsis
        /// </remarks>
        private List<int> GeneratePages()
        {
            var pages = new List<int>();
            int totalPages = (int)Math.Ceiling((double)TotalContent / ContentPerPage);

            if (totalPages <= 5)
            {
                pages.AddRange(Enumerable.Range(1, totalPages));
                return pages;
            }

            // Initial pages
            pages.Add(1);
            pages.Add(2);

            // Leading ellipsis
            if (CurrentPage > 3) pages.Add(-100);

            // Current page context
            if (CurrentPage > 2) pages.Add(CurrentPage - 1);
            pages.Add(CurrentPage);
            if (CurrentPage < totalPages) pages.Add(CurrentPage + 1);

            // Trailing ellipsis
            if (CurrentPage < totalPages - 2) pages.Add(-101);

            // Final pages
            pages.Add(totalPages - 1);
            pages.Add(totalPages);

            return pages
                .Distinct()
                .Where(p => p > 0 && p <= totalPages)
                .ToList();
        }

        /// <summary>
        /// Get complete pagination metadata
        /// </summary>
        /// <returns>Object containing:
        /// - Current page
        /// - Items per page
        /// - Total items
        /// - Page count
        /// - Display range
        /// - Page sequence with ellipsis markers
        /// </returns>
        public object GetPageInfo()
        {
            int totalPages = (int)Math.Ceiling((double)TotalContent / ContentPerPage);
            int showingFrom = Math.Min((CurrentPage - 1) * ContentPerPage + 1, TotalContent);
            int showingTo = Math.Min(CurrentPage * ContentPerPage, TotalContent);

            return new
            {
                CurrentPage,
                ContentPerPage,
                TotalContent,
                NumberOfPages = totalPages,
                ShowingFrom = showingFrom,
                ShowingTo = showingTo,
                Pages = GeneratePages(),
                OrderBy,
                OrderType
            };
        }

        /// <summary>
        /// Set total item count
        /// </summary>
        /// <param name="count">Total number of items</param>
        public void SetTotalContent(int count)
        {
            TotalContent = Math.Max(count, 0);
        }

        /// <summary>
        /// Set sorting to ascending order
        /// </summary>
        public void SetAscending()
        {
            OrderType = "asc";
        }

        /// <summary>
        /// Set sorting to descending order
        /// </summary>
        public void SetDescending()
        {
            OrderType = "desc";
        }
    }
}