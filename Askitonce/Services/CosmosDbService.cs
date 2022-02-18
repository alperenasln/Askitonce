namespace Askitonce.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Askitonce.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddQuestionItemAsync(Question question)
        {
            question.Type = "question";
            await this._container.CreateItemAsync<Question>(question, new PartitionKey(question.Type));
        }

       public async Task AddAnswerItemAsync(Answer answer)
        {
            answer.Type = "answer";
            await this._container.CreateItemAsync<Answer>(answer, new PartitionKey(answer.Type));
        }

        public async Task<Question> GetQuestionItemAsync(string id)
        {
            try
            {
                ItemResponse<Question> response = await this._container.ReadItemAsync<Question>(id, new PartitionKey("question"));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }
        public async Task<IEnumerable<Answer>> GetAnswerItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Answer>(new QueryDefinition(queryString));
            List<Answer> results = new List<Answer>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<IEnumerable<Question>> GetQuestionItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Question>(new QueryDefinition(queryString));
            List<Question> results = new List<Question>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
       
      


    }
}
