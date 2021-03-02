using System.Collections.Generic;

namespace Application.Infrastructure.CustomQueries
{
    /// <summary>
    /// Returning a collection with other addition result
    /// </summary>
    public class CollectionQueryResult<T>
    {
        /// <summary>
        /// Total items of the collection
        /// </summary>
        /// <value></value>
        public int TotalEntities { get; set; }

        /// <summary>
        /// The collection of items
        /// </summary>
        /// <value></value>
        public ICollection<T> Entities { get; set; }
    }
}