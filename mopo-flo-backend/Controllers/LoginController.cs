using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace mopo_flo_backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController : ControllerBase
{
    private const string TelegramBotToken = "6527269583:AAH_nb1LWdUgB3tGpWtLlSxzgQya74Zkrsg";
    [HttpPost]
    public async Task<IActionResult> Auth(LoginData loginDate)
    {
        var hash = loginDate.hash;
        var authDataJson = JObject.FromObject(loginDate);
        authDataJson.Remove("hash");
        authDataJson.Remove("photo_url");

        var dataCheckString = string.Join("\n", authDataJson.Properties().Select(prop => $"{prop.Name}={prop.Value}"));
        var secretKey = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(TelegramBotToken));
        var hmac = new HMACSHA256(secretKey);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
        var computedHashHex = BitConverter.ToString(computedHash).Replace("-", "").ToLowerInvariant();

        var result = computedHashHex == hash;

        return result ? Ok(result) : NotFound();
    }

    public class LoginData
    {
        public string id
        {
            get;
            set;
        }
        public string first_name { get; set; }
        public string? last_name { get; set; }
        public string? username { get; set; }
        public string? photo_url { get; set; }
        public long auth_date { get; set; }
        public string hash { get; set; }
    }
}