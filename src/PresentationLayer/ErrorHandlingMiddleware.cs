using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate Next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await Next.Invoke(context);
            if (context.Response.StatusCode == 403)
            {
                await context.Response.WriteAsync("Access Denied");
            }
            else if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync("Not Found");
            }
        }
    }
}
