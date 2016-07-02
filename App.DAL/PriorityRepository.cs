using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public class PriorityRepository
    {
        private TicketsDbContext _context;

        public PriorityRepository()
        {
            _context = new TicketsDbContext();
        }

        public List<PriorityType> GetAll()
        {
            return _context.PriorityTypes.ToList();
        }
    }
}
