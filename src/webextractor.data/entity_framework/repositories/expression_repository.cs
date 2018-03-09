using System;
using System.Linq;
using System.Collections.Generic;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebExtractor.Data.EntityFramework.Repositories
{
    public class ExpressionRepository : IExpressionRepository
    {
        private readonly WebExtractorContext _context;

        public ExpressionRepository(WebExtractorContext context) => _context = context;

        public IList<Expression> All() => _context.Expressions.Include(x => x.Link).OrderBy(o => o.Order).Select(s => s).ToList();

        public Expression Get(Guid id) => _context.Expressions.Include(x => x.Link).OrderBy(o => o.Order).Select(s => s).FirstOrDefault(x => x.Id.Equals(id));

        public void Create(Expression instance)
        {
            var identity = _context.Expressions.Where(w => w.LinkId.Equals(instance.LinkId)).Select(s => s.Order).ToList();
            instance.Order = identity.Count > 0 ? identity.Max() + 1 : 1;
            _context.Expressions.Add(instance);
            _context.SaveChanges();
        }

        public void Update(Expression instance)
        {
            _context.Entry<Expression>(instance).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _context.Expressions.Remove(_context.Expressions.Find(id));
            _context.SaveChanges();
        }

        public void Delete(Expression instance)
        {
            _context.Expressions.Remove(instance);
            _context.SaveChanges();
        }

        public void Dispose() => _context.Dispose();
    }
}