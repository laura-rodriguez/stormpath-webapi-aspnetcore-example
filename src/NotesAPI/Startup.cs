using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotesAPI.Models;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Services;
using Stormpath.AspNetCore;
using Stormpath.Configuration.Abstractions;

namespace NotesAPI
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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStormpath(new StormpathConfiguration()
            {
                Web = new WebConfiguration()
                {
                    Oauth2 = new WebOauth2RouteConfiguration()
                    {
                        Uri = "/token",
                        Password = new WebOauth2PasswordGrantConfiguration()
                        {
                            ValidationStrategy = WebOauth2TokenValidationStrategy.Stormpath
                        }
                    }
                }
            });
            services.AddDbContext<NoteAPIContext>(x => x.UseInMemoryDatabase());
            services.AddTransient<INoteRepository, InMemoryNoteRepository>();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseStormpath();
            app.UseMvc();
        }
    }
}
