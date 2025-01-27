using Telegram.Common.Menu;

public class InitialKeyboard : IInitialKeyboard
{
    public async Task SetInitialKeyboard(WizardContext wizardContext, string text = null)
    {
        await MenuHelper.SetInitialKeyboard(wizardContext.botClient, wizardContext.chatId, text);
    }
}