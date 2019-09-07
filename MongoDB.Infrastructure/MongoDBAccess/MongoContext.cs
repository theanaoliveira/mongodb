using MongoDB.Driver;
using System;

namespace MongoDB.Infrastructure.MongoDBAccess
{
    public class MongoContext : IDisposable
    {
        private readonly IMongoDatabase Database;

        public MongoContext()
        {
            if (Environment.GetEnvironmentVariable("MONGO_CONN") != null)
            {
                var client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_CONN"));

                if (client != null)
                    Database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DATABASE"));
            }
        }

        public void Dispose()
        {

        }
    }
}
