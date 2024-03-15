using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Models.User.Register;

using System.Net.Mail;

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsPartner;
public sealed class RegisterAsPartnerRequest : IRequest<Result<Message>>, IValidated
{
    public string UserName { get; }
    public string Email { get; }
    public string? PhoneNumber { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }
    public DateTimeOffset BirthOfDate { get; }
    public Ulid GenderId { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }
    public Ulid OrganizationId { get; }
    public Ulid PostId { get; }

    public RegisterAsPartnerRequest(RegisterAsPartnerData data)
    {
        UserName = data.UserName.TrimOrEmpty();
        Email = data.Email.TrimOrEmpty();
        PhoneNumber = data.PhoneNumber?.Trim();
        FirstName = data.FirstName.TrimOrEmpty();
        LastName = data.LastName.TrimOrEmpty();
        Patronymic = data.Patronymic?.Trim();
        BirthOfDate = data.BirthOfDate!.Value.Date;
        GenderId = Ulid.Parse(data.GenderId);
        Password = data.Password.TrimOrEmpty();
        ConfirmPassword = data.ConfirmPassword.TrimOrEmpty();
        OrganizationId = Ulid.Parse(data.OrganizationId);
        PostId = Ulid.Parse(data.PostId);
    }

    public RegisterAsPartnerRequest() : this(new()) { }

    public Error Validate()
    {
        if (string.IsNullOrWhiteSpace(UserName))
        {
            return UserErrors.InvalidUserName;
        }

        if (!MailAddress.TryCreate(Email, out _))
        {
            return UserErrors.InvalidEmail;
        }

        if (PhoneNumber is not null && !PhoneValidation.Validate(PhoneNumber))
        {
            return UserErrors.InvalidPhone;
        }

        if (string.IsNullOrWhiteSpace(FirstName))
        {
            return UserErrors.FirstNameAreEmpty;
        }

        if (string.IsNullOrWhiteSpace(LastName))
        {
            return UserErrors.LastNameAreEmpty;
        }

        if (BirthOfDate.ToUniversalTime().Date.Year - DateTimeOffset.UtcNow.ToUniversalTime().Date.Year >= 14)
        {
            return UserErrors.AgeAreSmall;
        }

        if (GenderId == Ulid.Empty)
        {
            return UserErrors.GenderAreEmpty;
        }

        if (Password.Length < 8)
        {
            return UserErrors.PasswordAreShort;
        }

        if (Password != ConfirmPassword)
        {
            return UserErrors.PasswordNotCompare;
        }

        if (OrganizationId == Ulid.Empty)
        {
            return OrganizationErrors.InvalidOrganizationId;
        }

        if (PostId == Ulid.Empty)
        {
            return PostErrors.InvalidPostId;
        }

        return Error.None;
    }
}
