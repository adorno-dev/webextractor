using System;
using System.Collections.Generic;

namespace WebExtractor.Domain.Models
{
    public class Site
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public virtual IList<Link> Links { get; set; }

        public Site() => (this.Id, this.Links) = (Guid.NewGuid(), new List<Link>());

        public Site(string name, string domain)
            => (this.Id, this.Links, this.Name, this.Domain) = (Guid.NewGuid(), new List<Link>(), name, domain);

        public void AddLink(string url)
            => this.Links.Add(new Link(url: url));
        
        public Link AddLink(string url, params string[] expressions)
        {
            var instance = new Link(url: url, expressions: expressions);
            this.Links.Add(instance);
            return instance;
        }
        
        public Link AddLink(Guid id, string url, params string[] expressions)
        {
            var instance = new Link(id: id, url: url, expressions: expressions);
            this.Links.Add(instance);
            return instance;
        }

        public Link AddLink(string id, string url, params string[] expressions)
        {
            var instance = new Link(id: id, url: url, expressions: expressions);
            this.Links.Add(instance);
            return instance;
        }
    }
}