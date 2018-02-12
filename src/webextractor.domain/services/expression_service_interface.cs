using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Services
{
    public interface IExpressionService : IDisposable
    {
        IList<Expression> GetExpressions();
        Expression Get(Guid ExpressionId);
        Expression Get(string ExpressionId);

        void Create(Expression Expression);
        void Update(Expression Expression);
        void Delete(Expression Expression);
    }
}