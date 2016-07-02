using App.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.DAL
{
    /// <summary>
    /// This class is to access the Status Types entity
    /// </summary>
    public class StatusTypeRepository
    {
        #region Variables
        /// <summary>
        /// This variable is to log on to the database 
        /// </summary>
        private TicketsDbContext _context;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of a context
        /// </summary>
        public StatusTypeRepository()
        {
            _context = new TicketsDbContext();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is to get a list of all Status Types
        /// </summary>
        /// <returns>
        /// This method returns a list of statusTypes
        /// </returns>
        public List<StatusType> GetAll()
        {
            return _context.StatusTypes.ToList();
        }
        #endregion
    }
}
