using Telegram.Bot;
using Telegram.Common;
using Telegram.Bot.Types.ReplyMarkups;

public static class MenuHelper
{
    public static async Task SetInitialKeyboard(ITelegramBotClient botClient, long chatId, string text = null)
    {
        if (Program.Sessions.TryGetValue(chatId, out var userSession))
        {
            Program.Sessions.TryRemove(chatId, out userSession);
        }

        // Send a welcome message with inline buttons
        var inlineKeyboard = new[]
        {
            new[] // First row
            {
                new KeyboardButton(BotOptions.AddNewWordCommand.GetDescription()),
                new KeyboardButton(BotOptions.RepeatRandomWordCommand.GetDescription()),
            },
            new[] // Second row
            {
                new KeyboardButton(BotOptions.GoToMainMenu.GetDescription()),
            }
        };
        await botClient.SendMessage(
            chatId: chatId,
            text: text ?? "Welcome to the Vocabulary Bot!",
            replyMarkup: new ReplyKeyboardMarkup(inlineKeyboard),
            cancellationToken: CancellationToken.None
        );
    }

    public static KeyboardButton GetGoToMainMenuBtn()
    {
        return new KeyboardButton(BotOptions.GoToMainMenu.GetDescription());
    }
}
