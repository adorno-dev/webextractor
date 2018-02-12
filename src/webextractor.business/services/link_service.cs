using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Repositories;
using WebExtractor.Domain.Services;

namespace WebExtractor.Business.Services
{
    public class LinkService : ILinkService
    {
        #region +Attrs

        private readonly ILinkRepository _repository;

        #endregion

        #region +Ctors

        public LinkService() {}

        public LinkService(ILinkRepository repository) => _repository = repository;

        #endregion

        #region +Repository

        public IList<Link> GetLinks() => _repository.All();

        public Link Get(Guid linkId) => _repository.Get(id: linkId);

        public Link Get(string linkId) => _repository.Get(id: Guid.Parse(linkId));

        public void Create(Link link) => _repository.Create(instance: link);

        public void Update(Link link) => _repository.Update(instance: link);

        public void Delete(Link link) => _repository.Delete(instance: link);

        #endregion

        public void Dispose() => _repository.Dispose();

        public string Download(Link link)
        {
            string path = $"{Environment.CurrentDirectory}/wwwroot/assets/{link.Id}.html";
            string response = String.Empty;

            if (!File.Exists(path))
            {
                using (var http = new WebClient())
                    response = http.DownloadString(link.Url);

                File.WriteAllText(path, response);
            }
            else
                response = File.ReadAllText(path);

            return (link.Content = response);
        }

        public List<string[]> Extract(Link link)
        {
            var values = new List<string[]>();

            foreach (var expression in link.Expressions)
            {
                if (values.Count == 0)
                    values = Regex.Matches(link.Content, expression.Value)
                                  .Cast<Match>()
                                  .Select(s => new[] { s.Value })
                                  .ToList();
                else
                    for (int index = 0; index < values.Count; index++)
                        values[index] = Regex.Matches(values[index].FirstOrDefault(), expression.Value)
                                            .Cast<Match>()
                                            .Select(select => select.Value)
                                            .ToArray();
            }

            return values;
        }
    }
}