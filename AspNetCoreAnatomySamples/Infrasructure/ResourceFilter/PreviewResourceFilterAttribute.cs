using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace AspNetCoreAnatomySamples.Infrasructure.ResourceFilter
{
    public class PreviewResourceFilterAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            // Is the preview header is missing or set to false we return not found
            if (!context.HttpContext.Request.Headers.TryGetValue("Preview", out var previewValue)
                || !bool.TryParse(previewValue.SingleOrDefault(), out var previewEnabled) || !previewEnabled)
            {
                context.Result = new NotFoundResult(); // short-circuit pipeline
                return;
            }

            // If the client is using preview mode, we let MVC handle the request (i.e. do not short-circuit) and include an extra header
            context.HttpContext.Response.Headers.Add("Preview-Endpoint", "True");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            
        }
    }
}
