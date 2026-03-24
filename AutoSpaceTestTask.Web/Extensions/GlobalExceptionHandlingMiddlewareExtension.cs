using AutoSpaceTestTask.Web.Middleware;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class GlobalExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
