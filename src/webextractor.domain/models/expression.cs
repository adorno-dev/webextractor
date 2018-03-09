using System;

namespace WebExtractor.Domain.Models
{
    public class Expression
    {
        public Guid Id { get; set; }

        public Guid LinkId { get; set; }
        public virtual Link Link { get; set; }

        public int Order { get; set; }

        public string Value { get; set; }

        public Expression() => 
            this.Id = Guid.NewGuid();

        public Expression(string value) =>
            (this.Id, this.Value) = (Guid.NewGuid(), value);
    }
}