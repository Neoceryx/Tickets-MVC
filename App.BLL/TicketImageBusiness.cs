using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class TicketImageBusiness
    {
        #region Variables
        private TicketImageRepository _ticketImageRepo;
        private FileUploadHelperBusiness _fileUploadHelperBll;
        #endregion

        #region Constructors
        public TicketImageBusiness()
        {
            _ticketImageRepo = new TicketImageRepository();
            _fileUploadHelperBll = new FileUploadHelperBusiness();
        }
        #endregion

        #region Methods
        public void Add(Ticket ticket, Image image)
        {
            TicketImage ticketImage = new TicketImage()
            {
                TicketId=ticket.Id,
                ImageId=image.Id
            };
            _ticketImageRepo.Add(ticketImage);
        }

        public TicketImage GetByTicketId(int id)
        {
            return _ticketImageRepo.GetByTicketId(id);
        }
        #endregion
    }
}
