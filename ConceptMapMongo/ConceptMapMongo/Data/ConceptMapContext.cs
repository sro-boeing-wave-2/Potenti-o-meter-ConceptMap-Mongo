using ConceptMapMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptMapMongo.Data
{
    public class ConceptMapContext
    {
        private readonly IMongoDatabase _database = null;

        public ConceptMapContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<ConceptMap> Concepts
        {
            get
            {
                return _database.GetCollection<ConceptMap>("Concepts");
            }
        }
    }
}
