using System;
using System.Linq;
using System.Collections.Generic;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using WebExtractor.Common.Extensions;

namespace WebExtractor.Data.EntityFramework.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly WebExtractorContext _context;
        public LinkRepository(WebExtractorContext context) => _context = context;

        public IList<Link> All() => _context.Links.Include(x => x.Expressions).Paginate().collection.ToList();

        public Link Get(Guid id) => _context.Links.Include(x => x.Expressions).FirstOrDefault(x => x.Id.Equals(id));

        public void Create(Link instance)
        {
            _context.Links.Add(instance);
            _context.SaveChanges();
        }

        public void Update(Link instance)
        {
            _context.Entry<Link>(instance).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Link instance)
        {
            _context.Links.Remove(instance);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}