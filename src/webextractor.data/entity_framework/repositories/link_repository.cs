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

        public IList<Link> AllFromSite(Guid id)
        {
            var all = _context.Links.Include(x => x.Site).Where(w => w.SiteId.Equals(id)).Paginate().collection.ToList();
            return all.AsEnumerable().Reverse().ToList();
        }

        public IList<Link> All() // => _context.Links.Include(x => x.Expressions).Paginate().collection.ToList();
        {
            var all = _context.Links.Include(x => x.Expressions).Paginate().collection.ToList();
            all.ForEach(f => f.Expressions = f.Expressions.OrderBy(o => o.Order).ToList());
            return all;
        }

        public Link Get(Guid id) // => _context.Links.Include(x => x.Expressions).FirstOrDefault(x => x.Id.Equals(id));
        {
            var one = _context.Links.Include(x => x.Expressions).FirstOrDefault(x => x.Id.Equals(id));
            
            if (one != null && one.Expressions != null)
                one.Expressions = one.Expressions.OrderBy(o => o.Order).ToList();
                
            return one;
        }

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
        
        public void Delete(Guid id)
        {
            _context.Links.Remove(_context.Links.Find(id));
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