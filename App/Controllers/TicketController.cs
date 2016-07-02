using App.BLL;
using App.Entities;
using App.Security;
using App.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace App.Controllers
{
    /// <summary>
    /// This controller is for everything related to Ticket
    /// </summary>
    public class TicketController : Controller
    {
        #region Private Constants
        private static readonly string ADMIN_NAME = "Admin";
        private static readonly string INVALID_FILE_EXTENSION_ERROR_MESSAGE = "El archivo solo puede ser .jpg, .gif o .png";
        private static readonly string USER = "user";
        private static readonly int IMAGE_SIZE = 3000000;
        #endregion

        #region Private Variables
        /// <summary>
        /// These private variables are to access the Business Logic Layer class members
        /// </summary>
        private TicketBusiness _ticketBll;
        private StatusTypeBusiness _statusBll;
        private RequestTypeBusiness _requestBll;
        private PriorityTypeBusiness _priorityBll;
        private TicketCommentBusiness _commentBll;
        private TicketImageBusiness _ticketImageBll;
        private ImageBusiness _imageBll;
        #endregion

        #region Constructors
        /// <summary>
        /// In this constructor initializes an instance of 
        /// Ticket Business, Status Type Business, Request Type Business
        /// Priority Type Businness, Ticket Comment Business and Email
        /// </summary>
        public TicketController()
        {
            _ticketBll = new TicketBusiness();
            _statusBll = new StatusTypeBusiness();
            _requestBll = new RequestTypeBusiness();
            _priorityBll = new PriorityTypeBusiness();
            _commentBll = new TicketCommentBusiness();
            _ticketImageBll = new TicketImageBusiness();
            _imageBll = new ImageBusiness();
        }
        #endregion

        #region Actions
        /// <summary>
        /// This method is to displays the add ticket form
        /// </summary>
        /// <returns>the view</returns>
        [AuthorizeRoles(IsCollaboratorExclusive = true)]
        public ActionResult Add()
        {
            var user = Session[USER] as User;
            //var user = getUser();
            ViewBag.User = user;

            var requestTypes = _requestBll.GetAll();
            var priorityTypes = _priorityBll.GetAll();
            var statusTypes = _statusBll.GetAll();
            var viewModel = new AddNewTicketViewModel(requestTypes, priorityTypes);

            return View(viewModel);
        }

        /// <summary>
        /// This method is to add a new ticket
        /// </summary>
        /// <param new ticket view model="viewModel"></param>
        /// <returns>the view</returns>
        [HttpPost]
        [AuthorizeRoles(IsCollaboratorExclusive = true)]
        public ActionResult Add(AddNewTicketViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = Session[USER] as User;
                //var user = getUser();
                ViewBag.User = user;
                viewModel.User = new User();
                
                if(Request.Files.Count > 0 && (Request.Files[0].ContentLength > 0 && Request.Files[0].ContentLength < IMAGE_SIZE)) 
                {
                    //TODO: error en extension de archivo incorrecta
                    viewModel.Image = new Image();
                    viewModel.Image.FileName = string.Format("{0}_{1}", DateTime.Now.ToString("ddMMyyyy_HHmmss"), Request.Files[0].FileName);

                    viewModel.ImageRelativePath = String.Format("{0}{1}", "~/Files/", viewModel.Image.FileName);
                    viewModel.Image.Path = Server.MapPath(viewModel.ImageRelativePath);
                    
                }

                viewModel.User.Id = user.Id;
                viewModel.User.Name = String.Format("{0} {1}", user.Name, user.LastNames);
                viewModel.User.Email = user.Email;

                var ticket = viewModel.NewTicket;
                var requestTypes = _requestBll.GetAll();
                var priorityTypes = _priorityBll.GetAll();
                var statusTypes = _statusBll.GetAll();

                try
                {
                    _ticketBll.Add(viewModel.NewTicket, viewModel.Image, Request.Files[0], user);
                    viewModel = new AddNewTicketViewModel(requestTypes, priorityTypes);
                    ModelState.Clear();
                }
                catch (InvalidFileExtension)
                {
                    ModelState.AddModelError("File", INVALID_FILE_EXTENSION_ERROR_MESSAGE);

                    viewModel.RequestTypes = requestTypes;
                    viewModel.PriorityTypes = priorityTypes;
                    viewModel.StatusTypes = statusTypes;
                    viewModel.InitializeDropdowns();
                }
                return View(viewModel);
            }

            return View();

        }
        
        /// <summary>
        /// This method is to see the ticket detail
        /// </summary>
        /// <param ticket id="id"></param>
        /// <returns>the view</returns>
        [AuthorizeRoles(IsCollaboratorExclusive = false, IsAdminExclusive =false)]
        public ActionResult Detail(int id)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket=new Ticket();
                try
                {
                    ticket = _ticketBll.GetById(Convert.ToInt32(id));
                }
                catch (BadRequestException)
                {
                    return RedirectToAction("Index","Error");
                }
                
                List<TicketComment> ticketComments = _commentBll.GetAllByTicketId(Convert.ToInt32(id));
                TicketImage ticketImage = _ticketImageBll.GetByTicketId(Convert.ToInt32(id));
                Image image  = null;

                if (ticketImage != null)
                {
                    image = _imageBll.GetById(ticketImage);
                }

                List<StatusType> statusTypes;
                List<RequestType> requestTypes;
                List<PriorityType> priorityTypes;
                var user = Session[USER] as User;
                //var user = getUser();
                ViewBag.User = user;

                InitializeTypeLists(out statusTypes, out requestTypes, out priorityTypes);

                var viewModel = new TicketDetailViewModel(ticket, statusTypes, requestTypes, priorityTypes, ticketComments, image);

                if (viewModel.Ticket.UserId != user.Id && user.Name!=null)
                {
                    return RedirectToAction("All","Ticket");
                }

                return View(viewModel);
            }

            return View();

        }

        /// <summary>
        /// This method is to update the ticket
        /// </summary>
        /// <param Whole ticket="ticket"></param>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult Update(Ticket ticket)
        {
            ticket.ModifiedDate = DateTime.Now;
            _ticketBll.Update(ticket);
            ViewBag.User = Session[USER] as User;
            //ViewBag.User = getUser();

            return Json(new
            {
                Status = "Success"
            });
        }

        /// <summary>
        /// This method is to add comment to ticket
        /// </summary>
        /// <param Whole Ticket="ticketComment"></param>
        /// <returns>Json</returns>
        [HttpPost]
        public JsonResult Comment(TicketComment ticketComment)
        {
            if (ModelState.IsValid)
            {
                var user = Session[USER] as User;
                //var user = getUser();
                ViewBag.User = user;
                
                if (user.Name != null)
                {
                    ticketComment.UserName = String.Format("{0}{1}{2}", user.Name, " ", user.LastNames);
                }
                else
                {
                    ticketComment.UserName = ADMIN_NAME;
                }


                _commentBll.Add(ticketComment);

                return Json(new
                {
                    Status = "Success"
                });
            }

            return Json(new
            {
                Status = "Something wrong"
            });
        }

        [HttpPost]
        public JsonResult GetSolutionDays(int id)
        {
            //_priorityBll.GetById(id);

            return Json(new { Status = _priorityBll.GetById(id).ToString() });
        }

        [HttpPost]
        public JsonResult GetHelper(int id)
        {
            return Json(new { Status = _requestBll.GetById(id).ToString() });
        }

        /// <summary>
        /// This method displays all tickets
        /// </summary>
        /// <returns>the view</returns>
        [AuthorizeRoles(IsCollaboratorExclusive = false, IsAdminExclusive = false)]
        public ActionResult All()
        {
            var user = Session[USER] as User;
            //var user = getUser();
            ViewBag.User = user;

            List<Ticket> tickets = _ticketBll.GetAll(user, user.IsAdmin);
            List<StatusType> statusTypes;
            List<RequestType> requestTypes;
            List<PriorityType> priorityTypes;


            InitializeTypeLists(out statusTypes, out requestTypes, out priorityTypes);

            var viewModel = new TicketDetailViewModel(tickets, statusTypes, requestTypes, priorityTypes);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult StatusFilter()
        {
            var user = Session[USER] as User;
            //var user = getUser();
            ViewBag.User = user;

            return InitializePartial("~/Views/Ticket/Partial/_StatusFilter.cshtml", user, user.IsAdmin);
        }

        [HttpGet]
        public ActionResult RequestFilter()
        {
            var user = Session[USER] as User;
            //var user = getUser();
            ViewBag.User = user;
            return InitializePartial("~/Views/Ticket/Partial/_RequestFilter.cshtml", user, user.IsAdmin);
        }

        [HttpGet]
        public ActionResult PriorityFilter()
        {
            var user = Session[USER] as User;
            //var user = getUser();
            ViewBag.User = user;

            return InitializePartial("~/Views/Ticket/Partial/_PriorityFilter.cshtml", user, user.IsAdmin);
        }

        public JsonResult UpdateOwner(Ticket ticket)
        {
            var owner = Session[USER] as User;

            _ticketBll.UpdateOwner(ticket, owner);

            return Json(new { Status = "Success" });
        }

        public JsonResult UpdateRate(Ticket ticket) 
        {
            _ticketBll.UpdateRate(ticket);
            return Json(new
            {
                Status = "Success"
            });
        }

        [AuthorizeRoles(IsCollaboratorExclusive = false, IsAdminExclusive = false)]
        public ActionResult Report()
        {

            var user = Session[USER] as User;
            //var user = getUser();
            ViewBag.User = user;

            List<Ticket> tickets = _ticketBll.GetAllFiled(user, user.IsAdmin);
            List<StatusType> statusTypes;
            List<RequestType> requestTypes;
            List<PriorityType> priorityTypes;


            InitializeTypeLists(out statusTypes, out requestTypes, out priorityTypes);

            var viewModel = new TicketDetailViewModel(tickets, statusTypes, requestTypes, priorityTypes);

            return View(viewModel);
        }

        [AuthorizeRoles(IsCollaboratorExclusive = true)]
        public ActionResult Rate(int id)
        {
            var user = Session[USER] as User;
            //var user = getUser();
            ViewBag.User = user;
            var ticket = _ticketBll.GetById(id);

            if (ticket.UserId != user.Id)
            {
                return RedirectToAction("Index", "Error");
            }
            if (ticket.Rate != 0)
            {
                return RedirectToAction("Detail", new { Id=id });
            }
            return View(id);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// This method initialize the status Type List, request Type List, priority Type List
        /// </summary>
        /// <param Status Types List="statusTypes"></param>
        /// <param Request Type List="requestTypes"></param>
        /// <param Priority Types List="priorityTypes"></param>
        private void InitializeTypeLists(out List<StatusType> statusTypes, out List<RequestType> requestTypes, out List<PriorityType> priorityTypes)
        {
            statusTypes = _statusBll.GetAll();
            requestTypes = _requestBll.GetAll();
            priorityTypes = _priorityBll.GetAll();
        }
        /// <summary>
        /// This method initialize
        /// </summary>
        /// <param name="partialViewLocation"></param>
        /// <returns></returns>
        private PartialViewResult InitializePartial(string partialViewLocation, User user, bool isAdmin)
        {
            List<Ticket> tickets = _ticketBll.GetAll(user, isAdmin);
            List<StatusType> statusTypes;
            List<RequestType> requestTypes;
            List<PriorityType> priorityTypes;

            InitializeTypeLists(out statusTypes, out requestTypes, out priorityTypes);

            var viewModel = new TicketDetailViewModel(tickets, statusTypes, requestTypes, priorityTypes);

            return PartialView(partialViewLocation, viewModel);
        }
        #endregion

        //private User getUser()
        //{
        //    return new User
        //    {
        //        Name = "Daniela",
        //        LastNames = "Delgado Hernandez",
        //        IsAdmin = false,
        //        Id = 192
        //    };
        //}
    }
}
