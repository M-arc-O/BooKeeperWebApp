using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using BooKeeperWebApp.Shared.Exceptions;

namespace BooKeeperWebApp.Shared.Services.Csv;
public class CsvService : ICsvService
{
    private char _seperator;
    private List<CsvPropertyInfo> PropertyInfos = new();

    public IEnumerable<T> ParseCsv<T>(char seperator, string[] input, bool hasHeader = false, bool trimQuotes = false) where T : class
    {
        _seperator = seperator;
        List<T> retVal = Process<T>(hasHeader, trimQuotes, ref input);
        return retVal;
    }

    public IEnumerable<T> ParseCsv<T>(char seperator, string filePath, bool hasHeader = false, bool trimQuotes = false) where T : class
    {
        _seperator = seperator;
        var lines = File.ReadAllLines(filePath);
        List<T> retVal = Process<T>(hasHeader, trimQuotes, ref lines);

        return retVal;
    }

    private List<T> Process<T>(bool hasHeader, bool trimQuotes, ref string[] lines) where T : class
    {
        var retVal = new List<T>();
        var headers = Array.Empty<string>();

        var type = typeof(T);
        var properties = type.GetProperties();

        if (hasHeader)
        {
            headers = lines[0].Split(new char[] { _seperator }, StringSplitOptions.None);
            headers = headers.Select(x => TrimValue(x, trimQuotes)).ToArray();
            lines = lines.Skip(1).ToArray();
        }

        foreach (var property in properties)
        {
            var column = 0;
            var format = string.Empty;
            var attributes = property.GetCustomAttributes(true);

            if (hasHeader)
            {
                var headerToFind = property.Name;

                if (attributes != null)
                {
                    var displayAttribute = attributes.FirstOrDefault(a => a.GetType() == typeof(ColumnAttribute));
                    if (displayAttribute != null)
                    {
                        headerToFind = (displayAttribute as ColumnAttribute).Name;
                    }
                }

                column = Array.FindIndex(headers, h => h.Equals(headerToFind));

                if (column < 0)
                {
                    throw new KeyNotFoundException($"Could not find '{headerToFind}' in csv headers.");
                }

            }
            else
            {
                var columnAttribute = attributes?.FirstOrDefault(a => a.GetType() == typeof(ColumnAttribute));
                if (columnAttribute == null)
                {
                    throw new MissingAttributeException($"Column attribute not found on property '{property.Name}'");
                }

                column = (columnAttribute as ColumnAttribute).Order;
            }

            PropertyInfos.Add(new CsvPropertyInfo { Property = property, ColumnNumber = column, Format = GetFormat(attributes) });
        }

        var lineIndex = 0;

        foreach (var line in lines)
        {
            if (!line.All((x) => x == _seperator || char.IsWhiteSpace(x)))
            {
                var entitie = (T)Activator.CreateInstance(type);
                var splitLine = line.Split(new char[] { _seperator }, StringSplitOptions.None);
                splitLine = splitLine.Select(x => TrimValue(x, trimQuotes)).ToArray();

                foreach (var propertyInfo in PropertyInfos)
                {
                    if (propertyInfo.ColumnNumber < 0 || propertyInfo.ColumnNumber >= splitLine.Length)
                    {
                        throw new IndexOutOfRangeException($"The column value '{propertyInfo.ColumnNumber}' is out of range of the csv line.");
                    }

                    var valueToConvert = splitLine[propertyInfo.ColumnNumber];
                    SetPropertyValue<T>(entitie, propertyInfo.Property, propertyInfo.Format, valueToConvert, lineIndex);
                }

                retVal.Add(entitie);
            }

            lineIndex++;
        }

        return retVal;
    }

    private static string GetFormat(object[] attributes)
    {
        var displayFormatAttribute = attributes?.FirstOrDefault(a => a.GetType() == typeof(DisplayFormatAttribute));
        if (displayFormatAttribute != null)
        {
            return (displayFormatAttribute as DisplayFormatAttribute).DataFormatString;
        }

        return string.Empty;
    }

    private static void SetPropertyValue<T>(T entitie, PropertyInfo property, string format, string valueToConvert, int line)
    {
        var value = new object();

        try
        {
            if (property.PropertyType == typeof(DateTime))
            { 
                value = DateTime.ParseExact(valueToConvert, format, System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(property.PropertyType);
                if (converter.CanConvertFrom(typeof(string)))
                {
                    if (property.PropertyType == typeof(double))
                    {
                        valueToConvert = valueToConvert.Replace(',', '.');
                        value = converter.ConvertFromString(valueToConvert);
                    }
                    else
                    {
                        value = converter.ConvertFromString(valueToConvert);
                    }
                }
            }
        }
        catch (ArgumentException ex)
        {
            throw new ConvertException($"Error while converting value for '{property.Name}' on line {line}, {ex.Message}");
        }

        property.SetValue(entitie, value);
    }

    private static string TrimValue(string value, bool trimQuotes)
    {
        var charactersToTrim = trimQuotes ? new[] { ' ', '"' } : new[] { ' ' };
        return value.Trim(charactersToTrim);
    }
}
