using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExtractor.Common.Extensions
{
    public static class PaginatedListExtensions
    {
        public static (int totalRecords, int totalPages, IQueryable<T> collection) Paginate<T>(this IQueryable<T> collection, int? page = 1, int? pageSize = 30) where T : class
        {
            int totalRecords = collection.Count();
            int totalPages = (int) Math.Ceiling(totalRecords / (double) pageSize);
            return (totalRecords, totalPages, collection.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value));
        }

        public static (int totalRecords, int totalPages, IEnumerable<T> collection) Paginate<T>(this IEnumerable<T> collection, int? page = 1, int? pageSize = 30) where T : class
        {
            int totalRecords = collection.Count();
            int totalPages = (int) Math.Ceiling(totalRecords / (double) pageSize);
            return (totalRecords, totalPages, collection.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value));
        }
    }
}