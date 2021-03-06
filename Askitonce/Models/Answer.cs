namespace Askitonce.Models
{
    using Newtonsoft.Json;
    public class Answer
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName ="questionid")]
        public string QuestionId { get; set; }  

        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "answerauthor")]
        public string AnswerAuthor { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
