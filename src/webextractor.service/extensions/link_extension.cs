using System;
using System.IO;
using System.Net;
using WebExtractor.Domain.Models;

namespace WebExtractor.Service.Extensions
{
    public static class LinkExtension
    {
        public static string Download(this Link link)
        {
            string answerPath = $"{Environment.CurrentDirectory}/assets/{link.Id}.html";
            string answer = String.Empty;

            if (!File.Exists(answerPath))
            {
                using (var http = new WebClient())
                    answer = http.DownloadString(link.Url);
                
                File.WriteAllText(answerPath, answer);
            }
            else
                answer = File.ReadAllText(answerPath);

            return (link.Content = answer);
        }
    }
}