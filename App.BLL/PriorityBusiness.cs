using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class PriorityBusiness
    {
        private PriorityRepository _priorityRepo;

        public PriorityBusiness()
        {
            _priorityRepo = new PriorityRepository();
        }

        public List<PriorityType> GetAll()
        {
            return _priorityRepo.GetAll();
        }
    }
}
