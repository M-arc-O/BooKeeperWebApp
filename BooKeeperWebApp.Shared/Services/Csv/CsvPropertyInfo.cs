using System.Reflection;

namespace BooKeeperWebApp.Shared.Services.Csv;
public class CsvPropertyInfo
{
    public PropertyInfo Property { get; set; }
    public int ColumnNumber { get; set; }
    public string Format { get; set; }
}
