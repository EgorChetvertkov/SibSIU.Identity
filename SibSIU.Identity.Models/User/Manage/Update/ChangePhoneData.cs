using System.ComponentModel.DataAnnotations;

namespace SibSIU.Identity.Models.User.Manage.Update;
public class ChangePhoneData
{
    [Required, DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;
}
