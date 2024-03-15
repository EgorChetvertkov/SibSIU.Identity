using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Pupil;
public sealed class AddPupilData
{
    [Required]
    [Range(1, 11)]
    public int ClassNumber { get; set; }
    [Required]
    public char ClassLitter { get; set; }
    [Required]
    public Ulid SchoolId { get; set; }
}
