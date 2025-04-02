using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Infrastructures;
using violet.backend.Models.Auth;
using violet.backend.Models.Common;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class DevAuthService(
    IOptions<ConfigModel> configuration,
    AppDbContext appDbContext) : IAuthService
{
    public async Task<string> LoginFromTelegram(TelegramLoginRequest request)
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

    private static UserModel CreateUserModel(TelegramInfo entity)
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
            entity.PhotoUrl,
            GenderType.Female);
    }

    private async Task<TelegramInfo> GetTelegramUser(int telegramId)
    {
        //return await appDbContext.TelegramInfos.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
        return new TelegramInfo();
    }

}