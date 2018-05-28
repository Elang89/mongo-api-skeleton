using Microsoft.Extensions.Options;
using MongoApi.Models;
using MongoApi.Properties;
using MongoDB.Driver;

namespace MongoApi.Data
{
    public class MongoDbContext
    {
        private IMongoDatabase _database { get; }

        public MongoDbContext (IOptions<Settings> settings)
        {
            var client = new MongoClient (settings.Value.ConnectionString);
            if (client != null)
            {
                _database = client.GetDatabase (settings.Value.Database);
            }
        }

        public IMongoCollection<Note> Notes
        {
            get
            {
                return _database.GetCollection<Note> ("Note");
            }
        }

    }
}