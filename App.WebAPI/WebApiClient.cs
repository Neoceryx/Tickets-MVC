using App.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace App.WebAPI
{
    public class WebApiClient
    {
        #region Constants
        /// <summary>
        /// This constant contains the admin API url to be consume
        /// </summary>
        private static readonly string ADMIN_API_URL = "http://104.131.157.163/api/users/sign_in.json";
        /// <summary>
        /// This constant contains the collaborator API url to be consume
        /// </summary>
        private static readonly string COLLABORATOR_API_URL = "http://104.131.157.163/api/users/sign_in_collaborator.json";
        
        #endregion

        #region Public Methods
        /// <summary>
        /// This method is to consume the WebApi
        /// </summary>
        /// <param user email="email"></param>
        /// <param user password="password"></param>
        /// <param user is admin="isAdmin"></param>
        /// <returns></returns>
        public User GetInformation(string email, string password, bool isAdmin)
        {
            
            User user = new User();
            WebClient webClient = new WebClient();
            NameValueCollection values = new NameValueCollection();

                values["email"] = email;
                Byte[] response = new Byte[] { };
            
                if (isAdmin)
                {
                    values["password"] = password;
                    response = webClient.UploadValues(ADMIN_API_URL, "POST", values);
                    
                }
                else
                {
                    values["pin"] = password;
                    response = webClient.UploadValues(COLLABORATOR_API_URL, "POST", values);
                }
                var responseString = Encoding.Default.GetString(response);
                var responseObject = JsonConvert.DeserializeObject<AuthenticationResponse>(responseString);

                if (!isAdmin) { user = responseObject.User; }
                else { user.UserId = responseObject.UserId; user.AdminEmail = email; }
                user.Token = responseObject.Token;
                user.IsAdmin = isAdmin;

           return user;
        }
        #endregion
    }
}
