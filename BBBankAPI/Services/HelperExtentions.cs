using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class HttpContextExtensions
    {
        public static string GetRequestedApiVersion(this HttpContext httpContext)
        {
            var path = httpContext?.Request.Path.Value;

            return path?.Split('/')
                        .FirstOrDefault(segment => segment.StartsWith("v", StringComparison.OrdinalIgnoreCase))
                        ?? "v1";
        }
        public static string? GetUserId(this HttpContext httpContext)
        {
            if (httpContext?.User?.Claims == null)
                return null;

            return httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
