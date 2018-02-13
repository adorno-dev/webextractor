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

        public IList<Expression> All() => _context.Expressions.Include(x => x.Link).ToList();

        public Expression Get(Guid id) => _context.Expressions.Include(x => x.Link).FirstOrDefault(x => x.Id.Equals(id));

        public void Create(Expression instance)
        {
            _context.Expressions.Add(instance);
            _context.SaveChanges();
        }

        public void Update(Expression instance)
        {
            _context.Entry<Expression>(instance).State = EntityState.Modified;
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