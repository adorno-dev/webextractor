using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Services;

namespace WebExtractor.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExpressionsController : Controller
    {
        private readonly IExpressionService _service;

        public ExpressionsController(IExpressionService service) => _service = service;

        [HttpGet]
        public Task<IList<Expression>> Get() => Task.FromResult(_service.GetExpressions());

        [HttpGet("{id}")]
        public Task<Expression> Get(Guid id) => Task.FromResult(_service.Get(id));

        [HttpPost]
        public Task Post([FromBody] Expression instance) => Task.Run(() => _service.Create(instance));

        [HttpPut]
        public Task Put([FromBody] Expression instance) => Task.Run(() => _service.Update(instance));

        [HttpDelete]
        public Task Delete([FromBody] Expression instance) => Task.Run(() => _service.Delete(instance));
    }
}