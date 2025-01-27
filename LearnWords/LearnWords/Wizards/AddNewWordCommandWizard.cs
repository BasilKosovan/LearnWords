using Domain;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Common.Menu;
using Telegram.Common.Menu.Steps;

public class AddNewWordCommandWizard : BaseWizard
{
    public override IStepFactory StepFactory => new AddNewWordWizardFactory();

    public override async Task SaveWizardAsync()
    {
        var wizard = this;
        var wordText = (string)(wizard.Values[StepType.GetWordStep]);
        var translation = (string)(wizard.Values[StepType.GetWordTranslationStep]);

        var word = new Word(Value: wordText, Translation: translation, string.Empty, Language.English, Language.Ukrainian);
        await wizard.Context.botClient.SendMessage(
            chatId: wizard.Context.chatId,
            text: $"You word was added! {wordText}: {translation}",
            cancellationToken: CancellationToken.None
        );
    }

    public class AddNewWordWizardFactory : IStepFactory
    {
        public BaseStep CreateNextStep(StepType stepType) => stepType switch
        {
            StepType.Initial => new GetWordStep(),
            StepType.GetWordStep => new GetWordTranslationStep(),
            StepType.GetWordTranslationStep => new FinalStep(),
            _ => throw new ArgumentException($"Unsupported step type: {stepType}")
        };
    }
}

public class GetWordStep : BaseStep
{
    public override StepType StepType => StepType.GetWordStep;
    public override string Message => "What is your word?";

    public override KeyboardButton[][] GetKeyBoard()
    {
        return [[MenuHelper.GetGoToMainMenuBtn()]];
    }

    public override async Task<bool> SaveStepDecisionAsync(BaseWizard wizard)
    {
        wizard.Values.Add(StepType, wizard.Context.message);
        return true;
    }
}

public class GetWordTranslationStep : BaseStep
{
    public override StepType StepType => StepType.GetWordTranslationStep;
    public override string Message => "What is translation fo your word?";

    public override KeyboardButton[][] GetKeyBoard()
    {
        return [[MenuHelper.GetGoToMainMenuBtn()]];
    }

    public override async Task<bool> SaveStepDecisionAsync(BaseWizard wizard)
    {
        wizard.Values.Add(StepType, wizard.Context.message);
        return true;
    }
}