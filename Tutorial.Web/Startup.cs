using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Tutorial.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IConfiguration configuration,
            ILogger<Startup> logger)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            app.Use(next =>
            {
                logger.LogInformation("app.Use()....");

                return async httpContext =>
                {
                    
                    logger.LogInformation("----async httpcontext");
                    if (httpContext.Request.Path.StartsWithSegments("/first"))
                    {
                        logger.LogInformation("----First!!!");
                        await httpContext.Response.WriteAsync("First!!!");
                    }
                    else
                    {
                        logger.LogInformation("-----next(httpContext---");
                        await next(httpContext);
                    }
                };
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
