using App.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.DAL
{
    /// <summary>
    /// This class is for access the Request Type Entity
    /// </summary>
    public class RequestTypeRepository
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
        public RequestTypeRepository()
        {
            _context = new TicketsDbContext();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is to get a list of all request types
        /// </summary>
        /// <returns>
        /// this method returns a list of request Types
        /// </returns>
        public List<RequestType> GetAll()
        {
            return _context.RequestTypes.ToList();
        }

        public string GetById(int id)
        {
            var request = _context.RequestTypes.FirstOrDefault(x => x.Id == id);
            return request.RequestHelper.ToString();
        }
        #endregion
    }
}
