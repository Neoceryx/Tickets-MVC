using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public class ImageRepository
    {
        #region Variables
        /// <summary>
        /// This variable is to log on to the database 
        /// </summary>
        private TicketsDbContext _context;

        #endregion

        #region Constructors
        public ImageRepository()
        {
            _context = new TicketsDbContext();
        }
        #endregion

        #region Methods
        public Image Add(Image image) 
        {
            _context.Images.Add(image);
            _context.SaveChanges();
            return image;
        }

        public Image GetById(TicketImage ticketImage)
        {
            return _context.Images.FirstOrDefault(x => x.Id == ticketImage.ImageId);
        }
        #endregion
    }
}
