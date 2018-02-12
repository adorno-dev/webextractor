using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Services
{
    public interface ISiteService : IDisposable
    {
        IList<Site> GetSites();
        Site Get(Guid siteId);
        Site Get(string siteId);

        void Create(Site site);
        void Update(Site site);
        void Delete(Site site);
    }
}