using Microsoft.AspNetCore.Identity;
using WebCinema.Initializer;

namespace WebCinema.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context, IServiceProvider serviceProvider, CinemaContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            DbInitializer.Initialize(dbContext);

            await RoleInitializer.InitializeAsync(userManager, roleManager);

            await _next.Invoke(context);
        }
    }

    //public static class DbInitializerExtensions
    //{
    //    public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<DbInitializerMiddleware>();
    //    }

    //}
}
