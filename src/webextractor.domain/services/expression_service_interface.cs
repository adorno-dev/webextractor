using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;

namespace WebExtractor.Domain.Services
{
    public interface IExpressionService : IDisposable
    {
        IList<Expression> GetExpressions();
        Expression Get(Guid expressionId);
        Expression Get(string expressionId);

        void Create(Expression expression);
        void Update(Expression expression);
        void Delete(Guid expressionId);
        void Delete(Expression expression);
    }
}