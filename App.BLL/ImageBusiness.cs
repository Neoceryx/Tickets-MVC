using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class ImageBusiness
    {
        #region Variables
        private ImageRepository _imageRepo;
        #endregion

        #region Constructor
        public ImageBusiness()
        {
            _imageRepo = new ImageRepository();
        }
        #endregion

        #region Methods
        public Image Add(Image image, string imageRelativePath)
        {
            image.Path = imageRelativePath;
            return _imageRepo.Add(image);
        }

        public Image GetById(TicketImage ticketImage)
        {
            return _imageRepo.GetById(ticketImage);
        }
        #endregion
    }
}
