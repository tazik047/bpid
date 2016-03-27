using Newtonsoft.Json;

namespace BLL.Models
{
    public class GoogleUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("given_name")]
        public string Name { get; set; }

        [JsonProperty("family_name")]
        public string Surname { get; set; }

        [JsonProperty("picture")]
        public string Photo { get; set; }
    }
}
