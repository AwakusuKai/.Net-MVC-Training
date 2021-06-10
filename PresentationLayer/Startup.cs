using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            int a = 2;

            //обрабатывает, если выполнено условие
            app.MapWhen(context =>
            {
                 return context.Request.Query.ContainsKey("name") &&
                 context.Request.Query["name"] == "Vlad";
            }, HandleName);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGet("/", async context =>
                {
                    
                    await context.Response.WriteAsync($"Hello World! Result is {a = a * 2}");
                });

                endpoints.MapGet("/anotherendpoint", async context =>
                {

                    await context.Response.WriteAsync($"Hello World! Its another endpoint. Result is {a = a * 2}");
                });


            });


            //Метод Run не передает обработку запроса дальше, поэтому эти методы располагают в конце.
            app.Run(async (context) =>   
            {
                await context.Response.WriteAsync("Not found!");
            });

            static void HandleName(IApplicationBuilder app)
            {
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Hi, Vlad!");
                });
            }
        }
    }
}
