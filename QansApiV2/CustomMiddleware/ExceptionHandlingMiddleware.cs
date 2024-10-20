
using Microsoft.AspNetCore.Mvc;
using System;

namespace QansApiV2.CustomMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            try
            {
              await  _next(httpContext);
            }
            catch (Exception ex)
            {
              await  HandleError(httpContext, ex);
            }
        }

        private async Task HandleError(HttpContext httpContext, Exception error)
        {

            httpContext.Response.ContentType = "application/json";
            var response = httpContext.Response;
            _logger.LogError(error, "Exception occurred: {Message}", error.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Extensions=new Dictionary<string, object?> {
                    {"Custom Error Middleware",new object[]{ error.Message} }
                }
               
            };
           

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(problemDetails);


        }








    }
}
