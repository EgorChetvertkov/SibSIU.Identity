using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Infrastructure.Models;

public class VerifyViewModel
{
    [Display(ResourceType = typeof(Resources.Resource), Name = "ApplicationName")]
    public string ApplicationName { get; set; } = null!;

    [BindNever, Display(ResourceType = typeof(Resources.Resource), Name = "ErrorName")]
    public string Error { get; set; } = null!;

    [Display(ResourceType = typeof(Resources.Resource), Name = "Scope")]
    public string Scope { get; set; } = null!;

    [FromQuery(Name = OpenIddictConstants.Parameters.UserCode)]
    [Display(ResourceType = typeof(Resources.Resource), Name = "UserCode")]
    public string UserCode { get; set; } = null!;
}
