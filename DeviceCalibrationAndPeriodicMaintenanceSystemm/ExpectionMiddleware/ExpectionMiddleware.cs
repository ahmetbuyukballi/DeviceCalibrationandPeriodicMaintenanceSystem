using ApplicationCore.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm.ExpectionMiddleware
{
    public class ExpectionMiddleware
    {
        private readonly RequestDelegate _next;
        static readonly Serilog.ILogger log = Serilog.Log.ForContext<ExpectionMiddleware>();
        public ExpectionMiddleware(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var now = DateTime.UtcNow;
            Log.Error($"{now.ToString("HH:mm:ss")}:{ex}");
            var dto = new ErrorResponseDto()
            {
                Message = ex.Message,
                StatusCode = httpContext.Response.StatusCode
            };
            var json=JsonSerializer.Serialize(dto);
            return httpContext.Response.WriteAsync(json);
        }
    }
}
