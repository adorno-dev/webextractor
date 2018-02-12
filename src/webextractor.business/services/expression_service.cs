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

        public Expression Get(Guid expressionId) => _repository.Get(id: expressionId);

        public Expression Get(string expressionId) => _repository.Get(id: Guid.Parse(expressionId));

        public void Create(Expression link) => _repository.Create(instance: link);

        public void Update(Expression link) => _repository.Update(instance: link);

        public void Delete(Expression link) => _repository.Delete(instance: link);

        #endregion

        public void Dispose() => _repository.Dispose();
    }
}