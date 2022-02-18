namespace Askitonce
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Askitonce.Models;
    public interface ICosmosDbService
    {
        Task<IEnumerable<Question>> GetQuestionItemsAsync(string query);
        Task<IEnumerable<Answer>> GetAnswerItemsAsync(string query);
        Task<Question> GetQuestionItemAsync(string id);
        Task AddQuestionItemAsync(Question question);
        Task AddAnswerItemAsync(Answer answer);
        
        
    }
}
