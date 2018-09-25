using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptMapMongo.Models
{
    public class ConceptMapDomain
    {
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; }
		public string Domain { get; set; }
		public double Version { get; set; }
		public string ConceptMapId  { get; set; }
	}

	
}
