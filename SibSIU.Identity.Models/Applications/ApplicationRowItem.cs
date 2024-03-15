namespace SibSIU.Identity.Models.Applications;
public sealed class ApplicationRowItem
{
    public string ClientId { get; }
    public string Name { get; }

    public ApplicationRowItem(string clientId, string name)
    {
        ClientId = clientId;
        Name = name;
    }

    public ApplicationRowItem() : this(string.Empty, string.Empty) { }
}
