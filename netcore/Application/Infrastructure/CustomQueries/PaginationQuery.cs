using System.Linq;
using Application.Infrastructure.GenericCommands.Pagination;

namespace Application.Infrastructure.CustomQueries
{
    /// <summary>
    /// IQuery Extension for pagination
    /// </summary>
    public static class PaginationQuery
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<T> AddPagination<T>(this IQueryable<T> query, PaginationCommand filter) {
            return query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            // return query;
        }
    }
}