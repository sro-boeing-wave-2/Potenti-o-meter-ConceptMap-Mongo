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
		public ContentTriplet[] contentConceptTriplet{ get; set; }

	}
	public class Content {
		public string Title { get; set; }
		public string Url { get; set; }
		public string[] Tags { get; set; }
		
	}
	public class ContentTriplet
	{
		public Content Source { get; set; }
		public Concept Target { get; set; }
		public ContentRelationship Relationship { get; set; }
	}
	public class ContentRelationship {
		public string Name { get; set; }
		public string Taxonomy { get; set; }

	}

}
