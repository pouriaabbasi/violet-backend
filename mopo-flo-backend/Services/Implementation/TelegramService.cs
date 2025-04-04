using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using violet.backend.Models.Common;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class TelegramService(IOptions<ConfigModel> configuration) : ITelegramService
{
    public bool ValidateTelegramData(string telegramData)
    {
        return true;
        var dataCheckString = PrepareDataCheckString(WebUtility.UrlDecode(telegramData), out var hash, out var authDate);
        return CheckTelegramHash(dataCheckString, hash) && CheckTelegramAuthDate(authDate);
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