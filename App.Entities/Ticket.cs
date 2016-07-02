using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Entities
{
    /// <summary>
    /// Ticket entity class, provides all the properties of a ticket
    /// This class is for the tickets
    /// the ticket has; id, priority type, request type
    /// status type, description, user id, 
    /// </summary>

    public class Ticket
    {
        #region Public Properties
        /// <summary>
        /// This property is for the Ticket Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// This property is for the ticket priority type Id
        /// </summary>
        public int PriorityTypeId { get; set; }
        /// <summary>
        /// This property is for the ticket request type Id
        /// </summary>
        public int RequestTypeId { get; set; }
        /// <summary>
        /// This property is for the ticket type Id
        /// </summary>
        public int Rate { get; set; }

        public int StatusTypeId { get; set; }
        /// <summary>
        /// This property is for the ticket user creator
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// This property is for the ticket user name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Ticket User email
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// This property is for the ticket request description
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// This property is for the ticket creation date
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// This property is for the ticket modification date
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        public int OwnerId { get; set; }

        public string OwnerEmail { get; set; }
        

        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of a list of ticket comments
        /// </summary>
        public Ticket()
        {
            TicketComments = new List<TicketComment>();
            TicketImages = new List<TicketImage>();
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// These navigation properties provide a way to navigate an associate Request Types and Tickets
        /// </summary>
        public virtual RequestType RequestType { get; set; }
        /// <summary>
        /// This navigation property provide a way to navigate an associate PriorityTypes and Tickets
        /// </summary>
        public virtual PriorityType PriorityType { get; set; }
        /// <summary>
        /// This navigation property provide a way to navigate an associate StatusTypes and Tickets
        /// </summary>
        public virtual StatusType StatusType { get; set; }
        /// <summary>
        /// This navigation property provide a way to navigate an associate TicketComments and Tickets
        /// </summary>
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        /// <summary>
        /// This navigation property provide a way to navigate an associate TicketCommentImage and Ticket
        /// </summary>
        public virtual ICollection<TicketImage> TicketImages { get; set; }
        #endregion
    }
}
