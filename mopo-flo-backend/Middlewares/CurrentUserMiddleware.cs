using mopo_flo_backend.Models.Auth;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Middlewares;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate next;

    public CurrentUserMiddleware(
        RequestDelegate next)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(
        HttpContext context,
        ICurrentUserService sessionProvider)
    {
        if (context.User.Claims != null && context.User.Claims.Any())
        {
            var user = new CurrentUserModel
            {
                Id = Convert.ToInt64(context.User.Claims.First(x => x.Type == "id").Value)
            };

            sessionProvider.Initialize(user);
        }

        await next(context);
    }
}