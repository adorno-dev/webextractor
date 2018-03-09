using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Repositories;
using WebExtractor.Domain.Services;

namespace WebExtractor.Business.Services
{
    public class SiteService : ISiteService
    {
        #region +Attrs

        private readonly ISiteRepository _repository;

        #endregion

        #region +Ctors

        public SiteService(ISiteRepository repository) => _repository = repository;

        #endregion

        #region +Repository

        public IList<Site> GetSites() => _repository.All();

        public Site Get(string siteId) => _repository.Get(id: Guid.Parse(siteId));

        public Site Get(Guid siteId) => _repository.Get(id: siteId);

        public void Create(Site site) => _repository.Create(instance: site);

        public void Update(Site site) => _repository.Update(instance: site);

        public void Delete(Guid siteId) => _repository.Delete(id: siteId);

        public void Delete(Site site) => _repository.Delete(instance: site);

        #endregion

        public void Dispose() => _repository.Dispose();
    }
}