using System.Collections.Generic;

namespace App.Entities
{
    /// <summary>
    /// This class is for ticket request type
    /// like breakfast questions, card replacement, 
    /// </summary>
    
    public class RequestType
    { 
        #region Public Properties
        /// <summary>
        /// This property is for the request Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// This property is for the request type description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RequestHelper { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of a list of tickets
        /// </summary>
        public RequestType()
        {
            Tickets = new List<Ticket>();
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// These navigation properties provide a way to navigate an associate
        /// Request Types and Tickets
        /// </summary>
        public virtual ICollection<Ticket> Tickets { get; set; }
        #endregion
    }
}
