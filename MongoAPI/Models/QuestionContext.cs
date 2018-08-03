using Microsoft.Extensions.Options;
using MongoAPI.Config;
using MongoDB.Driver;

namespace MongoAPI.Models
{
    public class QuestionContext
    {
        private readonly IMongoDatabase _database = null;

        public QuestionContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Question> Questions
        {
            get
            {
                return _database.GetCollection<Question>("Question");
            }
        }
    }
}
