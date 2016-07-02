using App.Common;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class EmailSenderHelperBusiness
    {
        private EmailSenderHelper _email;
        private RequestTypeBusiness _requestTypeBll;
        private enum Status {
            Pending = 1,
            InProgress = 2,
            Finished = 3,
            Canceled = 4
        };


        public EmailSenderHelperBusiness()
        {
            _email = new EmailSenderHelper();
            _requestTypeBll = new RequestTypeBusiness();
        }

        public bool SendEmail(Ticket ticket, bool isTicketFinished)
        {
            List<RequestType> requestTypes = _requestTypeBll.GetAll();
            
            return _email.SendEmail(ticket,requestTypes, isTicketFinished);
        }
    }
}
