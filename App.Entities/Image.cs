using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Image
    {
        #region Public Properties
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<TicketImage> TicketImages { get; set; }
        public virtual ICollection<TicketCommentImage> TicketCommentImages { get; set; }
        #endregion



    }
}
