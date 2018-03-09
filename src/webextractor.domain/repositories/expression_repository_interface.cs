using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Repositories
{
    public interface IExpressionRepository : IDisposable
    {
        IList<Expression> All();
        Expression Get(Guid id);

        void Create(Expression instance);
        void Update(Expression instance);
        void Delete(Guid id);
        void Delete(Expression instance);
    }
}