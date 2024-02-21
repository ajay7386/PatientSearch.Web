using PatientSearch.Application.Dto;
using PatientSearch.Application.Enums;
using PatientSearch.Application.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace PatientSearch.Web.Middleware;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ErrorHandleMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandleMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context, ILogger<ErrorHandleMiddleware> _logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Global Exception message :{ex.Message},stackTrace: {ex.StackTrace}");
            (int statusCode, string errorCode, string errorDescription) = ex switch
            {
                DataNotFoundException => ((int)HttpStatusCode.NotFound, (ex as DataNotFoundException).ErrorCode, (ex as DataNotFoundException).ErrorDescription),
                BadRequestException => ((int)HttpStatusCode.BadRequest, (ex as BadRequestException).ErrorCode, (ex as BadRequestException).ErrorDescription),
                InvalidInputException => ((int)HttpStatusCode.BadRequest, (ex as InvalidInputException).ErrorCode, (ex as InvalidInputException).ErrorDescription),
                SystemDataException => ((int)HttpStatusCode.InternalServerError, (ex as SystemDataException).ErrorCode, (ex as SystemDataException).ErrorDescription),
                ValidateException => ((int)HttpStatusCode.BadRequest, (ex as ValidateException).ErrorCode, (ex as ValidateException).ErrorDescription),
                _ => ((int)HttpStatusCode.InternalServerError, ErrorStatus.ErrorCode.generalError.ToString(), ex.Message)
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            var error = new Error()
            {
                errorCode = errorCode,
                errorDescription = errorDescription
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }
    }
}
// Extension method used to add the middleware to the HTTP request pipeline.
//public static class ErrorHandleMiddlewareExtensions
//{
//    public static void UseErrorHandleMiddleware(this WebApplication app)
//    {
//        app.UseMiddleware<ErrorHandleMiddleware>();
//    }
//}

