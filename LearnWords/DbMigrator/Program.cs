using SimpleMigrations.DatabaseProvider;
using SimpleMigrations;
using Microsoft.Data.SqlClient;
using SimpleMigrations.Console;

var migrationsAssembly = typeof(Program).Assembly;
using (var connection = new SqlConnection("Application Name=LearnWords.Migrator;Server=localhost\\SQLSERVER;Trusted_Connection=True;Database=LearnWords;MultipleActiveResultSets=True;TrustServerCertificate=True"))
{
    var databaseProvider = new MssqlDatabaseProvider(connection);
    var migrator = new SimpleMigrator(migrationsAssembly, databaseProvider);
    migrator.Load();

    var runner = new ConsoleRunner(migrator);
    runner.Run(args);
}
