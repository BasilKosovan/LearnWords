using System.Collections.Concurrent;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Common;
using Telegram.Common.Menu;

internal class Program
{
    private static readonly string botToken = Environment.GetEnvironmentVariable("botToken");
    public static readonly ITelegramBotClient botClient = new TelegramBotClient(botToken);
    public static readonly IInitialKeyboard _initialKeyboard = new InitialKeyboard();

    public static readonly ConcurrentDictionary<long, UserSession> Sessions = new ConcurrentDictionary<long, UserSession>();

    public static async Task Main(string[] args)
    {

        var me = await botClient.GetMe();
        Console.WriteLine($"Bot started: @{me.Username}");

        // Start receiving messages
        var cts = new CancellationTokenSource();
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            cancellationToken: cts.Token
        );

        Console.ReadKey();
    }

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            Console.WriteLine($"update.Type {update?.Type}. update.message {update?.Message} Data: {update?.CallbackQuery?.Data} Text: {update?.Message?.Text}");
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                await ProcessMessageRequest(botClient, update);
            }
            else if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery != null)
            {
            }
        }
        catch (Exception ex)
        {
            //await TradingMenu.SetInitialKeyboard(botClient, update.Message.Chat.Id, "Oops! An error has occurred. We are working on it.");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            throw;
        }
    }

    private static async Task ProcessMessageRequest(ITelegramBotClient botClient, Update update)
    {
        var chatId = update.Message.Chat.Id;
        if (!Sessions.ContainsKey(chatId))
        {
            Sessions[chatId] = new UserSession();
        }
        var userSession = Sessions[chatId];

        var message = update.Message.Text;
        var messageResponse = string.Empty;

        if (message == BotOptions.GoToMainMenu.GetDescription())
        {
            await MenuHelper.SetInitialKeyboard(botClient, chatId);
            return;
        }

        if (message == "/start")
        {
            await MenuHelper.SetInitialKeyboard(botClient, chatId);
            return;
        }

        if (message == BotOptions.AddNewWordCommand.GetDescription() || userSession.ActiveWizard is AddNewWordCommandWizard)
        {
            if (userSession.ActiveWizard is null)
            {
                userSession.ActiveWizard = new AddNewWordCommandWizard();
            }
            userSession.ActiveWizard.Context = new WizardContext(botClient, chatId, message);

            await StepsProcessor.ProcessSteps(userSession.ActiveWizard, _initialKeyboard);
            return;
        }

        messageResponse = $"Sorry bro.";

        await botClient.SendMessage(
                chatId: chatId,
                text: messageResponse,
                replyMarkup: new ReplyKeyboardMarkup(),
                cancellationToken: CancellationToken.None
            );

        await MenuHelper.SetInitialKeyboard(botClient, chatId);
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Error occurred: {exception.Message}");
        return Task.CompletedTask;
    }
}
