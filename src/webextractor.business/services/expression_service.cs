using System;
using System.Collections.Generic;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Repositories;
using WebExtractor.Domain.Services;

namespace WebExtractor.Business.Services
{
    public class ExpressionService : IExpressionService
    {
        #region +Attrs

        private readonly IExpressionRepository _repository;

        #endregion

        #region +Ctors

        public ExpressionService(IExpressionRepository repository) => _repository = repository;

        #endregion

        #region +Repository

        public IList<Expression> GetExpressions() => _repository.All();

        public Expression Get(string expressionId) => _repository.Get(id: Guid.Parse(expressionId));

        public Expression Get(Guid expressionId) => _repository.Get(id: expressionId);

        public void Create(Expression expression) => _repository.Create(instance: expression);

        public void Update(Expression expression) => _repository.Update(instance: expression);

        public void Delete(Guid expressionId) => _repository.Delete(id: expressionId);

        public void Delete(Expression expression) => _repository.Delete(instance: expression);

        #endregion

        public void Dispose() => _repository.Dispose();
    }
}