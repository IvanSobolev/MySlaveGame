using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MySlaveTelegramBot;
using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        var bot = new TelegramBotClient(File.ReadAllLines("../../../../tg.token")[0]);
        var me = await bot.GetMeAsync();
        Console.WriteLine($"Bot started: {me.Username}");

        bot.StartReceiving(UpdateHandler, ErrorHandler);

        Console.ReadLine();
    }
    
    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Type == UpdateType.Message && update.Message.Text == "/start")
        {
            var webAppUrl = "https://web.telegram.org/k/";
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                InlineKeyboardButton.WithWebApp("Open WebApp", webAppUrl)
            });

            await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Click the button below to open the WebApp:",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }
    }
    
    private static Task ErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error: {exception.Message}");
        return Task.CompletedTask;
    }
}