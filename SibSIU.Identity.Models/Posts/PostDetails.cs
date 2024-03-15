using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.Posts;
public sealed class PostDetails
{
    [Required]
    public Ulid Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
}
