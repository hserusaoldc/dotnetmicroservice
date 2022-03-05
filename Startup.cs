using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
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
        private readonly IDictionary<Guid, Vehicle> _vehicles;

        public Startup()
        {
            _vehicles = new Dictionary<Guid, Vehicle>(8);
        }

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
                    await context.Response.WriteAsJsonAsync(_vehicles.Values);
                });

                /// ... POST
                endpoints.MapPost("/vehicles", async context =>
                {
                    JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
                    try
                    {
                        var vehicle = context.Request.ReadFromJsonAsync<Vehicle>(options).Result;
                        _vehicles.Add(vehicle.Id, vehicle);
                        context.Response.StatusCode = (int)HttpStatusCode.Created;
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    }
                });

                /// ... PUT
                endpoints.MapPut("/vehicles/{id}", async context => 
                {
                    JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
                    var vehicle = context.Request.ReadFromJsonAsync<Vehicle>(options).Result;

                    Guid id = new Guid((string)context.Request.RouteValues["id"]);
                    if (_vehicles.ContainsKey(id))
                    {
                        _vehicles[id] = vehicle;
                    }
                    else
                    {
                        _vehicles.Add(vehicle.Id, vehicle);
                    }
                });

                endpoints.MapDelete("/vehicles/{id}", async context =>
                {
                    Guid id = new Guid((string)context.Request.RouteValues["id"]);

                    if (_vehicles.ContainsKey(id))
                    {
                        _vehicles.Remove(id);
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    }
                });
            });
        }
    }
}
