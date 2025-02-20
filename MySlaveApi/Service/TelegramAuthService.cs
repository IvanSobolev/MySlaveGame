using System.Security.Cryptography;
using System.Text;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Service;

public class TelegramAuthService
{
    private readonly string _botToken;

    public TelegramAuthService(string path)
    {
        _botToken = File.ReadAllLines(path)[0];
    }
    
    public async Task<bool> ValidateTelegramData(string initData)
    {
        var data = initData.Split('&')
            .Select(part => part.Split('='))
            .ToDictionary(part => part[0], part => part[1]);

        if (!data.ContainsKey("hash"))
            return false;

        var hash = data["hash"];

        data.Remove("hash");

        var sortedData = data.OrderBy(pair => pair.Key)
            .Select(pair => $"{pair.Key}={pair.Value}")
            .ToArray();

        var dataCheckString = string.Join("\n", sortedData);

        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("WebAppData")))
        {
            var secretKey = hmac.ComputeHash(Encoding.UTF8.GetBytes(_botToken));
            using (var hmac2 = new HMACSHA256(secretKey))
            {
                var computedHash = hmac2.ComputeHash(Encoding.UTF8.GetBytes(dataCheckString));
                var computedHashHex = BitConverter.ToString(computedHash)
                    .Replace("-", "")
                    .ToLower();

                return computedHashHex == hash;
            }
        }
    }
    
    public TgUser ExtractUserData(string initData)
    {
        var data = initData.Split('&')
            .Select(part => part.Split('='))
            .ToDictionary(part => part[0], part => part[1]);

        var userJson = Uri.UnescapeDataString(data["user"]);
        return System.Text.Json.JsonSerializer.Deserialize<TgUser>(userJson);
    }
}