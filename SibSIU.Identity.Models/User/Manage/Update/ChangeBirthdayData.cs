using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage.Update;
public class ChangeBirthdayData
{
    [Required]
    public DateTimeOffset Birthday { get; set; }
}
