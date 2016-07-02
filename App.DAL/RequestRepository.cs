using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public class RequestRepository
    {
        private TicketsDbContext _context;

        public RequestRepository()
        {
            _context = new TicketsDbContext();
        }

        public List<RequestType> GetAll()
        {
            return _context.RequestTypes.ToList();
        }
    }
}
