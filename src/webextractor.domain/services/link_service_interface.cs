using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Services
{
    public interface ILinkService : IDisposable
    {
        IList<Link> GetLinks();
        Link Get(Guid LinkId);
        Link Get(string LinkId);

        void Create(Link Link);
        void Update(Link Link);
        void Delete(Link Link);

        string Download(Link link);
        List<string[]> Extract(Link link);
    }
}