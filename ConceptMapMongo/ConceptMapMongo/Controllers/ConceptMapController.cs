using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConceptMapMongo.Models;
using ConceptMapMongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConceptMapMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptMapController : ControllerBase
    {
        private readonly IConceptMapControllerService _conceptmapservice;

        public ConceptMapController(IConceptMapControllerService service)
        {
            _conceptmapservice = service;
        }

        //GET: /api/conceptmap/1
        [HttpGet("{version}")]
        public async Task<IActionResult> GetData([FromRoute] double version)
        {
            var result = await _conceptmapservice.GetDatabyVersion(version);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostData([FromBody] ConceptMap data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool versionExists = await _conceptmapservice.VersionExists(data.Version);
            if (versionExists)
            {
                return BadRequest(error: "Version already exists");
            }
            var result = await _conceptmapservice.PostData(data);
            return CreatedAtAction("GetData", new { result.Version }, result);
        }
    }
}