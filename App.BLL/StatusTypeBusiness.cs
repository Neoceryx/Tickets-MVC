using App.DAL;
using App.Entities;
using System.Collections.Generic;

namespace App.BLL
{
    /// <summary>
    /// This class is to access the StatusTypeRepository members
    /// and manipulate information that StatusTypeRepository methods return
    /// </summary>
    public class StatusTypeBusiness
    {
        #region Variables
        /// <summary>
        /// This variable is to access the StatusTypeRepository class members
        /// </summary>
        private StatusTypeRepository _statusRepo;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of Status Type Reposotory
        /// </summary>
        public StatusTypeBusiness()
        {
            _statusRepo = new StatusTypeRepository();
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// this method calls GetAll method from StatusTypeRepository
        /// </summary>
        /// <returns>this method returns a list of Status Types</returns>
        public List<StatusType> GetAll()
        {
            return _statusRepo.GetAll();
        }
        #endregion
    }
}
