using Newtonsoft.Json;
using System;

namespace App.Entities
{
    /// <summary>
    /// This class is used for help to deserialize the Json User returned by the WebAPI
    /// </summary>
    public class User
    {
        #region Public Properties
        /// <summary>
        /// This property is used for deserializing user the json returned by the authentication api
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// This JsonProperty is for User Name from WebAPI
        /// </summary>
        [JsonProperty("names")]
        public string Name { get; set; }

        /// <summary>
        /// This JsonProperty is for the User Last Names from WebAPI
        /// </summary>
        [JsonProperty("last_names")]
        public string LastNames { get; set; }

        /// <summary>
        /// This JsonProperty is for the User Curp from WebAPI
        /// </summary>
        [JsonProperty("curp")]
        public string Curp { get; set; }

        /// <summary>
        /// This JsonProperty is for the User BirthDate from WebAPI
        /// </summary>
        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }

        /// <summary>
        /// This JsonProperty is for the User Gender from WebAPI
        /// </summary>
        [JsonProperty("gender")]
        public string Gender { get; set; }

        /// <summary>
        /// This JsonProperty is for the User Position from WebAPI
        /// </summary>
        [JsonProperty("position")]
        public string Position { get; set; }

        /// <summary>
        /// This JsonProperty is for the User Status from WebAPI
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// This JsonProperty is for the User Team Name from WebAPI
        /// </summary>
        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        /// <summary>
        /// This JsonProperty is for the User Working Status from WebAPI
        /// </summary>
        [JsonProperty("workingstatus")]
        public string WorkingStatus { get; set; }

        /// <summary>
        /// This JsonProperty is for the User email from WebAPI
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// This JsonProperty is for the User working Start Date from WebAPI
        /// </summary>
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// This JsonProperty is for the User working Finish Date from WebAPI
        /// </summary>
        [JsonProperty("finish_date")]
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// This property is for the user token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
       /// This property is to know if the user is an admin or not
        /// </summary>
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
        public string AdminEmail { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// This is a default constructor of the class
        /// </summary>
        public User() { }
        #endregion
    }
}
 