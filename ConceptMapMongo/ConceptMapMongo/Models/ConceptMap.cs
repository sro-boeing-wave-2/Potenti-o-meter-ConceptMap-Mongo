using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ConceptMapMongo.Models
{
    public class ConceptMap
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalID { get; set; }
        public double Version { get; set; }
        public string Data { get; set; }
    }
}
