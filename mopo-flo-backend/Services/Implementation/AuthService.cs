using Microsoft.Extensions.Options;
using mopo_flo_backend.Models.Auth;
using mopo_flo_backend.Models.Common;
using mopo_flo_backend.Services.Contracts;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using mopo_flo_backend.Entities;
using mopo_flo_backend.Infrastructures;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace mopo_flo_backend.Services.Implementation;

public class AuthService(
    IOptions<ConfigModel> configuration,
    AppDbContext appDbContext) : IAuthService
{
    public async Task<string> Login(LoginRequest request)
    {
        if (!ValidateTelegramData(request.TelegramData))
            throw new Exception("Request is not valid");

        var userModel = await AddTelegramUserToDatabase(request.UserModel);

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
            },
            expires: DateTime.Now.AddHours(1),
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserModel> AddTelegramUserToDatabase(TelegramUserModel userModel)
    {
        var telegramUserEntity = await GetTelegramUser(userModel.Id);
        if (telegramUserEntity == null)
        {

            telegramUserEntity = new TelegramUser
            {
                AddedToAttachmentMenu = userModel.AddedToAttachmentMenu,
                AllowsWriteToPm = userModel.AllowsWriteToPm,
                FirstName = userModel.FirstName,
                IsBot = userModel.IsBot,
                IsPremium = userModel.IsPremium,
                LanguageCode = userModel.LanguageCode,
                LastName = userModel.LastName,
                PhotoUrl = userModel.PhotoUrl,
                TelegramId = userModel.Id,
                Username = userModel.Username
            };

            await appDbContext.TelegramUsers.AddAsync(telegramUserEntity);
        }
        else
        {
            telegramUserEntity.FirstName = userModel.FirstName;
            telegramUserEntity.LastName = userModel.LastName;
            telegramUserEntity.Username = userModel.Username;
            telegramUserEntity.AddedToAttachmentMenu = userModel.AddedToAttachmentMenu;
            telegramUserEntity.AllowsWriteToPm = userModel.AllowsWriteToPm;
            telegramUserEntity.IsBot = userModel.IsBot;
            telegramUserEntity.IsPremium = userModel.IsPremium;
            telegramUserEntity.LanguageCode = userModel.LanguageCode;
            telegramUserEntity.PhotoUrl = userModel.PhotoUrl;
        }
        await appDbContext.SaveChangesAsync();
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

    private async Task<TelegramUser> GetTelegramUser(long telegramId)
    {
        return await appDbContext.TelegramUsers.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
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