using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Services;
using WebExtractor.Common.Extensions;

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
        [HttpGet("{id}/values/page/{pageIndex}")]
        public Task<JsonResult> GetValues(Guid id, int? pageIndex, int? pageSize = 30)
        {

            IList<string[]> values = null;
            Link instance = _service.Get(id);

            instance.Content = _service.Download(instance);
            values = _service.Extract(instance);
            
            if (pageIndex.HasValue)
            {
                var paginated = values.Paginate(page: pageIndex, pageSize: pageSize);

                var answer = new {
                    pagination = new { Page = pageIndex, TotalRecords = paginated.totalRecords, TotalPages = paginated.totalPages },
                    data = paginated.collection.ToList()
                };
                
                return Task.FromResult(Json(answer));
            }

            return Task.FromResult(Json(values));
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