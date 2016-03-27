using Newtonsoft.Json;

namespace BLL.Models
{
    internal class Payload
    {
        [JsonProperty("given_name")]
        public string Name { get; set; }

        [JsonProperty("family_name")]
        public string Surname { get; set; }

        [JsonProperty("sub")]
        public string GoogleId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
