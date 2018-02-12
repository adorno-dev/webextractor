using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using WebExtractor.Domain.Models;
using WebExtractor.Domain.Services;
using WebExtractor.Business.Services;
// using WebExtractor.Service.Extensions;

namespace WebExtractor.Spammer
{

    public class Emulator
    {
        public string Name { get; set; }
        public string Link { get; set; }

        public Emulator(string link, string name)
            => (this.Link, this.Name) = (link, name);
    }

    public class Rom
    {
        public string Title { get; set; }
        public string Link { get; set; }

        public Rom(string link, string title)
            => (this.Link, this.Title) = (link, title);
    }

    class Program
    {
        private static readonly string URL_BASE = "https://www.emuparadise.me";
        private static readonly string URL_EMULATORS = "https://www.emuparadise.me/Emulators.php";
        private static readonly string REGEX_EMULATOR_ANCHORS = "<a(\\sstyle=\"font)[^>]*>([^<]+)<\\/a>";
        private static readonly string REGEX_EMULATOR_HREF_NAME = "(?<=href=\")(.*)(?=\" )|(?<=>)(.*)(?=<)";

        private static readonly string URL_ROMS = "https://www.emuparadise.me/Sony_Playstation_2_ISOs/List-All-Titles/41";
        private static readonly string REGEX_ROM_ANCHORS = "<a(\\sclass=\"index gamelist\")[^>]*>([^<]+)<\\/a>";
        private static readonly string REGEX_ROM_HREF_TITLE = "(?<=href=\")(.+)(?=\")|(?<=>)(.*)(?=<)";

        static void Main(string[] args)
        {

            var service = new LinkService();
            var site = new Site(name: "EmuParadise", domain: URL_BASE);
            var emulators = new List<Emulator>();
            var roms = new List<Rom>();

            var emulatorsLink = site.AddLink(id: "3905adb3-e6b1-4262-95b5-0ef552bcfa18", url: URL_EMULATORS,
                expressions: new[] { REGEX_EMULATOR_ANCHORS, REGEX_EMULATOR_HREF_NAME });

            var romsLink = site.AddLink(id: "07f5a759-de52-40d5-a5ff-ce3663596532", url: URL_ROMS,
                expressions: new[] { REGEX_ROM_ANCHORS, REGEX_ROM_HREF_TITLE });

            // emulatorsLink.Download();
            // emulatorsLink.Response().ForEach(f => emulators.Add((Emulator)Activator.CreateInstance(typeof(Emulator), f)));

            service.Download(emulatorsLink);
            service.Extract(emulatorsLink).ForEach(f => emulators.Add((Emulator)Activator.CreateInstance(typeof(Emulator), f)));

            // romsLink.Download();
            // romsLink.Response().ForEach(f => roms.Add((Rom)Activator.CreateInstance(typeof(Rom), f)));

            service.Download(romsLink);
            service.Extract(romsLink).ForEach(f => roms.Add((Rom)Activator.CreateInstance(typeof(Rom), f)));

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("---------------");
            Console.WriteLine("-  Emulators  -");
            Console.WriteLine("---------------");
            
            foreach (var emulator in emulators)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("=> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(emulator.Name);
                Console.Write(Environment.NewLine);
            }
            
            Console.ResetColor();
        }
    }
}
