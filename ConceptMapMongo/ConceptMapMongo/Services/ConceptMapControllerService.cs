using ConceptMapMongo.Data;
using ConceptMapMongo.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConceptMapMongo.Services
{
    public class ConceptMapControllerService : IConceptMapControllerService
    {
        private readonly ConceptMapContext _context = null; 

        public ConceptMapControllerService(IOptions<Settings> settings)
        {
            _context = new ConceptMapContext(settings);
        }

        public async Task<List<ConceptMap>> GetConceptMapbyVersionandDomain(double version,string domain)
        {
            var result = await _context.Concepts.Find(x => x.Version == version && x.Domain == domain).ToListAsync();//.Find(x => version == x.Version).FirstOrDefaultAsync();
			
            return result;
        }

        public async Task<ConceptMap> PostConceptMap(ConceptMap map)
        {
            await _context.Concepts.InsertOneAsync(map);
			ConceptMapDomain conceptMapDomain = new ConceptMapDomain();
			conceptMapDomain.Domain = map.Domain;
			conceptMapDomain.Version = map.Version;
			var result = GetConceptMapbyVersionandDomain(map.Version, map.Domain).Result;
			conceptMapDomain.ConceptMapId = result.Select(x => x.ID).ToString();
			await _context.ConceptMapDomain.InsertOneAsync(conceptMapDomain);
			return map;
        }

        public async Task<bool> VersionExists(double version, string Domain)
        {
            var result = await _context.Concepts.Find(x => x.Version == version&& x.Domain == Domain).ToListAsync();
            if(result.Count != 0)
            {
                return true;
            }
            return false;
        }
		public async Task<List<ConceptMapDomain>> GetAllConceptMapByDomain( string domain)
		{
			var result = await _context.ConceptMapDomain.Find(x =>  x.Domain == domain).ToListAsync();//.Find(x => version == x.Version).FirstOrDefaultAsync();

			return result;
		}

		public async Task<List<string>> GetAllDistinctDomainAsync()
		{
			var result = await _context.ConceptMapDomain.Find(x => true).ToListAsync();
			var domain =result.Select(x => x.Domain).Distinct();
			return domain.ToList();
		}
        public async Task<bool> DeleteAllConceptMapByDomain( string domain)
		{
			DeleteResult actionResult = await _context.ConceptMapDomain.DeleteManyAsync(Builders<ConceptMapDomain>.Filter.Eq("Domain", domain));
			return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
		}
		public async Task<List<ConceptMapDomain>> GetAllConceptMapDomain()
		{
			var result = await _context.ConceptMapDomain.Find(x => true).ToListAsync();
			
			return result;
		}
		public async Task<bool> DeleteConceptMapDomainById(string id)
		{
			DeleteResult actionResult = await _context.ConceptMapDomain.DeleteOneAsync(Builders<ConceptMapDomain>.Filter.Eq("ID", id));
			return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;

		
		}

	}

    public interface IConceptMapControllerService
    {
        Task<List<ConceptMap>> GetConceptMapbyVersionandDomain(double version,string domain);
        Task<ConceptMap> PostConceptMap(ConceptMap map);
        Task<bool> VersionExists(double version,string Domain);
		Task<List<ConceptMapDomain>> GetAllConceptMapByDomain(string domain);
		Task<List<string>> GetAllDistinctDomainAsync();
		Task<bool> DeleteAllConceptMapByDomain(string domain);
		Task<List<ConceptMapDomain>> GetAllConceptMapDomain();
		Task<bool> DeleteConceptMapDomainById(string id);




	}
}
