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

        public async Task AddItemAsync(Question question)
        {
            await this._container.CreateItemAsync<Question>(question, new PartitionKey(question.Id));
        }

       

        public async Task<Question> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Question> response = await this._container.ReadItemAsync<Question>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Question>> GetItemsAsync(string queryString)
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
