using System.Collections.Generic;

namespace Application.Infrastructure.GenericCommands.Pagination
{
    /// <summary>
    /// The pagination result sent back from the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginationDTO<T>
    {
        /// <summary>
        /// The Page number
        /// </summary>
        /// <value></value>
        public int PageNumber { get; set; }

        /// <summary>
        /// The page size maximum of 20 and minimum of 1
        /// </summary>
        /// <value></value>
        public int PageSize { get; set; }

        /// <summary>
        /// Total items in the database or searched
        /// </summary>
        /// <value></value>
        public int totalItems { get; set; }

        /// <summary>
        /// Total items in the database or searched
        /// </summary>
        /// <value></value>
        public int SubTotal { get; set; }

        /// <summary>
        /// Entity to send back
        /// </summary>
        /// <value></value>
        public ICollection<T> Entities { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public void PaginationDto()
        {
            Entities = new List<T>();
        }
    }

    /// <summary>
    /// Pgination for branch
    /// </summary>
    public class BranchPaginationDto<T> : PaginationDTO<T>
    {

        /// <summary>
        /// Total Branched will determine if the organization is a branch or head quarters
        /// </summary>
        /// <value></value>
        public int TotalBranches { get; set; }

        /// <summary>
        /// Shows if organization should allow 
        /// </summary>
        /// <value></value>
        public bool AllowRequest { get; set; }

        /// <summary>
        /// Id of the headquarter
        /// </summary>
        /// <value></value>
        public string HeadQuarterId { get; set; }
    }

}
