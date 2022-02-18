namespace Askitonce.Models
{
    using Newtonsoft.Json;
    public class Question
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName ="title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

       [JsonProperty(PropertyName ="type")]
       public string Type { get; set; }
    }
}
