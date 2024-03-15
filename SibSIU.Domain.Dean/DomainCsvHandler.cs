using CsvHelper;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace SibSIU.Domain.Dean;
internal static class DomainCsvHandler
{
    public static string WriteCsvByList<T>(List<T> data)
    {
        using var writer = new StringWriter();
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(data);
        }

        return writer.ToString();
    }

    public static List<T> ReadCsvToList<T>(IFormFile csvFile)
    {
        using var reader = new StreamReader(csvFile.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csv.GetRecords<T>().ToList();
    }
}
