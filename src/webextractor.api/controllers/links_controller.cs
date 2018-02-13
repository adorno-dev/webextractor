using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Services;

namespace WebExtractor.Api.Controllers
{
    [Route("api/[controller]")]
    public class LinksController : Controller
    {
        private readonly ILinkService _service;

        public LinksController(ILinkService service) => _service = service;

        [HttpGet]
        public Task<IList<Link>> Get() => Task.FromResult(_service.GetLinks());

        [HttpGet("{id}")]
        public Task<Link> Get(Guid id) => Task.FromResult(_service.Get(id));

        [HttpGet("{id}/values")]
        public Task<List<string[]>> GetValues(Guid id)
        {
            var instance = _service.Get(id);
            _service.Download(instance);
            return Task.FromResult(_service.Extract(instance));
        }

        [HttpPost]
        public Task Post([FromBody] Link instance)
        {
            _service.Create(instance);
            return Task.CompletedTask;
        }

        [HttpPut]
        public Task Put([FromBody] Link instance) => Task.Run(() => _service.Update(instance));

        [HttpDelete]
        public Task Delete([FromBody] Link instance) => Task.Run(() => _service.Delete(instance));
    }
}