using SibSIU.Core.Services;
using SibSIU.Core.Services.RequestInterfaces;
using SibSIU.Core.Services.ResultObject;
using SibSIU.Domain.UserManager.Errors;
using SibSIU.Domain.UserManager.Utils;
using SibSIU.Identity.Models.User.Register;
using System.Net.Mail;

namespace SibSIU.Domain.UserManager.Users.Commands.RegisterAsStudent;
public sealed class RegisterAsStudentRequest : IRequest<Result<Message>>, IValidated
{
    public int DeanCode { get; }
    public string UserName { get; }
    public string Email { get; }
    public string? PhoneNumber { get; }
    public DateTimeOffset BirthOfDate { get; }
    public Ulid GenderId { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }

    public RegisterAsStudentRequest(RegisterAsStudentData data)
    {
        DeanCode = data.DeanCode;
        UserName = data.UserName.TrimOrEmpty();
        Email = data.Email.TrimOrEmpty();
        PhoneNumber = data.PhoneNumber?.Trim();
        BirthOfDate = data.BirthOfDate.Date;
        GenderId = Ulid.Parse(data.GenderId);
        Password = data.Password.TrimOrEmpty();
        ConfirmPassword = data.ConfirmPassword.TrimOrEmpty();
    }

    public RegisterAsStudentRequest() : this(new()) { }

    public Error Validate()
    {
        if (DeanCode <= 0)
        {
            return StudentErrors.InvalidStudentDeanCode;
        }

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

        return Error.None;
    }
}
