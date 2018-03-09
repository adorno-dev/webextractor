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

        [HttpGet("site/{siteId}/page/{pageIndex}")]
        public Task<JsonResult> GetFromSite(Guid siteId, int? pageIndex, int? pageSize = 15)
        {
            var values = _service.GetLinksFromSite(siteId);

            if (pageIndex.HasValue)
            {
                var paginated = values.Paginate(page: pageIndex, pageSize: pageSize);

                var answer = new
                {
                    pagination = new { Page = pageIndex, TotalRecords = paginated.totalRecords, TotalPages = paginated.totalPages },
                    data = paginated.collection.ToList()
                };

                return Task.FromResult(Json(answer));
            }

            return Task.FromResult(Json(values));
        }

        [HttpGet("{id}")]
        public Task<Link> Get(Guid id) => Task.FromResult(_service.Get(id));

        [HttpGet("{id}/values")]
        [HttpGet("{id}/values/page/{pageIndex}")]
        public Task<JsonResult> GetValues(Guid id, int? pageIndex, int? pageSize = 15)
        {
            IList<string[]> values = null;
            Link instance = _service.Get(id);

            instance.Content = _service.Download(instance);
            values = _service.Extract(instance);

            if (pageIndex.HasValue)
            {
                var paginated = values.Paginate(page: pageIndex, pageSize: pageSize);

                var answer = new
                {
                    pagination = new { Page = pageIndex, TotalRecords = paginated.totalRecords, TotalPages = paginated.totalPages },
                    data = paginated.collection.ToList()
                };

                return Task.FromResult(Json(answer));
            }

            return Task.FromResult(Json(values));
        }

        [HttpGet("{id}/search/values")]
        [HttpGet("{id}/search/values/page/{pageIndex}")]
        public Task<JsonResult> GetSearchValues(Guid id, [FromHeader] string search, int? pageIndex, int? pageSize = 15, [FromHeader] string expression = "")
        {
            IList<string[]> values = null;
            Link instance = _service.Get(id);

            if (string.IsNullOrEmpty(search) || instance == null)
                return string.IsNullOrEmpty(expression) ?
                    this.GetValues(id, pageIndex, pageSize) :
                    this.GetPreviewValues(id, expression, pageIndex, pageSize);
                    
                // return Task.FromResult(Json(new { }));

            instance.Content = _service.Download(instance);

            values = string.IsNullOrEmpty(expression) ?
                _service.Extract(instance) : 
                _service.Extract(instance, expression);

            values = values.Where(w => w.Any(a => a.ToLower().Contains(search.ToLower()))).Select(s => s).ToList();

            if (pageIndex.HasValue)
            {
                var paginated = values.Paginate(page: pageIndex, pageSize: pageSize);

                var answer = new
                {
                    pagination = new { Page = pageIndex, TotalRecords = paginated.totalRecords, TotalPages = paginated.totalPages },
                    data = paginated.collection.ToList()
                };

                return Task.FromResult(Json(answer));
            }

            return Task.FromResult(Json(values));
        }

        [HttpGet("{id}/preview/values")]
        [HttpGet("{id}/preview/values/page/{pageIndex}")]
        public Task<JsonResult> GetPreviewValues(Guid id, [FromHeader] string expression, int? pageIndex, int? pageSize = 15)
        {
            IList<string[]> values = null;
            Link instance = _service.Get(id);

            if (string.IsNullOrEmpty(expression))
                return Task.FromResult(Json(new { }));

            instance.Content = _service.Download(instance);
            values = _service.Extract(instance, expression);

            if (pageIndex.HasValue)
            {
                var paginated = values.Paginate(page: pageIndex, pageSize: pageSize);

                var answer = new
                {
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

        [HttpDelete("{id}")]
        public Task Delete(Guid id) => Task.Run(() => _service.Delete(id));
    }
}