using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Rainfall.Model;
using System.Net;

namespace Rainfall.Api.Filters
{
    public class ExceptionHandlingFilterConfig : IExceptionFilter
    {
        private readonly ILogger<ExceptionHandlingFilterConfig> _logger;

        public ExceptionHandlingFilterConfig(ILogger<ExceptionHandlingFilterConfig> logger)
        {
            _logger = logger;
        }


        public void OnException(ExceptionContext context)
        {
            Error httpErrorInfo = new();
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            switch (context.Exception)
            {
                case ApplicationException or ArgumentException or
                     InvalidOperationException or IndexOutOfRangeException or
                     FileNotFoundException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    httpErrorInfo.Message = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} - {context.Exception.Message}";
                    break;
                case UnauthorizedAccessException ex:
                    httpStatusCode = HttpStatusCode.Unauthorized;
                    httpErrorInfo.Message = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} - {ex.Message}";
                    break;
                case KeyNotFoundException ex:
                    httpStatusCode = HttpStatusCode.NotFound;
                    httpErrorInfo.Message = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} - {ex.Message}";
                    break;
                default:
                    httpErrorInfo.Message = $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} - {context.Exception.Message}";
                    break;
            }


            context.HttpContext.Response.StatusCode = (int)httpStatusCode;
            context.Result = new JsonResult(httpErrorInfo);
            context.ExceptionHandled = true;

            _logger.LogError(context.Exception, $"{DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss")} - {context.Exception.Message}");
        }
    }
}
