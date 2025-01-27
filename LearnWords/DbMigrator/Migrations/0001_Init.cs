using SimpleMigrations;

namespace DbMigrator.Migrations;

[Migration(1)]
public class Initial : Migration
{
    protected override void Down()
    {
        throw new NotImplementedException();
    }

    protected override void Up()
    {
        Execute(@"

-- Create the Account table
CREATE TABLE Accounts (
    AccountId INT IDENTITY PRIMARY KEY,
    ChatId BIGINT NOT NULL,            -- The unique chat identifier for the account
    CreationDate DATETIME DEFAULT GETDATE()  -- The date when the account was created
);

-- Create the Language enum table to store the enum values for WordLanguage and TranslationLanguage
CREATE TABLE Languages (
    LanguageId INT PRIMARY KEY,
    LanguageName NVARCHAR(50) NOT NULL
);

-- Insert the Language values (to represent the Language enum)
INSERT INTO Languages (LanguageId, LanguageName) VALUES
(0, 'English'),
(1, 'Ukrainian'),
(2, 'Spanish');

-- Create the FamiliarityLevel enum table to store the enum values for FamiliarityLevel
CREATE TABLE FamiliarityLevels (
    FamiliarityLevelId INT PRIMARY KEY,
    LevelName NVARCHAR(50) NOT NULL
);

-- Insert the FamiliarityLevel values (to represent the FamiliarityLevel enum)
INSERT INTO FamiliarityLevels (FamiliarityLevelId, LevelName) VALUES
(0, 'Unknown'),
(1, 'Beginner'),
(2, 'Intermediate'),
(3, 'Advanced'),
(4, 'Mastered');

-- Create the Word table
CREATE TABLE Words (
    WordId INT IDENTITY PRIMARY KEY,
    Value NVARCHAR(255) NOT NULL,      -- The word itself
    Translation NVARCHAR(255),         -- The translation of the word
    ExampleSentence NVARCHAR(500),     -- Example sentence with the word
    WordLanguageId INT,                -- Foreign Key to the Languages table (Word language)
    TranslationLanguageId INT,         -- Foreign Key to the Languages table (Translation language)
    FamiliarityLevelId INT,            -- Foreign Key to the FamiliarityLevels table
    AccountId INT,                     -- Foreign Key to the Accounts table
    FOREIGN KEY (WordLanguageId) REFERENCES Languages(LanguageId),
    FOREIGN KEY (TranslationLanguageId) REFERENCES Languages(LanguageId),
    FOREIGN KEY (FamiliarityLevelId) REFERENCES FamiliarityLevels(FamiliarityLevelId),
    FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId)
);


");
    }
}
