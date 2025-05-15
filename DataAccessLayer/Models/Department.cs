using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Department
{
    [Key]
    public int Id {get; set;}
    public required string Name {get; set;}

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<Student> Student { get; set; } = new List<Student>();

}
