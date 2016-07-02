using App.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace App.DAL
{
    /// <summary>
    /// This class is to access the Ticket Comment Entity
    /// </summary>
    public class TicketRepository
    {
        #region Variables
        /// <summary>
        /// this variable is to log on to the database 
        /// </summary>
        private TicketsDbContext _context;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of a context
        /// </summary>
        public TicketRepository()
        {
            _context = new TicketsDbContext();
            _context.Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is to save a new ticket in database
        /// </summary>
        /// <param Whole ticket="ticket"></param>
        public Ticket Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        /// <summary>
        /// This method is to get ticket by id
        /// </summary>
        /// <param tiket id="id"></param>
        /// <returns></returns>
        public Ticket GetById(int id)
        {
            return _context.Tickets.FirstOrDefault(ticket => ticket.Id == id);
        }

        /// <summary>
        /// This method is for update the ticket status, description and modified Date
        /// </summary>
        /// <param Whole ticket="ticket"></param>
        public void Update(Ticket ticket)
        {
            Ticket updateTicket = GetById(ticket.Id);
            updateTicket.StatusTypeId = ticket.StatusTypeId;
            updateTicket.ModifiedDate = ticket.ModifiedDate;
            updateTicket.Description = ticket.Description;
            updateTicket.PriorityTypeId = ticket.PriorityTypeId;
            updateTicket.RequestTypeId = ticket.RequestTypeId;
            updateTicket.Rate = ticket.Rate;
            _context.Entry(updateTicket).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Ticket> GetAllFiled(int id)
        {
            return _context.Tickets.Where(x => x.StatusTypeId == id).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public List<Ticket> GetAllFiledByUserId(int userId, int statusId)
        {
            return _context.Tickets.Where(x => x.UserId == userId && x.StatusTypeId == statusId).OrderByDescending(x=>x.CreatedDate).ToList();
        }

        public List<Ticket> GetByUserId(User user)
        {
            return _context.Tickets.Where(x => x.UserId == user.Id).ToList();
        }

        public List<TicketApi> GetTicketsByUserId(User user)
        {
            var x = (from t in _context.Tickets
                     join s in _context.StatusTypes on t.StatusTypeId equals s.Id
                     join r in _context.RequestTypes on t.RequestTypeId equals r.Id
                     join p in _context.PriorityTypes on t.PriorityTypeId equals p.Id
                     where t.UserId == user.Id
                     select new TicketApi
                     {
                         Id = t.Id,
                         PriorityType = p.Description,
                         RequestType = r.Description,
                         StatusType = s.Description,
                         TicketDescription = t.Description,
                         CreatedDate = t.CreatedDate,
                         UserId=t.UserId,
                         UserName = t.UserName,
                         Rate = t.Rate,
                         UserEmail = t.UserEmail,
                     }).ToList();
            return x;
        }

        public void UpdateRate(Ticket ticket)
        {
            Ticket updateTicket = GetById(ticket.Id);
            updateTicket.Rate = ticket.Rate;
            updateTicket.StatusTypeId = ticket.StatusTypeId;
            _context.Entry(updateTicket).State = EntityState.Modified;
            _context.SaveChanges();

        }

        public void UpdateOwner(Ticket ticket)
        {
            Ticket updateTicket = GetById(ticket.Id);
            updateTicket.OwnerEmail = ticket.OwnerEmail;
            updateTicket.OwnerId = ticket.OwnerId;
            updateTicket.StatusTypeId = ticket.StatusTypeId;
            _context.Entry(updateTicket).State = EntityState.Modified;
            _context.SaveChanges();
        }
        /// <summary>
        /// retrun a ticket's list by Admin Id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Ticket> GetAllByAdminId(User user)
        {
            return _context.Tickets.Where(x => x.OwnerId == user.UserId || x.OwnerId == 0)
                .OrderBy(x => x.CreatedDate)
                .ThenByDescending(y => y.StatusTypeId)
                .ToList();
        }
        #endregion
    }
}
