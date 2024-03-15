namespace SibSIU.Identity.Models.CORSes;
public sealed class CORSRowItem
{
    public Ulid Id { get; set; }
    public string Origin { get; set; }

    public CORSRowItem(Ulid id, string origin)
    {
        Id = id;
        Origin = origin;
    }

    public CORSRowItem() : this(Ulid.Empty, string.Empty) { }
}
