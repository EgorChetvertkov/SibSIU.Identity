using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.Units;
public sealed class UnitDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    [Required]
    public string ShortName { get; set; } = null!;
    public UnitItem? Parent { get; set; }
    public List<UnitItem> Children { get; set; } = [];
}
