using System.Collections.Generic;

namespace App.Entities
{
    /// <summary>
    /// This class is for the ticket status type
    /// working, pending, cancelled, finished
    /// </summary>
    
    public class StatusType
    {
        #region Public Properties
        /// <summary>
        /// This property is for the Status ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// This property is for the status description
        /// </summary>
        public string Description { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of a list of tickets
        /// </summary>
        public StatusType()
        {
            Tickets = new List<Ticket>();
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// These navigation properties provide a way to navigate an associate
        /// Status Types and Tickets
        /// </summary>
        public virtual ICollection<Ticket> Tickets { get; set; }
        #endregion
    }
}
