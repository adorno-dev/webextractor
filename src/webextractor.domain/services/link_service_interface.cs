using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Services
{
    public interface ILinkService : IDisposable
    {
        IList<Link> GetLinks();
        IList<Link> GetLinksFromSite(Guid siteId);
        Link Get(string linkId);
        Link Get(Guid linkId);
        
        void Create(Link link);
        void Update(Link link);
        void Delete(Guid linkId);
        void Delete(Link link);

        string Download(Link link);
        List<string[]> Extract(Link link);
        List<string[]> Extract(Link link, string expression);
    }
}