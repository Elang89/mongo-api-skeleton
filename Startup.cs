using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoApi.Data.Interfaces;
using MongoApi.Data.Repositories;
using MongoApi.Properties;

namespace MongoApi
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddCors (options =>
            {
                options.AddPolicy ("CorsPolicy",
                    builder => builder.AllowAnyOrigin ()
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ());
            });
            services.AddMvcCore ()
                .AddFormatterMappings ()
                .AddJsonFormatters ();
            services.Configure<Settings> (options =>
            {
                options.ConnectionString = Configuration.GetSection ("MongoConnection:ConnectionString").Value;
                options.Database = Configuration.GetSection ("MongoConnection:Database").Value;
            });
            services.AddTransient<INoteRepository, NoteRepository> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }
            app.UseCors ("CorsPolicy");
            app.UseMvc ();
        }
    }
}