using App.Entities;
using Newtonsoft.Json;
using System;

namespace App.WebAPI
{
    /// <summary>
    /// This class is used for deserialize the Json returned by the WebAPI
    /// </summary>
    [Serializable]
    [JsonObject("data")]
    public class AuthenticationResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("collaborator")]
        public User User { get; set; }
    }
}
