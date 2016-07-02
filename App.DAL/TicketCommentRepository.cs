using App.Entities;
using System.Collections.Generic;
using System.Linq;

namespace App.DAL
{
    /// <summary>
    /// This class is to access the Ticket Comment Entity
    /// </summary>
    public class TicketCommentRepository
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
        public TicketCommentRepository()
        {
            _context = new TicketsDbContext();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is for get all comments from an specific ticket
        /// </summary>
        /// <param ticket id="id"></param>
        /// <returns>
        /// This method returns a list of all comments from an specific ticket sorted
        /// by created date descending
        /// </returns>
        public List<TicketComment> GetAllByTicketId(int id)
        {
            return _context.TicketComments.Where(x => x.TicketId == id)
                .OrderByDescending(x => x.CreatedDate).ToList();
        }

        /// <summary>
        /// This method is to save a comment to database
        /// </summary>
        /// <param Entire ticket comment="ticketComment"></param>
        public void Add(TicketComment ticketComment)
        {
            _context.TicketComments.Add(ticketComment);
            _context.SaveChanges();
        }
        #endregion
    }
}