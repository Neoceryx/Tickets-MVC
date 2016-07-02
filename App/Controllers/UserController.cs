using App.BLL;
using App.Entities;
using App.ViewModels;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace App.Controllers
{
    public class UserController : Controller
    {

        private static readonly string ERROR = "Usuario o contraseña incorrecta";
        private static readonly string USER = "user";

        /// <summary>
        /// 
        /// </summary>
        private UserBusiness _UserBll;


        /// <summary>
        /// 
        /// </summary>
        public UserController()
        {
            _UserBll = new UserBusiness();
        }
        //GET: LogIn
        public ActionResult Login()
        {
            if (Session[USER]!=null)
            {
                return RedirectToAction("All", "Ticket");
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(LogInViewModel viewModel)
        {
            try
            {
                //string decodeUrl = "";
                User user = _UserBll.GetInformation(viewModel.Email, viewModel.Password, viewModel.IsAdmin);
                if (string.IsNullOrEmpty(user.Token))
                {
                    ///TODO: return error if no token is available
                    return RedirectToAction("Login");
                }
                Session[USER] = user;
                var returnUrl = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReturnUrl"];

                if (returnUrl != null)
                {
                    if (!string.IsNullOrEmpty(returnUrl.ToString()))
                    {
                        var decodeUrl = Server.UrlDecode(returnUrl.ToString());
                        return Redirect(decodeUrl);
                    }
                }

                return RedirectToAction("All", "Ticket");
            }
            catch(InvalidCredentials)
            {
                ModelState.AddModelError("Error", ERROR);
            }

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            ViewBag.User = null;
            Session[USER] = null;
            return RedirectToAction("Login");
        }

    }
}