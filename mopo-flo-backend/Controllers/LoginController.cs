using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace mopo_flo_backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController : ControllerBase
{
    private const string TelegramBotToken = "6527269583:AAGAY9mRWX9tX8x0dgEfvEKX9_sxETP9OTg";
    [HttpPost]
    public async Task<IActionResult> Auth(string userData)
    {
        var rawData =
            "query_id=AAH7p0ICAAAAAPunQgKmArau&user=%7B%22id%22%3A37922811%2C%22first_name%22%3A%22Pouria%22%2C%22last_name%22%3A%22Abbasi%22%2C%22username%22%3A%22PouriaAbbasi%22%2C%22language_code%22%3A%22en%22%2C%22allows_write_to_pm%22%3Atrue%7D&auth_date=1724068367&hash=f5f158eb984a7ab8f6ce013d653201e8c6d2e7eaf49039fb0543c26c6bef4065";

        var dataCheckString = PrepareString(WebUtility.UrlDecode(rawData), out var hash);

        var secretKey = ComputeHmacSha256("WebAppData", TelegramBotToken);

        var computedHash = ComputeHmacSha256Hex(dataCheckString, secretKey);

        var result = computedHash == hash;

        return result ? Ok(result) : NotFound();
    }

    public static byte[] ComputeHmacSha256(string key, string data)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        using (var hmac = new HMACSHA256(keyBytes))
        {
            byte[] hashBytes = hmac.ComputeHash(dataBytes);
            return hashBytes;
        }
    }

    public static string ComputeHmacSha256Hex(string data, byte[] secretKey)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        using (var hmac = new HMACSHA256(secretKey))
        {
            byte[] hashBytes = hmac.ComputeHash(dataBytes);
            return ByteArrayToHexString(hashBytes);
        }
    }

    public static string ByteArrayToHexString(byte[] byteArray)
    {
        var hex = new StringBuilder(byteArray.Length * 2);
        foreach (var b in byteArray)
        {
            hex.AppendFormat("{0:x2}", b);
        }
        return hex.ToString();
    }

    public static string PrepareString(string value, out string hash)
    {
        hash = string.Empty;
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

            dicResult.Add(keyValue[0], keyValue[1]);
        }

        return string.Join("\n", dicResult.OrderBy(x => x.Key).Select(prop => $"{prop.Key}={prop.Value}"));
    }
}