using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.WorkPlace;
public sealed class AddWorkPlaceData
{
    [Required]
    public Ulid UnitId { get; set; }
    [Required]
    public Ulid PostId { get; set; }
}
