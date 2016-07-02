using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public class TicketCommentImageRepository
    {
        #region Variables
        private TicketsDbContext _context;
        #endregion

        #region Constructor
        public TicketCommentImageRepository()
        {
            _context = new TicketsDbContext();
        }
        #endregion

        #region Methods
        public void Add(TicketCommentImage ticketCommentImage)
        {
            _context.TicketCommentImages.Add(ticketCommentImage);
            _context.SaveChanges();
        }
        #endregion
    }
}
