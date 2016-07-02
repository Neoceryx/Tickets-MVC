using App.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.DAL
{
    /// <summary>
    /// This class is to access the Priority Type Entity
    /// </summary>
    public class PriorityTypeRepository
    {
        #region Variables
        /// <summary>
        /// this variable is to access the database 
        /// </summary>
        private TicketsDbContext _context;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of a context
        /// </summary>
        public PriorityTypeRepository()
        {
            _context = new TicketsDbContext();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is to get a list of all priority types
        /// </summary>
        /// <returns>
        /// this method returns a list of priority types
        /// </returns>
        public List<PriorityType> GetAll()
        {
            return _context.PriorityTypes.ToList();
        }

        public PriorityType GetById(int id)
        {
            return _context.PriorityTypes.FirstOrDefault(x => x.Id == id);
        }
        #endregion
    }
}
