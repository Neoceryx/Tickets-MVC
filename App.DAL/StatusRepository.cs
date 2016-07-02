using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public class StatusRepository
    {
        private TicketsDbContext _context;

        public StatusRepository()
        {
            _context = new TicketsDbContext();
        }

        public List<StatusType> GetAll()
        {
            return _context.StatusTypes.ToList();
        }
    }
}
