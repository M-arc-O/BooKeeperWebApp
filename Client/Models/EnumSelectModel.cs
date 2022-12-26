namespace Client.Models;

public class EnumSelectModel<T>
{
    public T EnumValue { get; set; }
    public string EnumString { get; set; }

    public static List<EnumSelectModel<T>> GetEnumModel()
    {
        var retVal = new List<EnumSelectModel<T>>();
        var types = Enum.GetValues(typeof(T)).Cast<T>();
        foreach (var type in types)
        {
            retVal.Add(new EnumSelectModel<T> { EnumValue = type, EnumString = type!.ToString()! });
        }

        return retVal;
    }
}
