using App.Entities;
using Newtonsoft.Json;
using System;

namespace App.Controllers
{
    [Serializable]
    [JsonObject("data")]
    class AuthenticationAPI
    {
        [JsonProperty("collaborator")]
        public User User { get; set; }
    }
}
