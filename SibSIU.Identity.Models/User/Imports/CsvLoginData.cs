namespace SibSIU.Identity.Models.User.Imports;
public sealed class CsvLoginData
{
    public string Data { get; init; }

    public CsvLoginData() : this(string.Empty) { }

    public CsvLoginData(string data)
    {
        Data = data;
    }
}
