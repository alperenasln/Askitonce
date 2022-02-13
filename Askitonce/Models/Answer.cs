namespace Askitonce.Models
{
    using Newtonsoft.Json;
    public class Answer
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
    }
}
