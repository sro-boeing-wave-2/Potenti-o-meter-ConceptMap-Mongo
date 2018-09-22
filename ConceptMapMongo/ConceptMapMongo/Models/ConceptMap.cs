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
        public string ID { get; set; }
        public double Version { get; set; }
		public string Domain { get; set; }
		public ConceptMapData[] Triplet { get; set; }
		public string[] Concepts { get; set; }

	}
}
