using System;

namespace WebExtractor.Domain.Models
{
    public class Expression
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        protected Expression() => 
            this.Id = Guid.NewGuid();

        public Expression(string value) =>
            (this.Id, this.Value) = (Guid.NewGuid(), value);
    }
}