namespace Askitonce
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Askitonce.Models;
    public interface ICosmosDbService
    {
        Task<IEnumerable<Question>> GetItemsAsync(string query);
        Task<Question> GetItemAsync(string id);
        Task AddItemAsync(Question question);
        
    }
}
