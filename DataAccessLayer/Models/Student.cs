using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("UserCred")]
    public int UserCredId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    [Required]
    public int CreditsEarned { get; set; }


    public virtual UserCred UserCred { get; set; } = null!;
    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollment { get; set; } = new List<Enrollment>();


}
