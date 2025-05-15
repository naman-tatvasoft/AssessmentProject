using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Course
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    [Required]
    [Range(1, 6, ErrorMessage = "Credits must be between 1 and 6.")]
    public int Credits { get; set; }

    [Required]
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    public bool isDeleted {get; set;} = false;

    public bool isAavailable {get; set;} = true;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollment { get; set; } = new List<Enrollment>();

}