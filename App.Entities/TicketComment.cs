using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Entities
{
    /// <summary>
    /// This class is for the tickets comments
    /// </summary>
    public class TicketComment
    {
        #region Public Properties
        /// <summary>
        /// This property  is for de Id ticket
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// This property is for the ticket ID
        /// </summary>
        public int TicketId { get; set; }
        /// <summary>
        /// This property is for the user name 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// This proeprty is for the ticket comment
        /// </summary>
        [Required]
        public string Comment { get; set; }
        /// <summary>
        /// this property is for the comment creation date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// This is a default constructor of the class
        /// </summary>
        public TicketComment()
        {
            TicketCommentImages = new List<TicketCommentImage>();
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// This navigation property provide a way to navigate an associate
        /// Tickets and Ticket Comment
        /// </summary>
        public virtual Ticket Ticket { get; set; }

        /// <summary>
        /// This navigation property provide a way to navigate an associate TicketCommentImage and Ticket
        /// </summary>
        public virtual ICollection<TicketCommentImage> TicketCommentImages { get; set; }
        #endregion
    }
}
