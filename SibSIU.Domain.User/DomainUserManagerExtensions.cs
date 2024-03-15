using Microsoft.Extensions.DependencyInjection;

using SibSIU.Domain.UserManager.ClaimTypes.Commands.Create;
using SibSIU.Domain.UserManager.ClaimTypes.Commands.Delete;
using SibSIU.Domain.UserManager.ClaimTypes.Commands.Update;
using SibSIU.Domain.UserManager.ClaimTypes.Queries.GetDetails;
using SibSIU.Domain.UserManager.ClaimTypes.Queries.GetPage;
using SibSIU.Domain.UserManager.ClaimTypes.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Groups.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Organizations.Commands.Create;
using SibSIU.Domain.UserManager.Organizations.Commands.Delete;
using SibSIU.Domain.UserManager.Organizations.Commands.Update;
using SibSIU.Domain.UserManager.Organizations.Queries.GetDetails;
using SibSIU.Domain.UserManager.Organizations.Queries.GetPage;
using SibSIU.Domain.UserManager.Organizations.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Posts.Commands.Create;
using SibSIU.Domain.UserManager.Posts.Commands.Delete;
using SibSIU.Domain.UserManager.Posts.Commands.Update;
using SibSIU.Domain.UserManager.Posts.Queries.GetDetails;
using SibSIU.Domain.UserManager.Posts.Queries.GetPage;
using SibSIU.Domain.UserManager.Posts.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Schools.Commands.Create;
using SibSIU.Domain.UserManager.Schools.Commands.Delete;
using SibSIU.Domain.UserManager.Schools.Commands.Update;
using SibSIU.Domain.UserManager.Schools.Queries.GetDetails;
using SibSIU.Domain.UserManager.Schools.Queries.GetPage;
using SibSIU.Domain.UserManager.Schools.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Scopes.Commands.Create;
using SibSIU.Domain.UserManager.Scopes.Commands.Delete;
using SibSIU.Domain.UserManager.Scopes.Commands.Update;
using SibSIU.Domain.UserManager.Scopes.Queries.GetDetails;
using SibSIU.Domain.UserManager.Scopes.Queries.GetPage;
using SibSIU.Domain.UserManager.Scopes.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Units.Commands.Create;
using SibSIU.Domain.UserManager.Units.Commands.Delete;
using SibSIU.Domain.UserManager.Units.Commands.Update;
using SibSIU.Domain.UserManager.Units.Queries.GetDetails;
using SibSIU.Domain.UserManager.Units.Queries.GetPage;
using SibSIU.Domain.UserManager.Units.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Users.Commands.AddClaim;
using SibSIU.Domain.UserManager.Users.Commands.AddPartner;
using SibSIU.Domain.UserManager.Users.Commands.AddPupil;
using SibSIU.Domain.UserManager.Users.Commands.AddStudent;
using SibSIU.Domain.UserManager.Users.Commands.AddWorkPlace;
using SibSIU.Domain.UserManager.Users.Commands.ChangeBirthday;
using SibSIU.Domain.UserManager.Users.Commands.ChangeEmail;
using SibSIU.Domain.UserManager.Users.Commands.ChangeFIO;
using SibSIU.Domain.UserManager.Users.Commands.ChangePassword;
using SibSIU.Domain.UserManager.Users.Commands.ChangePasswordByAdmin;
using SibSIU.Domain.UserManager.Users.Commands.ChangePhone;
using SibSIU.Domain.UserManager.Users.Commands.ChangeUserName;
using SibSIU.Domain.UserManager.Users.Commands.ConfirmedEmail;
using SibSIU.Domain.UserManager.Users.Commands.CreateUser;
using SibSIU.Domain.UserManager.Users.Commands.ForgotPassword;
using SibSIU.Domain.UserManager.Users.Commands.Login;
using SibSIU.Domain.UserManager.Users.Commands.RegisterAsPartner;
using SibSIU.Domain.UserManager.Users.Commands.RegisterAsPupil;
using SibSIU.Domain.UserManager.Users.Commands.RegisterAsStudent;
using SibSIU.Domain.UserManager.Users.Commands.RejectRegistration;
using SibSIU.Domain.UserManager.Users.Commands.RemoveClaim;
using SibSIU.Domain.UserManager.Users.Commands.RemovePartner;
using SibSIU.Domain.UserManager.Users.Commands.RemovePupil;
using SibSIU.Domain.UserManager.Users.Commands.RemoveStudent;
using SibSIU.Domain.UserManager.Users.Commands.RemoveWorkPlace;
using SibSIU.Domain.UserManager.Users.Commands.ResetPassword;
using SibSIU.Domain.UserManager.Users.Commands.SubmitRegistration;
using SibSIU.Domain.UserManager.Users.Queries.GetDetails;
using SibSIU.Domain.UserManager.Users.Queries.GetPage;
using SibSIU.Domain.UserManager.Users.Queries.GetSelectList;
using SibSIU.Domain.UserManager.Users.Queries.GetStudentsWithoutUser;
using SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedDetails;
using SibSIU.Domain.UserManager.Users.Queries.GetUnconfirmedPage;
using SibSIU.Domain.UserManager.Users.Queries.GetUserInfoByUserName;
using SibSIU.Identity.Web.Utils;

