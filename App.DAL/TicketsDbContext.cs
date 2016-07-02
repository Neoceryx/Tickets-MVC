using App.Entities;
using System.Data.Entity;

namespace App.DAL
{
    /// <summary>
    /// This class allows you to link your model properties
    /// </summary>
    public class TicketsDbContext:DbContext
    {
        #region Public Properties
        /// <summary>
        /// these properties are for types that need to be part of the model
        /// </summary>
        public DbSet<PriorityType> PriorityTypes { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<TicketCommentImage> TicketCommentImages { get; set; }
        public DbSet<TicketImage> TicketImages { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// This is a default constructor of the class
        /// </summary>
        public TicketsDbContext():base("TicketsDbConnName")
        {
            
        }
        #endregion
        
        #region Overriding Methods
        /// <summary>
        /// This overriding method is for create our custom conventions
        /// </summary>
        /// <param model="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasRequired(x => x.PriorityType).
               WithMany(x => x.Tickets).HasForeignKey(x => x.PriorityTypeId);

            modelBuilder.Entity<Ticket>().HasRequired(x => x.RequestType).
                WithMany(x => x.Tickets).HasForeignKey(x => x.RequestTypeId);

            modelBuilder.Entity<Ticket>().HasRequired(x => x.StatusType).
                WithMany(x => x.Tickets).HasForeignKey(x => x.StatusTypeId);

            modelBuilder.Entity<TicketComment>().HasRequired(x => x.Ticket).
                WithMany(x => x.TicketComments).HasForeignKey(x => x.TicketId);

            modelBuilder.Entity<TicketCommentImage>().HasRequired(x => x.TicketComments).
                WithMany(x => x.TicketCommentImages).HasForeignKey(x => x.TicketCommentId);

            modelBuilder.Entity<TicketImage>().HasRequired(x => x.Tickets).
                WithMany(x => x.TicketImages).HasForeignKey(x => x.TicketId);

        }
        #endregion
    }
}
