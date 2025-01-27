using System.ComponentModel;

public enum BotOptions
{
    [Description("Add a new word to your vocabulary list.")]
    AddNewWordCommand,

    [Description("Repeat a random word from your vocabulary list.")]
    RepeatRandomWordCommand,

    [Description("Cancel the current action and return to the main menu.")]
    GoToMainMenu,
}
