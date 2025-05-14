using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class UserCred
{
    [Key]
    public int Id {get; set;}
    public required string Email {get; set;}
    public required string Password {get; set;}
    
    [ForeignKey("Role")]
    public required int RoleId {get; set;}
    public DateTime CreatedAt { get; set; }

    public virtual Role Role { get; set; } = null!;
}

