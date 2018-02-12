using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebExtractor.Domain.Models;
using System.Text;

namespace WebExtractor.Service.Extensions
{
    public static class ExpressionExtension
    {
        public static List<string[]> Response(this Link link)
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