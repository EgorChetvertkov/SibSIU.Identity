namespace SibSIU.Domain.Dean.Synchronization.Commands.ImportingStudentsFromDean.Models;
internal sealed class UserNameAndPassword
{
    public string UserName { get; private set; }
    public string EmailAddress { get; private set; }
    public string Password { get; private set; }

    private UserNameAndPassword(string userName, string emailAddress, string password)
    {
        UserName = userName;
        EmailAddress = emailAddress;
        Password = password;
    }

    public static UserNameAndPassword CreateByUserName(string userName, string password)
    {
        return new(userName, $"{userName}@sibsiu.ru", password);
    }

    public static UserNameAndPassword CreateByEmail(string email, string password)
    {
        return new(email, email, password);
    }

    public override bool Equals(object? obj)
    {
        return obj is UserNameAndPassword other &&
            other.UserName == this.UserName && other.Password == this.Password;
    }

    public override int GetHashCode()
    {
        return UserName.GetHashCode() ^ Password.GetHashCode();
    }

    public override string ToString()
    {
        return UserName;
    }
}
