using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace LeaseManagement.Infrastructure.MongoDB.Data
{
    public class MongoDBService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;  
        public MongoDBService(IConfiguration configuration) 
        {
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("MongoDbConnection");   
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);    
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => _database;
    }
}
