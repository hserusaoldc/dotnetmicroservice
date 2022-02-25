using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dotnetmicroservice
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                /// ... GET
                endpoints.MapGet("/vehicles", async context =>
                {
                    var vehicles = new Vehicle[10];

                    await context.Response.WriteAsync("Hello World!");
                });

                /// ... POST
                endpoints.MapPost("/vehicles", async context => 
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Created;
                    await context.Response.WriteAsync("Got it!");
                });

                endpoints.MapPut("/vehicles/{id}", async context => 
                {
                    await context.Response.WriteAsync("");
                });
            });
        }
    }
}
