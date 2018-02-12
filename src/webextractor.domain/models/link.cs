using System;
using System.Linq;
using System.Collections.Generic;

namespace WebExtractor.Domain.Models
{
    public class Link
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public virtual IList<Expression> Expressions { get; set; }

        protected Link()
            => (Id, Expressions) = (Guid.NewGuid(), new List<Expression>());

        public Link(string url, params string[] expressions)
            => (Id, Url, Expressions) = (Guid.NewGuid(), url, expressions.Select(s => new Expression(value: s)).ToArray());

        public Link(Guid id, string url, params string[] expressions)
            => (Id, Url, Expressions) = (id, url, new List<Expression>());
        
        public Link(string id, string url, params string[] expressions)
            => (Id, Url, Expressions) = (Guid.Parse(id), url, expressions.Select(s => new Expression(value: s)).ToArray());

        public void AddExpression(string expression) 
            => this.Expressions.Add(new Expression(value: expression));
        
        public void AddExpressionRange(params string[] expressions)
            => expressions.ToList()
                          .ForEach(s => this.AddExpression(expression: s));
    }
}