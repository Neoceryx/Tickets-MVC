using App.DAL;
using App.Entities;
using System.Collections.Generic;

namespace App.BLL
{
    /// <summary>
    /// This class is to access the PriorityTypeRepository members
    /// and manipulate information that PriorityTypeRepository methods return
    /// </summary>
    public class PriorityTypeBusiness
    {
        #region Variables
        /// <summary>
        /// This variable is to access the PriorityTypeRepository class members
        /// </summary>
        private PriorityTypeRepository _priorityRepo;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of Priority Type Reposotory
        /// </summary>
        public PriorityTypeBusiness()
        {
            _priorityRepo = new PriorityTypeRepository();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// This method calls GetAll method from priority type repository from Data Access Layer
        /// </summary>
        /// <returns>This method returns a list of all request types</returns>
        public List<PriorityType> GetAll()
        {
            return _priorityRepo.GetAll();
        }

        public string GetById(int id)
        {
            var priority = _priorityRepo.GetById(id);
            return string.Format("Días estimados de repuesta de {0} a {1} días", priority.DayRangeStarts, priority.DayRangeFinish);
        }
        #endregion

    }
}
