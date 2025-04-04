using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Infrastructures.Repository;
using violet.backend.Models.Auth;
using violet.backend.Models.Common;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class AuthService(
    IOptions<ConfigModel> configuration,
    ICurrentUserService currentUser,
    ITelegramService telegramService,
    IUserRepository repository) : IAuthService
{
    public async Task<string> LoginFromTelegram(TelegramLoginRequest request)
    {
        if (!telegramService.ValidateTelegramData(request.TelegramData))
            throw new Exception("Request is not valid");

        var userModel = await LoadUser(request.TelegramInfoDto);

        return CreateToken(userModel);
    }

    public async Task<string> RevokeToken()
    {
        var user = await repository.GetUserFromUserId(currentUser.User.Id);
        var userModel = await CreateUserModel(user);

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
                new("first_name", userModel.FirstName ?? string.Empty),
                new("last_name", userModel.LastName ?? string.Empty),
                new("username", userModel.Username ?? string.Empty),
                new("gender", userModel.Gender?.ToString() ?? string.Empty)
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserModel> LoadUser(TelegramInfoDto telegramInfoDto)
    {
        var userEntity = await repository.GetUserFromTelegramId(telegramInfoDto.Id);
        if (userEntity == null)
        {
            userEntity = new User();
            await repository.CreateNewUser(userEntity);
        }

        userEntity = await repository.UpdateTelegramInfo(userEntity, telegramInfoDto);

        return await CreateUserModel(userEntity);
    }

    private async Task<UserModel> CreateUserModel(User entity)
    {
        GenderType? gender = await repository.GetUserGender(entity.Id);

        return new UserModel(
            entity.Id,
            entity.TelegramInfo.TelegramId,
            entity.TelegramInfo.IsBot,
            entity.TelegramInfo.FirstName,
            entity.TelegramInfo.LastName,
            entity.TelegramInfo.Username,
            entity.TelegramInfo.LanguageCode,
            entity.TelegramInfo.IsPremium,
            entity.TelegramInfo.AddedToAttachmentMenu,
            entity.TelegramInfo.AllowsWriteToPm,
            entity.TelegramInfo.PhotoUrl,
            gender);
    }
}