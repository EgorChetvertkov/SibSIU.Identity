using SibSIU.Core.Services;

namespace SibSIU.Domain.ExternalApplication;
public struct ValueString(string value)
{
    public readonly string Value => value.TrimOrEmpty();
}
