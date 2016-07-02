using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class TicketCommentImageBusiness
    {
        #region Variales
        private TicketCommentImageRepository _ticketCommentImageRepo;
        #endregion

        #region Constructors
        public TicketCommentImageBusiness()
        {
            _ticketCommentImageRepo = new TicketCommentImageRepository();
        }
        #endregion

        #region Methods
        public void Add(TicketCommentImage ticketCommentImage)
        {
            _ticketCommentImageRepo.Add(ticketCommentImage);
        }
        #endregion
    }
}
