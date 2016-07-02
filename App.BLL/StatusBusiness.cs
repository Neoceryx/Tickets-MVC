using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class StatusBusiness
    {
        private StatusRepository _statusRepo;

        public StatusBusiness()
        {
            _statusRepo = new StatusRepository();
        }

        public List<StatusType> GetAll()
        {
            return _statusRepo.GetAll();
        }
    }
}
