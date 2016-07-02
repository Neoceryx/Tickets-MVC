using App.Entities;
using App.WebAPI;
using System.Net;

namespace App.BLL
{
    public class UserBusiness
    {
        private WebApiClient _webApi;

        public UserBusiness()
        {
            _webApi = new WebApiClient();
        }

        public User GetInformation(string email, string password, bool isAdmin)
        {
            var webApiResponse = new User();

            try
            {
                webApiResponse = _webApi.GetInformation(email, password, isAdmin);
            }
            catch (WebException)
            {
                throw new InvalidCredentials();
            }

            return webApiResponse;
        }

    }
}
