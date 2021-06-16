using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAnatomySamples.Infrasructure.Extentions;
using System.Diagnostics;
using AspNetCoreAnatomySamples.Infrasructure.Services;

namespace AspNetCoreAnatomySamples
{
    //https://www.stevejgordon.co.uk/anatomy-of-asp-net-core-requests-talk-resources
    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-3.1
    //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1
    //https://www.stevejgordon.co.uk/asp-net-core-anatomy-part-2-addmvc
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
            services.AddSingleton<IMetricRecorder, MetricRecorder>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCoreAnatomySamples", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCoreAnatomySamples v1"));
            }

            app.UseHttpsRedirection();

            // Inline middleware example
            app.Use(async (ctx, next) =>
            {
                var stopWatch = Stopwatch.StartNew();

                await next(); // call next middleware in pipeline

                stopWatch.Stop();

                var recorder = ctx.RequestServices.GetRequiredService<IMetricRecorder>();

                recorder.RecordRequest(ctx.Response.StatusCode, stopWatch.ElapsedMilliseconds);
            });

            // Adds endpoint logging before "UseRouting", so this is not "Endpoint aware"
            app.UseEndPointLoggingMiddleWare();

            app.UseRouting();

            // Adds endpoint logging before "UseRouting", so this is not "Endpoint aware"
            app.UseEndPointLoggingMiddleWare();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
