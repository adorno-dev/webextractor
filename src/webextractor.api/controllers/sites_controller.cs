using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        // public JsonResult Get() 
        // {
        //     var response = _service.GetSites().Select(
        //         s => new { Id = s.Id, Name = s.Name, Domain = s.Domain, Links = s.Links.Select(s1 => new { Id = s1.Id, Url = s1.Url }) });
        //     return Json(response);
        // }

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