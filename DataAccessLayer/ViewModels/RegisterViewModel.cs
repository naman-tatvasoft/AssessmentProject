using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    [RegularExpression (@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least 8 characters, including uppercase, lowercase, numbers and special characters")]
    public string Password { get; set; }

    [Required]
    public int DepartmentId { get; set; }
}
