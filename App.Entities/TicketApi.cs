using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class TicketApi
    {
        public int Id { get; set; }
        public string PriorityType { get; set; }
        public string RequestType { get; set; }
        public string StatusType { get; set; }
        public string TicketDescription { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Rate { get; set; }
        public string UserEmail { get; set; }
    }
}
