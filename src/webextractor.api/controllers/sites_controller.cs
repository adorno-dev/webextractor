using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Services;

namespace WebExtractor.Api.Controllers
{
    [Route("api/[controller]")]
    public class SitesController : Controller
    {
        private readonly ISiteService _service;

        public SitesController(ISiteService service) => _service = service;

        [HttpGet]
        public Task<IList<Site>> Get() => Task.FromResult(_service.GetSites());

        [HttpGet("{id}")]
        public Task<Site> Get(Guid id) => Task.FromResult(_service.Get(id));

        [HttpPost]
        public Task Post([FromBody] Site instance) => Task.Run(()=> _service.Create(instance));

        [HttpPut]
        public Task Put([FromBody] Site instance) => Task.Run(()=> _service.Update(instance));

        [HttpDelete]
        public Task Delete([FromBody] Site instance) => Task.Run(()=> _service.Delete(instance));
    }
}