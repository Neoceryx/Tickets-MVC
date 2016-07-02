using App.DAL;
using App.Entities;
using System.Collections.Generic;

namespace App.BLL
{
    /// <summary>
    /// This class is to access the RequestTypeRepository members
    /// and manipulate information that RequestTypeRepository methods return
    /// </summary>
    public class RequestTypeBusiness
    {
        #region Variables
        /// <summary>
        /// This variable is to access the RequestTypeRepository class members
        /// </summary>
        private RequestTypeRepository _requestRepo;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of Request Type Reposotory
        /// </summary>
        public RequestTypeBusiness()
        {
            _requestRepo = new RequestTypeRepository();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method calls GetAll method from status type repository from Data Access Layer
        /// </summary>
        /// <returns>This method returns a list of all request types</returns>
        public List<RequestType> GetAll()
        {
            return _requestRepo.GetAll();
        }

        public string GetById(int id)
        {
            return _requestRepo.GetById(id);
        }
        #endregion
    }
}
