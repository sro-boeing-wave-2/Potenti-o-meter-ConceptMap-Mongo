using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptMapMongo.Models
{
    public class ConceptMapData
    {
		public Concept Source { get; set; }
		public Concept Target { get; set; }
		public Predicate Relationship { get; set; }
	}
}
