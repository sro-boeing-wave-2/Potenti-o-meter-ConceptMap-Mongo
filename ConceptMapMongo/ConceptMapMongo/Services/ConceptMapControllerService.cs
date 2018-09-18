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

        public async Task<List<ConceptMap>> GetDatabyVersionandDomain(double version,string domain)
        {
            var result = await _context.Concepts.Find(x => x.Version == version && x.Domain == domain).ToListAsync();//.Find(x => version == x.Version).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ConceptMap> PostData(ConceptMap map)
        {
            await _context.Concepts.InsertOneAsync(map);
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
    }

    public interface IConceptMapControllerService
    {
        Task<List<ConceptMap>> GetDatabyVersionandDomain(double version,string domain);
        Task<ConceptMap> PostData(ConceptMap map);
        Task<bool> VersionExists(double version,string Domain);
    }
}
