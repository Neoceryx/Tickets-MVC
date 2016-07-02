using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public class TicketImageRepository
    {
        #region Variables
        private TicketsDbContext _context;
        #endregion

        #region Constructors
        public TicketImageRepository()
        {
            _context = new TicketsDbContext();
        }
        #endregion

        #region Methods
        public void Add(TicketImage ticketImage)
        {
            _context.TicketImages.Add(ticketImage);
            _context.SaveChanges();

        }

        public TicketImage GetByTicketId(int id)
        {
            return _context.TicketImages.FirstOrDefault(x => x.TicketId == id);
        }
        #endregion
    }
}
