using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.Models;

public class Enrollment
{
     [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Course")]
    public int CourseId { get; set; }

    [Required]
    [ForeignKey("Student")]
    public int StudentId { get; set; }

    public bool isCompleted {get; set;} = false;

    public bool isWithdrawn {get; set;} = false;

    
    public virtual Course Course { get; set; } = null!;
    public virtual Student Student { get; set; } = null!;
}
