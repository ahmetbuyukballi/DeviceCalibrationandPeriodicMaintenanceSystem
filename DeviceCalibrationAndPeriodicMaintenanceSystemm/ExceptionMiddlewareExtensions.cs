using DeviceCalibrationAndPeriodicMaintenanceSystemm.ExpectionMiddleware;

namespace DeviceCalibrationAndPeriodicMaintenanceSystemm
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExpectionMiddleware.ExpectionMiddleware>();
        }
    }
}
