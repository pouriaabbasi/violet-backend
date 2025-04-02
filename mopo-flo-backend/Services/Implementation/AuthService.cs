using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
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
    IUserRepository userRepository) : IAuthService
{
    public async Task<string> LoginFromTelegram(TelegramLoginRequest request)
    {
        if (!ValidateTelegramData(request.TelegramData))
            throw new Exception("Request is not valid");

        var userModel = await LoadUser(request.TelegramInfoDto);

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
                new("last_name", userModel.FirstName ?? string.Empty),
                new("username", userModel.Username ?? string.Empty),
                new("gender", userModel.Gender?.ToString() ?? string.Empty)
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserModel> LoadUser(TelegramInfoDto telegramInfoDto)
    {
        var userEntity = await userRepository.GetUserFromTelegramId(telegramInfoDto.Id);
        if (userEntity == null) userEntity = new User();

        userEntity = await userRepository.UpdateTelegramInfo(userEntity, telegramInfoDto);

        return CreateUserModel(userEntity);
    }

    private static UserModel CreateUserModel(User entity)
    {
        GenderType? gender = entity switch
        {
            FemaleUser => GenderType.Female,
            MaleUser => GenderType.Male,
            _ => null
        };

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

    private bool ValidateTelegramData(string telegramData)
    {
        var dataCheckString = PrepareDataCheckString(WebUtility.UrlDecode(telegramData), out var hash, out var authDate);
        return CheckTelegramHash(dataCheckString, hash) && CheckTelegramAuthDate(authDate);
    }

    private static bool CheckTelegramAuthDate(long telegramAuthDate)
    {
        var createdTelegramDataDate = DateTimeOffset.FromUnixTimeSeconds(telegramAuthDate).LocalDateTime;
        return DateTime.Now.Subtract(createdTelegramDataDate).TotalMinutes < 60;
    }

    private bool CheckTelegramHash(string dataCheckString, string telegramHash)
    {
        var secretKey = ComputeHmacSha256("WebAppData"u8.ToArray(), configuration.Value.TelegramBot.BotToken);
        var computedHashBytes = ComputeHmacSha256(secretKey, dataCheckString);
        var computedHash = ByteArrayToHexString(computedHashBytes);
        return telegramHash == computedHash;
    }

    private static string PrepareDataCheckString(string value, out string hash, out long authDate)
    {
        hash = string.Empty;
        authDate = 0;
        var dicResult = new Dictionary<string, string>();

        var separatedValues = value.Split("&");
        foreach (var separatedValue in separatedValues)
        {
            var keyValue = separatedValue.Split('=');
            if (keyValue[0] == "hash")
            {
                hash = keyValue[1];
                continue;
            }

            if (keyValue[0] == "auth_date") authDate = Convert.ToInt64(keyValue[1]);

            dicResult.Add(keyValue[0], keyValue[1]);
        }

        return string.Join("\n", dicResult.OrderBy(x => x.Key).Select(prop => $"{prop.Key}={prop.Value}"));
    }

    private static byte[] ComputeHmacSha256(byte[] key, string data)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);

        using var hmac = new HMACSHA256(key);
        var hashBytes = hmac.ComputeHash(dataBytes);
        return hashBytes;
    }

    private static string ByteArrayToHexString(byte[] byteArray)
    {
        var hex = new StringBuilder(byteArray.Length * 2);
        foreach (var b in byteArray)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }
}