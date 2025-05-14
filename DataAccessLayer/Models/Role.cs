using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<UserCred> UserCred { get; set; } = new List<UserCred>();
}
