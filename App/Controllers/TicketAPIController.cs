using App.BLL;
using App.Dtos;
using App.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace App.Controllers
{
    public class TicketAPIController : ApiController
    {
        ///<summary>These are a static variables</summary>
        //private static readonly string CollaboratorAPI = "http://104.131.157.163/api/collaborator";
        //private readonly string ContentType = "application/json";
        private TicketBusiness _ticketBll;


        public TicketAPIController()
        {
            _ticketBll = new TicketBusiness();

        }

        /// <summary>
        /// This method is to consume the WebApi
        /// </summary>
        [HttpGet]
        public ResponseDto<List<TicketApi>> Get(string token)
        {
            User user = GetAll(token);

            var respo = new ResponseDto<List<TicketApi>>
            {
                Success = true,
                Response = _ticketBll.GetTicketsByUserId(user)
            };

            return respo;

        }

        public User GetAll(string token)
        {
            User user = new User();
            var collaborator = new APIWrapeer().GetCollaborator(token);
            var data = JsonConvert.DeserializeObject<AuthenticationAPI>(collaborator);

            user = data.User;

            return user;
        }

        class APIWrapeer
        {
            string _tokenresponse=null;

            public string token
            {
                get
                {
                    return _tokenresponse;
                }
            }

            public string GetCollaborator(string token)
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    client.Headers.Add("Authorization", token);

                    var T = client.DownloadString("http://104.131.157.163/api/collaborator.json");

                    return T;
                }

            }
        }

        public class CollaboratorResult
        {
            public User Collaborators { get; set; }
        }
    }
}