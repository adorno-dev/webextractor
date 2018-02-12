using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Repositories
{
    public interface ILinkRepository : IDisposable
    {
        IList<Link> All();
        Link Get(Guid id);

        void Create(Link instance);
        void Update(Link instance);
        void Delete(Link instance);
    }
}