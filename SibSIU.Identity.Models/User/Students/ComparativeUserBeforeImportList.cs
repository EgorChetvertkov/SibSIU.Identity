namespace SibSIU.Identity.Models.User.Students;
public sealed class ComparativeUserBeforeImportList
{
    public string CacheKey { get; set; }
    public List<ComparativeUserBeforeImport> DataList { get; set; }

    public ComparativeUserBeforeImportList(string cacheKey, List<ComparativeUserBeforeImport> dataList)
    {
        CacheKey = cacheKey;
        DataList = dataList;
    }

    public ComparativeUserBeforeImportList() : this(string.Empty, []) { }
}
