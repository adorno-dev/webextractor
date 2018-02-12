﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebExtractor.Api.Extensions;
using WebExtractor.Business.Services;
using WebExtractor.Data.EntityFramework;
using WebExtractor.Data.EntityFramework.Repositories;
using WebExtractor.Domain.Repositories;
using WebExtractor.Domain.Services;

namespace WebExtractor.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AsLowerCaseLocation()
                    .AddMvc();

            services.AddDbContext<WebExtractorContext>(options => options.UseInMemoryDatabase(databaseName: "WebExtractor"))
                    .AddScoped<WebExtractorContext>()
                    .AddScoped<ISiteRepository, SiteRepository>()
                    .AddScoped<ILinkRepository, LinkRepository>()
                    .AddScoped<IExpressionRepository, ExpressionRepository>()
                    .AddScoped<ISiteService, SiteService>()
                    .AddScoped<ILinkService, LinkService>()
                    .AddScoped<IExpressionService, ExpressionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc(m => m.MapRoute(name: "api", template: "{controller}/{action}/{id?}"));
        }
    }
}