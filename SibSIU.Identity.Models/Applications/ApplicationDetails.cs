using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.Applications;
public sealed class ApplicationDetails
{
    [Required]
    public string ApplicationType { get; set; } = string.Empty;
    [Required]
    public string ClientId { get; set; } = string.Empty;
    [Required]
    public string ClientType { get; set; } = string.Empty;
    public string? ClientSecret { get; set; }
    public string? JSONWebKeySet { get; set; }
    [Required]
    public string ConsentType { get; set; } = string.Empty;
    [Required]
    public string DisplayName { get; set; } = string.Empty;
    [Required]
    public List<string> Permissions { get; set; } = [];
    public List<Uri> RedirectUris { get; set; } = [];
    public List<Uri> PostLogoutRedirectUris { get; set; } = [];
    [Required]
    public List<string> Requirements { get; set; } = [];
    [Required]
    public Dictionary<string, string> Settings { get; set; } = new(StringComparer.Ordinal);
}
