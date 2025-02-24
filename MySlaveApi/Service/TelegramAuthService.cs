using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.Extensions.Logging.Console;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Service;

public class TelegramAuthService
{
    private readonly string _botToken;

    public TelegramAuthService(string path)
    {
        Console.WriteLine(File.ReadAllLines(path)[0]);
        _botToken = File.ReadAllLines(path)[0];
    }
    
    public async Task<bool> ValidateTelegramData(TgAppData data)
    {
        //заглушка.
        return true;
    }
}