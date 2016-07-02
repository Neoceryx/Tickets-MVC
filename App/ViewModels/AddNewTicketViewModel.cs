using App.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.ViewModels
{
    /// <summary>
    /// This viewmodel combines information from ticket and all its related entities 
    /// to allow to insert a new ticket
    /// </summary>
    public class AddNewTicketViewModel
    {

        #region Public Properties
        /// <summary>
        /// These properties are needed for the different entities in the same view
        /// </summary>
        public TicketImage TicketImage { get; set; }
        public Image Image { get; set; }
        public string ImageRelativePath { get; set; }
        public int RequestType { get; set; }
        [Required(ErrorMessage = "Por favor seleccione una prioridad.")]
        public int Priority { get; set; }
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Por favor agregue una descripción.")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public User User { get; set; }
        public string UserEmail { get; set; }
        public IList<RequestType> RequestTypes { get; set; }
        public IList<PriorityType> PriorityTypes { get; set; }
        public IList<StatusType> StatusTypes { get; set; }
        public IList<SelectListItem> RequestTypeDropDown { get; set; }

        public Ticket NewTicket
        {
            get
            {
                return new Ticket
                {
                    Description = Description,
                    ModifiedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    UserId = User.Id,
                    UserName = User.Name,
                    UserEmail = User.Email,
                    RequestTypeId = RequestType,
                    PriorityTypeId = Priority,
                    StatusTypeId = 1
                };
            }
        }

        public TicketImage NewTicketImage
        {
            get
            {
                return new TicketImage
                {
                    TicketId = NewTicket.Id,
                    ImageId = Image.Id
                };
            }
        }


        #endregion

        #region Constructors
        /// <summary>
        /// This is a default constructor
        /// </summary>
        public AddNewTicketViewModel(){ }

        /// <summary>
        /// This constructor initialize a request types list and priority type List
        /// </summary>
        /// <param Request Type List="requestTypes"></param>
        /// <param Priority Type List="priorityTypes"></param>
        public AddNewTicketViewModel(IList<RequestType> requestTypes, IList<PriorityType> priorityTypes)
        {
            RequestTypes = requestTypes;
            PriorityTypes = priorityTypes;

            InitializeDropdowns();
        }

        #endregion

        #region Methods
        /// <summary>
        /// This method fills the request type drop down
        /// </summary>
        public void InitializeDropdowns()
        {
            RequestTypeDropDown = new List<SelectListItem>();

            foreach (var requestType in RequestTypes)
            {
                RequestTypeDropDown.Add(new SelectListItem { Text = requestType.Description, Value = requestType.Id.ToString() });
            }
        }
        #endregion
    }
}