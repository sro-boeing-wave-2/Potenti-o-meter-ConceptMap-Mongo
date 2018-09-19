using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConceptMapMongo.Models;
using ConceptMapMongo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace ConceptMapMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptMapController : ControllerBase
    {
        private readonly IConceptMapControllerService _conceptmapservice;
		ConnectionFactory factoryformessagebus = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

		public ConceptMapController(IConceptMapControllerService service)
        {
            _conceptmapservice = service;
        }

		public object factory { get; private set; }

		//GET: /api/conceptmap/1
		[HttpGet("{domain}/{version}")]
        public async Task<IActionResult> GetData([FromRoute] double version,string domain)
        {
            var result = await _conceptmapservice.GetDatabyVersionandDomain(version,domain);
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

            bool versionExists = await _conceptmapservice.VersionExists(data.Version,data.Domain);
            if (versionExists)
            {
                return BadRequest(error: "Version already exists");
            }
            var result = await _conceptmapservice.PostData(data);
			if (result.Equals(data))
			{
				using (var connection = factoryformessagebus.CreateConnection())
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare(queue: "Concepts",
										 durable: false,
										 exclusive: false,
										 autoDelete: false,
										 arguments: null);
					string bodydata= JsonConvert.SerializeObject(data);
					channel.BasicPublish(exchange: "",
										 routingKey: "Concepts",
										 mandatory: true,
										 basicProperties: null,
										 body: Encoding.UTF8.GetBytes(bodydata));
				}
			}
			return CreatedAtAction("GetData", new { result.Domain,result.Version }, result);
        }
    }
}