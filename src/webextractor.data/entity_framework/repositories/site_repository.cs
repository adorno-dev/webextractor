using System;
using System.Linq;
using System.Collections.Generic;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebExtractor.Data.EntityFramework.Repositories
{
    public class SiteRepository : ISiteRepository
    {
        private readonly WebExtractorContext _context;

        public SiteRepository(WebExtractorContext context) => _context = context;

        // public IList<Site> All() => _context.Sites.Include(x => x.Links).ToList();
        public IList<Site> All() => _context.Sites.Include("Links").Include("Links.Expressions").ToList();

        // public Site Get(Guid id) => _context.Sites.Include(x => x.Links).FirstOrDefault(x => x.Id.Equals(id));
        public Site Get(Guid id) => _context.Sites.FirstOrDefault(x => x.Id.Equals(id));

        public void Create(Site instance)
        {
            _context.Sites.Add(instance);
            _context.SaveChanges();
        }

        public void Update(Site instance)
        {
            _context.Entry<Site>(instance).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _context.Sites.Remove(_context.Sites.Find(id));
            _context.SaveChanges();
        }

        public void Delete(Site instance)
        {
            _context.Sites.Remove(instance);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}