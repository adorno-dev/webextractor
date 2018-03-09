using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Repositories
{
    public interface ISiteRepository : IDisposable
    {
        IList<Site> All();
        Site Get(Guid id);

        void Create(Site instance);
        void Update(Site instance);
        void Delete(Guid id);
        void Delete(Site instance);
    }
}