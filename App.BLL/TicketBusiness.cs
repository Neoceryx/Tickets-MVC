using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace App.BLL
{
    /// <summary>
    /// This class is to acces the TicketRepository members
    /// and manipulate information that TicketRepository methods return
    /// </summary>
    public class TicketBusiness
    {

        public enum Status {
            InProgress=2,
            Pending=1,
            Finished = 3,
            Canceled = 4,
            Filed = 5
        }
        private readonly int RATE = 0;

        #region Variables
        /// <summary>
        /// This variable is to access the Ticket Repository Class members
        /// </summary>
        private TicketRepository _ticketRepo;
        private EmailSenderHelperBusiness _emailSender;
        private TicketImageBusiness _ticketImageBll;
        private ImageBusiness _imageBll;
        private FileUploadHelperBusiness _fileUploadHelperBll;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of Ticket Reposotory
        /// </summary>
        public TicketBusiness()
        {
            _ticketRepo = new TicketRepository();
            _emailSender = new EmailSenderHelperBusiness();
            _ticketImageBll = new TicketImageBusiness();
            _imageBll = new ImageBusiness();
            _fileUploadHelperBll = new FileUploadHelperBusiness();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method calls method from Ticket Repository
        /// from Data Access Layer
        /// </summary>
        /// <param Whole Ticket="ticket"></param>
        public void Add(Ticket ticket, Image image, HttpPostedFileBase file, User user)
        {
            bool isTicketFinished = false;
            ticket.Rate = 0;
            ticket.OwnerId = 0;

            if (file.ContentLength > 0)
            {
                var imageRelativePath = string.Format("{0}{1}", "~/Files/", image.FileName);
                _fileUploadHelperBll.FileSaveUploadFile(file, image.Path);
                ticket = _ticketRepo.Add(ticket);
                image = _imageBll.Add(image, imageRelativePath);
                _ticketImageBll.Add(ticket, image);
            }
            else
            {
                ticket = _ticketRepo.Add(ticket);
            }
            
            _emailSender.SendEmail(ticket, isTicketFinished);

        }

        /// <summary>
        /// This method calls GetById method from Ticket reposotory
        /// from Data Access Layer
        /// </summary>
        /// <param Ticket Id="id"></param>
        /// <returns>returns a ticket</returns>
        public Ticket GetById(int id)
        {
            var ticket = _ticketRepo.GetById(id);
            if (ticket == null)
            {
                throw new BadRequestException();
            }
            return ticket;
        }



        /// <summary>
        /// This method calls Update method from Ticket Reposotory
        /// from Data Access Layer
        /// </summary>
        /// <param Whole Ticket="ticket"></param>
        public void Update(Ticket ticket)
        {

            ticket.Rate = RATE;
                
            _ticketRepo.Update(ticket);
            ticket = _ticketRepo.GetById(ticket.Id);

            if (ticket.StatusTypeId == (int)Status.Finished)
            {
                bool isTicketFinished = true;
                _emailSender.SendEmail(ticket,isTicketFinished);
            }
        }

        /// <summary>
        /// This method calls GetAll from Ticket Repository
        /// from Data Access Layer
        /// </summary>
        /// <returns>
        /// returns a list of tickets
        /// </returns>
        public List<Ticket> GetAll(User user, bool isAdmin)
        {
            
            if (isAdmin)
            {

                return _ticketRepo.GetAllByAdminId(user);
            }

            return _ticketRepo.GetByUserId(user);
        }

        public List<Ticket> GetAllFiled(User user, bool isAdmin)
        {
            if (!isAdmin)
            {
                return _ticketRepo.GetAllFiledByUserId(user.Id,(int)Status.Filed);
            }

            return _ticketRepo.GetAllFiled((int)Status.Filed);
        }

        public void UpdateRate(Ticket ticket)
        {
            ticket.StatusTypeId = (int)Status.Filed;
            _ticketRepo.UpdateRate(ticket);
        }

        public List<TicketApi> GetTicketsByUserId(User user)
        {
            return _ticketRepo.GetTicketsByUserId(user);
        }

        public void UpdateOwner(Ticket ticket, User owner)
        {
            ticket.OwnerId = owner.UserId;
            ticket.OwnerEmail = owner.AdminEmail;
            ticket.StatusTypeId = (int)Status.InProgress;
            _ticketRepo.UpdateOwner(ticket);
        }

        //public List<Ticket> GetAllByAdminId(User user)
        //{
        //    return _ticketRepo.GetAllByAdminId(user);
        //}
        #endregion
    }
}
