namespace Domain;

public record Word(string Value,
                   string Translation,
                   string ExampleSentence,
                   Language WordLanguage,
                   Language TranslationLanguage,
                   FamiliarityLevel FamiliarityLevel = FamiliarityLevel.Unknown);

public enum Language
{
    English,
    Ukrainian,
    Spanish
}

public enum FamiliarityLevel
{
    Unknown = 0,
    Beginner = 1,
    Intermediate = 2,
    Advanced = 3,
    Mastered = 4
}

public class Account
{
    private readonly List<Word> _words = new();
    public IReadOnlyCollection<Word> Words => _words.AsReadOnly();

    public void AddWord(Word word)
    {
        if (!_words.Contains(word))
        {
            _words.Add(word);
        }
    }

    public bool DeleteWord(Word word)
    {
        return _words.Remove(word);
    }
}