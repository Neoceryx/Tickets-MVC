using System.Collections.Generic;

namespace App.Entities
{
    /// <summary>
    /// This class is for the ticket priority type, like high and normal
    /// </summary>

    public class PriorityType
    {
        #region Public Properties
        /// <summary>
        /// This property is for the Priority Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// This property is for the Priority Type Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// This property is for the priority type initial range day
        /// </summary>
        public int DayRangeStarts { get; set; }
        /// <summary>
        /// This property is for the priority type final range day
        /// </summary>
        public int DayRangeFinish { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of a list of tickets
        /// </summary>
        public PriorityType()
        {
            Tickets = new List<Ticket>();
        }
        #endregion

        #region Navigation Properties
        /// <summary>
        /// These navigation properties provide a way to navigate an associate
        /// Priority Types with Tickets
        /// </summary>
        public virtual ICollection<Ticket> Tickets { get; set; }
        #endregion
    }
}
