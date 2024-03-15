using Microsoft.AspNetCore.Hosting;

namespace SibSIU.Identity.Web.Utils;

public class MailPathForUserManager(IWebHostEnvironment environment)
{
    private static readonly string _mailTemplateFolder = "EmailTemplates";
    private static readonly string _imageTemplateFolder = "Images";
    public string LogoPath => Path.Combine(environment.WebRootPath, _mailTemplateFolder, _imageTemplateFolder, "logo.png");
    public string ChangePasswordTemplatePath => Path.Combine(environment.WebRootPath, _mailTemplateFolder, "ChangePassword.cshtml");
    public string ChangePhoneTemplatePath => Path.Combine(environment.WebRootPath, _mailTemplateFolder, "ChangePhone.cshtml");
    public string ChangeUserNameTemplatePath => Path.Combine(environment.WebRootPath, _mailTemplateFolder, "ChangeUserName.cshtml");
    public string RegistrationTemplatePath => Path.Combine(environment.WebRootPath, _mailTemplateFolder, "Registration.cshtml");
    public string EmailConfirmedTemplatePath => Path.Combine(environment.WebRootPath, _mailTemplateFolder, "ConfirmedEmail.cshtml");
    public string ForgotPasswordTemplatePath => Path.Combine(environment.WebRootPath, _mailTemplateFolder, "ForgotPassword.cshtml");
}
