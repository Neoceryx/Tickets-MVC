using App.DAL;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BLL
{
    public class RequestBusiness
    {
        public RequestRepository _requestRepo;

        public RequestBusiness()
        {
            _requestRepo = new RequestRepository();
        }

        public List<RequestType> GetAll()
        {
            return _requestRepo.GetAll();
        }
    }
}
