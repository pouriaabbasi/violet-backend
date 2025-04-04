using violet.backend.Enums;
using violet.backend.Models.Auth;
using violet.backend.Services.Contracts;
using static System.Guid;

namespace violet.backend.Middlewares;

public class CurrentUserMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task Invoke(
        HttpContext context,
        ICurrentUserService sessionProvider)
    {
        if (context.User.Claims.Any())
        {
            TryParse((ReadOnlySpan<char>)context.User.Claims.First(x => x.Type == "id").Value, out var userId);
            var user = new CurrentUserModel
            {
                Id = userId,
                Gender = context.User.Claims
                    .Where(x => x.Type.Contains("gender"))
                    .Select(x => Enum.TryParse(typeof(GenderType), x.Value, true, out var result)
                        ? (GenderType?)result
                        : null)
                    .FirstOrDefault()
            };

            sessionProvider.Initialize(user);
        }

        await next(context);
    }
}