using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.Organizations;
public sealed class OrganizationDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string ShortName { get; set; } = null!;
    [Required]
    [StringLength(13, MinimumLength = 13)]
    public string OGRN { get; set; } = null!;
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string TIN { get; set; } = null!;
    [Required]
    [StringLength(9, MinimumLength = 9)]
    public string KPP { get; set; } = null!;
}
