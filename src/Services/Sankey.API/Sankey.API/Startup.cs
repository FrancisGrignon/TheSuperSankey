using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sankey.Infrastructure;

namespace Sankey.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var dbname = Guid.NewGuid().ToString();
            
            services.AddDbContext<SankeyContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                options.UseSqlite(Configuration.GetConnectionString("SqlLiteConnection"))
            );

            services.Configure<SankeySettings>(Configuration);

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "TheSuperSankey API V1",
                    Version = "v1",
                    Description = "The Super Sankey API V1. Provide data to create Energy Sankey",
                    TermsOfService = "Terms Of Service"
                });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("/swagger/v1/swagger.json", "TheSuperSankey API V1");
              });

            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new string[] { "index.html" }
            });
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
