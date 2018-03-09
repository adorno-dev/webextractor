using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Repositories
{
    public interface ILinkRepository : IDisposable
    {
        IList<Link> All();
        IList<Link> AllFromSite(Guid id);
        Link Get(Guid id);

        void Create(Link instance);
        void Update(Link instance);
        void Delete(Guid id);
        void Delete(Link instance);
    }
}