using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mopo_flo_backend.Entities;
using mopo_flo_backend.Infrastructures;
using mopo_flo_backend.Models.Auth;
using mopo_flo_backend.Models.Common;
using mopo_flo_backend.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mopo_flo_backend.Services.Implementation;

public class DevAuthService(
    IOptions<ConfigModel> configuration,
    AppDbContext appDbContext) : IAuthService
{
    public async Task<string> Login(LoginRequest request)
    {
        var userModel = await AddTelegramUserToDatabase(37922811);

        return CreateToken(userModel);
    }

    private string CreateToken(UserModel userModel)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.Value.Jwt.SecretKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration.Value.Jwt.Issuer,
            audience: configuration.Value.Jwt.Audience,
            claims: new List<Claim>
            {
                new("id", userModel.Id.ToString()),
                new("first_name", userModel.FirstName),
                new("last_name", userModel.FirstName),
                new("username", userModel.Username),
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserModel> AddTelegramUserToDatabase(int userId)
    {
        var telegramUserEntity = await GetTelegramUser(userId);
        return CreateUserModel(telegramUserEntity);
    }

    private static UserModel CreateUserModel(TelegramUser entity)
    {
        return new UserModel(
            entity.Id,
            entity.TelegramId,
            entity.IsBot,
            entity.FirstName,
            entity.LastName,
            entity.Username,
            entity.LanguageCode,
            entity.IsPremium,
            entity.AddedToAttachmentMenu,
            entity.AllowsWriteToPm,
            entity.PhotoUrl);
    }

    private async Task<TelegramUser> GetTelegramUser(int telegramId)
    {
        return await appDbContext.TelegramUsers.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
    }

}