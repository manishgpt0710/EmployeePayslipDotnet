using System;
using System.Text;

namespace EmployeePayslip.Middlewares
{
    public class CustomSessionMiddleware
    {
        private RequestDelegate _next;

        public CustomSessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IConfiguration configuration)
        {
            var browserId = httpContext.Request.Cookies["BrowserID"];

            if (string.IsNullOrEmpty(httpContext.Request.Cookies["BrowserID"]))
            {
                DateTime dt = DateTime.UtcNow;
                int timeSpan = Convert.ToInt32((dt - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
                StringBuilder bid = new StringBuilder(timeSpan.ToString());
                bid.Append(Guid.NewGuid().ToString("N"));

                var cookieOptions = new CookieOptions()
                {
                    Expires = DateTime.Now.AddYears(1)
                };

                browserId = bid.ToString();
                httpContext.Response.Cookies.Append("BrowserID", browserId, cookieOptions);
            }

            httpContext.Items["BrowserID"] = browserId;

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomSessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomSession(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomSessionMiddleware>();
        }
    }
}

