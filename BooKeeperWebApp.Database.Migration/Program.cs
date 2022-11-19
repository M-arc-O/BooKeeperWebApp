using DbUp;
using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var connectionString = builder.GetConnectionString("DefaultConnection");
EnsureDatabase.For.SqlDatabase(connectionString);

Console.WriteLine(connectionString);

var upgrader = DeployChanges.To
    .SqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .LogToConsole()
    .JournalToSqlTable("dbo", "_DbUpMigrations")
    .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
}
else
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Successfully upgraded datatbase!");
}

Console.ResetColor();
Console.ReadLine();