using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WordSmith.Core.Managers;
using WordSmith.Core.Managers.Interfaces;
using WordSmith.Core.Factories.Interfaces;
using WordSmith.Core.Wrappers.Interfaces;
using Service.Infrastructure;
using WordSmith.Core.Factories;
using Service.Filters;

namespace Service
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var configWrapper = new ConfigWrapper(this.Configuration);
            services.AddSingleton<IConfigWrapper>(provider => configWrapper);


            services.AddSingleton<IDatabaseManager, DatabaseManager>();
            services.AddTransient<ISentenceManager, SentenceManager>();
            services.AddTransient<IDatabaseFactory, DatabaseFactory>();

            // Add framework services.
            services.AddMvc()
                .AddMvcOptions(options => {
                    options.Filters.Add(new RequestExceptionFilter());
                });

            services.AddCors(options => {
                options.AddPolicy("AllowAll", p => {
                    p.AllowAnyOrigin();
                    p.AllowAnyMethod();
                    p.AllowAnyHeader();
                });
            });

        }
                

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.Use(async (context, next) => {
                if (context.Request.Path == "/") {
                    var headers = context.Response.Headers;
                    headers.Add("Cache-Control", "no-store");
                }
                await next();

            });

            app.UseCors("AllowAll");



            app.UseMvc();
        }
    }
}
