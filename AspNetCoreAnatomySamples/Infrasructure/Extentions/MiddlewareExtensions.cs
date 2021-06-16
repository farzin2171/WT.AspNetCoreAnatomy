using AspNetCoreAnatomySamples.Infrasructure.Middelwares;
using Microsoft.AspNetCore.Builder;

namespace AspNetCoreAnatomySamples.Infrasructure.Extentions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseEndPointLoggingMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<EndpointLoggingMiddleware>();
        }
    }
}
