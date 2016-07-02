using App.Entities;
using Newtonsoft.Json;

namespace App.Controllers
{
    internal class AuthenticationResponse
    {

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("collaborator")]
        public User User { get; set; }

    }

}