namespace SibSIU.Domain.UserManager;
public static class DomainUserManagerExtensions
{
    public static void AddUserManagerServices(this IServiceCollection services)
    {
        services.AddSingleton<MailPathForUserManager>();
        services.AddScoped<IGetUserPageHandler, GetUserPageHandler>();
        services.AddScoped<IGetUserDetailsHandler, GetUserDetailsHandler>();
        services.AddScoped<IGetUserInfoByUserNameHandler, GetUserInfoByUserNameHandler>();

        services.AddScoped<ICreateUserHandler, CreateUserHandler>();

        services.AddScoped<IChangeUserNameHandler, ChangeUserNameHandler>();
        services.AddScoped<IChangePhoneHandler, ChangePhoneHandler>();
        services.AddScoped<IChangeBirthdayHandler, ChangeBirthdayHandler>();
        services.AddScoped<IChangeEmailHandler, ChangeEmailHandler>();
        services.AddScoped<IChangePasswordHandler, ChangePasswordHandler>();
        services.AddScoped<IChangePasswordByAdminHandler, ChangePasswordByAdminHandler>();
        services.AddScoped<IChangeFIOHandler, ChangeFIOHandler>();
        
        services.AddScoped<IAddStudentHandler, AddStudentHandler>();
        services.AddScoped<IRemoveStudentHandler, RemoveStudentHandler>();
        services.AddScoped<IAddPupilHandler, AddPupilHandler>();
        services.AddScoped<IRemovePupilHandler, RemovePupilHandler>();
        services.AddScoped<IAddPartnerHandler, AddPartnerHandler>();
        services.AddScoped<IRemovePartnerHandler, RemovePartnerHandler>();
        services.AddScoped<IAddWorkPlaceHandler, AddWorkPlaceHandler>();
        services.AddScoped<IRemoveWorkPlaceHandler, RemoveWorkPlaceHandler>();
        services.AddScoped<IAddClaimHandler, AddClaimHandler>();
        services.AddScoped<IRemoveClaimHandler, RemoveClaimHandler>();
        
        services.AddScoped<ICreateOrganizationHandler, CreateOrganizationHandler>();
        services.AddScoped<IUpdateOrganizationHandler, UpdateOrganizationHandler>();
        services.AddScoped<IDeleteOrganizationHandler, DeleteOrganizationHandler>();
        services.AddScoped<IGetOrganizationDetailsHandler, GetOrganizationDetailsHandler>();
        services.AddScoped<IGetOrganizationSelectListHandler, GetOrganizationSelectListHandler>();
        services.AddScoped<IGetOrganizationPageHandler, GetOrganizationPageHandler>();

        services.AddScoped<ICreatePostHandler, CreatePostHandler>();
        services.AddScoped<IUpdatePostHandler, UpdatePostHandler>();
        services.AddScoped<IDeletePostHandler, DeletePostHandler>();
        services.AddScoped<IGetPostDetailsHandler, GetPostDetailsHandler>();
        services.AddScoped<IGetPostSelectListHandler, GetPostSelectListHandler>();
        services.AddScoped<IGetPostPageHandler, GetPostPageHandler>();

        services.AddScoped<ICreateSchoolHandler, CreateSchoolHandler>();
        services.AddScoped<IUpdateSchoolHandler, UpdateSchoolHandler>();
        services.AddScoped<IDeleteSchoolHandler, DeleteSchoolHandler>();
        services.AddScoped<IGetSchoolDetailsHandler, GetSchoolDetailsHandler>();
        services.AddScoped<IGetSchoolSelectListHandler, GetSchoolSelectListHandler>();
        services.AddScoped<IGetSchoolPageHandler, GetSchoolPageHandler>();

        services.AddScoped<ICreateUnitHandler, CreateUnitHandler>();
        services.AddScoped<IDeleteUnitHandler, DeleteUnitHandler>();
        services.AddScoped<IUpdateUnitHandler, UpdateUnitHandler>();
        services.AddScoped<IGetUnitDetailsHandler, GetUnitDetailsHandler>();
        services.AddScoped<IGetUnitPageHandler, GetUnitPageHandler>();
        services.AddScoped<IGetUnitSelectListHandler, GetUnitSelectListHandler>();

        services.AddScoped<ICreateScopeHandler, CreateScopeHandler>();
        services.AddScoped<IDeleteScopeHandler, DeleteScopeHandler>();
        services.AddScoped<IUpdateScopeHandler, UpdateScopeHandler>();
        services.AddScoped<IGetScopeDetailsHandler, GetScopeDetailsHandler>();
        services.AddScoped<IGetScopePageHandler, GetScopePageHandler>();
        services.AddScoped<IGetScopeSelectListHandler, GetScopeSelectListHandler>();

        services.AddScoped<ICreateClaimTypeHandler, CreateClaimTypeHandler>();
        services.AddScoped<IDeleteClaimTypeHandler, DeleteClaimTypeHandler>();
        services.AddScoped<IUpdateClaimTypeHandler, UpdateClaimTypeHandler>();
        services.AddScoped<IGetClaimTypeDetailsHandler, GetClaimTypeDetailsHandler>();
        services.AddScoped<IGetClaimTypePageHandler, GetClaimTypePageHandler>();
        services.AddScoped<IGetClaimTypeSelectListHandler, GetClaimTypeSelectListHandler>();

        services.AddScoped<IRegisterAsPartnerHandler, RegisterAsPartnerHandler>();
        services.AddScoped<IRegisterAsPupilHandler, RegisterAsPupilHandler>();
        services.AddScoped<IRegisterAsStudentHandler, RegisterAsStudentHandler>();
        services.AddScoped<ILoginHandler, LoginHandler>();

        services.AddScoped<IGetUserSelectListHandler, GetUserSelectListHandler>();
        services.AddScoped<IGetGroupSelectListHandler, GetGroupSelectListHandler>();
        services.AddScoped<IGetStudentsWithoutUserHandler, GetStudentsWithoutUserHandler>();
        services.AddScoped<IGetUnconfirmedUserPageHandler, GetUnconfirmedUserPageHandler>();
        services.AddScoped<IGetUnconfirmedDetailsHandler, GetUnconfirmedDetailsHandler>();
        services.AddScoped<IRejectRegistrationHandler, RejectRegistrationHandler>();
        services.AddScoped<ISubmitRegistrationHandler, SubmitRegistrationHandler>();

        services.AddScoped<IConfirmedEmailHandler, ConfirmedEmailHandler>();
        services.AddScoped<IForgotPasswordHandler, ForgotPasswordHandler>();
        services.AddScoped<IResetPasswordHandler, ResetPasswordHandler>();
    }
}
