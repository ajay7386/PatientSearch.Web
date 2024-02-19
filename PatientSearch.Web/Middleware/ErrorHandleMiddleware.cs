using PatientSearch.Application.Dto;
using PatientSearch.Application.Enums;
using PatientSearch.Application.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PatientSearch.Web.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandleMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public async Task Invoke(HttpContext Context, ILogger<ErrorHandleMiddleware> logger)
        {
            try
            {
                await _next.Invoke(Context);
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception message :{ex.Message},stackTrace: {ex.StackTrace}");
                await HandleException(Context, ex).ConfigureAwait(false);
            }
        }
        private Task HandleException(HttpContext context, Exception exception)
        {
            (int statusCode, string errorCode, string errorDescription) = exception switch
            {
                DataNotFoundException _ => ((int)HttpStatusCode.NotFound, (exception as DataNotFoundException).ErrorCode, (exception as DataNotFoundException).ErrorDescription),
                InvalidInputException => ((int)HttpStatusCode.BadRequest, (exception as InvalidInputException).ErrorCode, (exception as InvalidInputException).ErrorDescription),
                SystemDataException => ((int)HttpStatusCode.InternalServerError, (exception as SystemDataException).ErrorCode, (exception as SystemDataException).ErrorDescription),
                ValidateException _ => ((int)HttpStatusCode.BadRequest, (exception as ValidateException).ErrorCode, (exception as ValidateException).ErrorDescription),
                _ => ((int)HttpStatusCode.InternalServerError, ErrorStatus.ErrorCode.generalError.ToString(), exception.Message)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var error = new Error()
            {
                errorCode = errorCode,
                errorDescription = errorDescription
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {requestBody}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {responseBody}";
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    //public static class ErrorHandleMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseErrorHandleMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<ErrorHandleMiddleware>();
    //    }
    //}
}
