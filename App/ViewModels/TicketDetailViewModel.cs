using App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace App.ViewModels
{

    
    /// <summary>
    /// This viewmodel combines information from ticket and all its related entities
    /// </summary>
    public class TicketDetailViewModel
    {
        #region Constants
        private static readonly int FILED = 5;
        #endregion

        #region Public Properties
        /// <summary>
        /// these properties are needed for the different entities in the same view
        /// </summary>
        public Ticket Ticket { get; set; }
        public TicketComment TicketComment { get; set; }
        public Image Image { get; set; }
        public TicketImage TicketImage { get; set; }
        public TicketCommentImage TicketCommentImage { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase File { get; set; }
        public List<RequestType> RequestTypes { get; set; }
        public List<TicketComment> TicketCommentList { get; set; }
        public List<StatusType> StatusTypes { get; set; }
        public List<SelectListItem> StatusTypesDropDown { get; set; }
        public List<PriorityType> PriorityTypes { get; set; }
        public List<SelectListItem> PriorityTypesRadioButton { get; set; }
        public List<Ticket> Tickets { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TicketCreatedDate
        {
            get
            {
                return Ticket.CreatedDate;
            }
        }

        #endregion



        #region Constructors
        /// <summary>
        /// This constructor initializes objects or lists that are needed in the Detail view
        /// </summary>
        /// <param Whole ticket="ticket"></param>
        /// <param Status List="statusTypes"></param>
        /// <param Requests List="requestTypes"></param>
        /// <param Priorities List="priorityTypes"></param>
        /// <param Ticket Comment List="ticketCommentList"></param>
        public TicketDetailViewModel(Ticket ticket, List<StatusType> statusTypes, List<RequestType> requestTypes, List<PriorityType> priorityTypes, List<TicketComment> ticketCommentList)
        {
            Ticket = ticket;
            StatusTypes = statusTypes;
            RequestTypes = requestTypes;
            PriorityTypes = priorityTypes;
            TicketCommentList = ticketCommentList;

            InitializeDropDowns();
        }

        /// <summary>
        /// This constructor initializes objects or lists that are needed in the Detail view
        /// and in the All view
        /// </summary>
        /// <param Tickets List="tickets"></param>
        /// <param Status List="statusTypes"></param>
        /// <param Requests List="requestTypes"></param>
        /// <param Priorities List="priorityTypes"></param>
        public TicketDetailViewModel(List<Ticket> tickets, List<StatusType> statusTypes, List<RequestType> requestTypes, List<PriorityType> priorityTypes)
        {
            Tickets = tickets;
            StatusTypes = statusTypes;
            RequestTypes = requestTypes;
            PriorityTypes = priorityTypes;
        }

        public TicketDetailViewModel(Ticket ticket, List<StatusType> statusTypes, List<RequestType> requestTypes, List<PriorityType> priorityTypes, List<TicketComment> ticketCommentList, Image image) : this(ticket, statusTypes, requestTypes, priorityTypes, ticketCommentList)
        {
            Ticket = ticket;
            StatusTypes = statusTypes;
            RequestTypes = requestTypes;
            PriorityTypes = priorityTypes;
            TicketCommentList = ticketCommentList;
            Image = image;
            
            
            InitializeDropDowns();

        }
        #endregion

        #region Methods
        /// <summary>
        /// This method Initialize a Status Type Drop Down
        /// </summary>
        public void InitializeDropDowns()
        {
            StatusTypesDropDown = new List<SelectListItem>();

            foreach (var status in StatusTypes)
            {
                if (status.Id == FILED)
                {
                    continue;
                }
                if (status.Id == Ticket.StatusTypeId)
                {
                    StatusTypesDropDown.Add(new SelectListItem { Text = status.Description, Value = status.Id.ToString(), Selected = true });
                }
                else
                {
                    StatusTypesDropDown.Add(new SelectListItem { Text = status.Description, Value = status.Id.ToString(), Selected = false });
                }                
            }            
        }
        #endregion
    }
}
