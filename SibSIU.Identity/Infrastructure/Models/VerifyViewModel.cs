using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Infrastructure.Models;

//TODO : set names by resources
public class VerifyViewModel
{
    [Display(Name = "Приложение")]
    public string ApplicationName { get; set; } = null!;

    [BindNever, Display(Name = "Ошибка")]
    public string Error { get; set; } = null!;

    [Display(Name = "Область")]
    public string Scope { get; set; } = null!;

    [FromQuery(Name = OpenIddictConstants.Parameters.UserCode)]
    [Display(Name = "Код пользователя")]
    public string UserCode { get; set; } = null!;
}
