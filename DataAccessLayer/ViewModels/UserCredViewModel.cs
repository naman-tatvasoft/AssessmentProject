using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class UserCredViewModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
}
