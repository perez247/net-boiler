using System;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.IRepository;

namespace Persistence.Repository
{
    /// <summary>
    /// Repository for the database
    /// </summary>
    public class UnitOfWork: IUnitOfWork
    {
        
        private readonly DefaultDataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public UnitOfWork(DefaultDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Save all changes made to the database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Complete()
        {
            try
            {
                if (await _context.SaveChangesAsync() <= 0)
                    throw new CustomMessageException("It seems we are having issues saving at the moment");

                return true;
            }
            catch (Exception e)
            {
                throw new CustomMessageException(e.Message);
            }

        }

        /// <summary>
        /// Over rides the dispose method
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}