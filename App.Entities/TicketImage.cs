using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class TicketImage
    {
        #region Public Properties
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ImageId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Ticket Tickets { get; set; }
        #endregion


    }
}
