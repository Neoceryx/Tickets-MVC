using App.Common;
using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;

namespace App.BLL
{
    /// <summary>
    /// This class is to access the TicketCommentRepository members
    /// and manipulate information that TicketCommentRepository methods return
    /// </summary>
    public class TicketCommentBusiness
    {
        #region Variables
        /// <summary>
        /// This variable is to access the Ticket Comment Reposotory Class members
        /// </summary>
        private TicketCommentRepository _ticketCommentRepo;
        private ImageBusiness _imageBll;
        private TicketCommentImageBusiness _ticketCommentImageBll;
        private FileUploadHelper _fileUploadHelperBll;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of Ticket Comment Reposotory
        /// </summary>
        public TicketCommentBusiness()
        {
            _ticketCommentRepo = new TicketCommentRepository();
            _imageBll = new ImageBusiness();
            _ticketCommentImageBll = new TicketCommentImageBusiness();
            _fileUploadHelperBll = new FileUploadHelper();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method calls GetAllByTicketId Method from TicketCommentRepository 
        /// from Data Access Layer
        /// </summary>
        /// <param Ticket id="id"></param>
        /// <returns></returns>
        public List<TicketComment> GetAllByTicketId(int id)
        {
            return _ticketCommentRepo.GetAllByTicketId(id);
        }

        /// <summary>
        /// This method calls Add Method from TicketCommentReposotory
        /// from Data Access Layer
        /// </summary>
        /// <param Whole Ticket Comment="ticketComment"></param>
        public void Add(TicketComment ticketComment)
        {
            ticketComment.CreatedDate = DateTime.Now;
            _ticketCommentRepo.Add(ticketComment);
            //_imageBll.Add(image);
            //ticketCommentImageBll.Add(ticketCommentImage);
            //_fileUploadHelperBll.IsUploadedFileValid();
            //_fileUploadHelperBll.SaveUploadFile(image.Path);
        }
        #endregion
    }
}
