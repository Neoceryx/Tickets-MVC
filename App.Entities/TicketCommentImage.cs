using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class TicketCommentImage
    {
        #region Public Properties
        public int Id { get; set; }
        public int TicketCommentId { get; set; }
        public int ImageId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual TicketComment TicketComments { get; set; }
        #endregion

    }
}
