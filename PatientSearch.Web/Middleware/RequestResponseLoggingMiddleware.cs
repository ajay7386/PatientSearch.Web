using System.Text;

namespace PatientSearch.Web.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            //First, get the incoming request
            var request = await FormatRequest(context.Request);

            if (request != null)
            {
                logger.LogInformation($"Request in middleware: {Environment.NewLine} {request}");
            }

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);


                var response = await FormatResponse(context.Response);
                logger.LogInformation($"Response in middleware: {Environment.NewLine} {response}");

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode}: {text}";
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class RequestResponseLoggingMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    //    }
    //}
}
