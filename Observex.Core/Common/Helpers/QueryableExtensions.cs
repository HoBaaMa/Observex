using SafetyVision.Core.Common.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace SafetyVision.Core.Common.Helpers
{
    public static class QueryableExtensions
    {
        //public static async Task<System.Linq.Dynamic.Core.PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, PaginationParams paginationParams)
        //{
        //    // Filtering
        //    if (!string.IsNullOrWhiteSpace(paginationParams.FilterOn) && !string.IsNullOrWhiteSpace(paginationParams.FilterQuery))
        //    {
        //        var propertyInfo = typeof(T).GetProperty(paginationParams.FilterOn, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //        if (propertyInfo != null)
        //        {
        //            query = query.Where($"{paginationParams.FilterOn}.Contains(@0)", paginationParams.FilterQuery);
        //        }
        //    }

        //    // Sorting
        //    if (!string.IsNullOrWhiteSpace(paginationParams.SortBy))
        //    {
        //        query = query.OrderBy($"{paginationParams.SortBy} {(paginationParams.IsAscending ? "ascending" : "descending")}");
        //    }

        //    // Get total count before pagination
        //    var totalCount = await query.CountAsync();

        //    // Apply pagination
        //    var data = await query
        //        .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
        //        .Take(paginationParams.PageSize)
        //        .ToListAsync();

        //    return new System.Linq.Dynamic.Core.PagedResult<T>(data, totalCount, paginationParams.PageNumber, paginationParams.PageSize);
        //}
    }
}
