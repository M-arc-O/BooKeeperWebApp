namespace BooKeeperWebApp.Shared.Services.Csv;

public interface ICsvService
{
    IEnumerable<T> ParseCsv<T>(char seperator, string filePath, bool hasHeader = false, bool removeQuotes = false) where T : class;
    IEnumerable<T> ParseCsv<T>(char seperator, string[] input, bool hasHeader = false, bool removeQuotes = false) where T : class;
}