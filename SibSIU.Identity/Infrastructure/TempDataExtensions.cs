using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace SibSIU.Identity.Infrastructure;

public static class TempDataExtensions
{
    public const string BaseKey = "toast_key";

    public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
    {
        tempData[key] = JsonConvert.SerializeObject(value);
    }
    public static T? Get<T>(this ITempDataDictionary tempData, string key) where T : class
    {
        tempData.TryGetValue(key, out object? o);
        return o is not null and string type ?
            JsonConvert.DeserializeObject<T>(type) :
            null;
    }
}
