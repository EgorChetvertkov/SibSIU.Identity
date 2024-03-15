using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Partner;
public sealed class AddPartnerData
{
    [Required]
    public Ulid OrganizationId { get; set; }
    [Required]
    public Ulid PostId { get; set; }
}
