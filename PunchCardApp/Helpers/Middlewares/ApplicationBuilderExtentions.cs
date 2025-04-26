namespace PunchCardApp.Helpers.Middlewares;

public static class ApplicationBuilderExtentions
{
    public static IApplicationBuilder UseUserSessionValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UserSessionValidation>();
    }
}